using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : IAttack
{
    public Button GBtn;
    public Button UBtn;

    private GameObject firstProjectile;

    void Start()
    {
        // key : 스킬 게임오브젝트 value : 각 스킬의 발사체 오브젝트
        TileDict = new SkillDic();
        // 스킬매니저를 통하여 해당 스킬의 발사체를 가져온다
        getProjectiles();
        // 각 발사체의 오브젝트 풀 생성
        createObjectPool();
        mAutoAttackSpeed = 1f; //TO-DO 임시로 넣어놓음. 실제 공속은 무엇?
        mAutoAttackCheckTime = mAutoAttackSpeed;
    }

    void Update()
    {
        if (mAutoAttackCheckTime > mAutoAttackSpeed)
        {
            // 자동공격 매서드
            {
                launchProjectile(
                    SkillManager.Instance.CurrentBaseSkill,
                    0,
                    MonsterManager.Instance.GetNearestMonsterPos(firePosition.transform.position),
                    firePosition.transform.position,
                    false);
            }
            mAutoAttackCheckTime = 0f;
        }
        mAutoAttackCheckTime += Time.deltaTime;
    }

    private void getProjectiles()
    {
        // 스킬 매니저를 통해 현재 장착중인 스킬을 받아온다
        pushProjectile(SkillManager.Instance.CurrentBaseSkill);
        pushProjectile(SkillManager.Instance.CurrentGeneralSkill);
        pushProjectile(SkillManager.Instance.CurrentUltimateSkill);
    }

    // 받아온 스킬의 발사체 리스트를 발사체매니저를 통해 받아온다
    //  <스킬, 발사체리스트> 타입의 Dic에 추가
    private void pushProjectile(Skill _skill)
    {
        List<Projectile> newList = new List<Projectile>();
        for (int i = 0; i < _skill.Spec.getProjectiles().Count; i++)
        {
            string projectile = _skill.Spec.getProjectiles()[i];
            Projectile newProjectile = ProjectileManager.Instance.allProjectiles[projectile].GetComponent<Projectile>();
            newList.Add(newProjectile);
        }
        setTileDict(_skill, newList);
    }

    //TO-DO 무기생성 Create를 여기서하는건 옳지않음.
    //PlayerManager에서 무기종류가 확실히 정해지면 거기서 Create하는걸로 수정
    private void createObjectPool()
    {
        foreach (Skill key in TileDict.Keys)
        {
            for (int i = 0; i < TileDict[key].Count; i++)
            {
                ObjectPoolManager.Instance.CreateDictTable(TileDict[key][i].gameObject, 10, 10);
            }
        }
    }

    // 발사체 데미지를 설정합니다.
    private void setProjectileData(ref GameObject obj)
    {
        obj.GetComponent<Projectile>().Damage = 
            (int)((50 + EquipmentManager.Instance.getCurrentDamage()) * obj.GetComponent<Projectile>().Spec.ProjectileDamage);
    }


    /*
     * 스킬 클릭 시 호출됩니다.
     */
    public void clickGeneralSkillBtn()
    {
        GBtn.gameObject.SetActive(false);
        StartCoroutine(useSkill(GBtn, SkillManager.Instance.CurrentGeneralSkill));
        StartCoroutine(activeAnimation());
    }
    public void clickUltimateSkillBtn()
    {
        UBtn.gameObject.SetActive(false);
        StartCoroutine(useSkill(UBtn, SkillManager.Instance.CurrentUltimateSkill));
        StartCoroutine(activeAnimation());
    }
    /*
     * 스킬로 공격 시 애니메이션 재생
     * 공격 애니메이션 재생시 움직임 불가(변수로 제어)
     * Todo : PlayerManager쪽으로 애니메이션 코드 옮기기
     */
    IEnumerator activeAnimation()
    {
        GetComponent<PlayerMove>().MMoveable = false;
        GetComponent<PlayerMove>().MAnim.SetFloat("AttackState", 0f);
        GetComponent<PlayerMove>().MAnim.SetFloat("NormalState", 0.5f);
        GetComponent<PlayerMove>().MAnim.SetTrigger("Attack");
        yield return new WaitForSeconds(1f); // Todo : 추후에 스킬 데이터에서 대기시간을 받아 입력 
        GetComponent<PlayerMove>().MMoveable = true;
    }

    IEnumerator useSkill(Button _btn, Skill _skill)
    {
        int count = _skill.Spec.SkillClickCount;
        // 한번 클릭
        if (count == 1)
        {
            launchSkill(_skill);
            yield return new WaitForSeconds(_skill.Spec.getSkillCoolTime()[_skill.CurrentCoolTimeIndex]);
            _btn.gameObject.SetActive(true);
        }
        // 무한 클릭(시간으로 관리)
        else if (count == -1)
        {
            // Todo : 쿨타임동안에는 무한발사가 가능한 스킬의 경우 
        }
        // 클릭횟수가 정해져 있는 경우
        else
        {
            // 마지막 클릭일 경우
            if (count - 1 == _skill.CurrentUseCount)
            {
                _skill.CurrentCoolTimeIndex++;
                launchSkill(_skill);
                yield return new WaitForSeconds(_skill.Spec.getSkillCoolTime()[_skill.CurrentCoolTimeIndex]);
                _skill.CurrentCoolTimeIndex = 0;
                _skill.CurrentUseCount = 0;
                _btn.gameObject.SetActive(true);
            }
            else
            {
                _skill.CurrentUseCount++;
                launchSkill(_skill);
                yield return new WaitForSeconds(_skill.Spec.getSkillCoolTime()[_skill.CurrentCoolTimeIndex]);
                _btn.gameObject.SetActive(true);
            }
        }
    }
    // 우선 발사체가 한개인 경우만 구현
    private void launchSkill(Skill _skill)
    {
        int count = _skill.Spec.SkillCount;
        int projectileCount = TileDict[_skill].Count;
        Vector3 targetPos = MonsterManager.Instance.GetNearestMonsterPos(firePosition.transform.position);
        Vector3 firePos = firePosition.transform.position;
        // 발사체가 한개인 경우
        if(projectileCount <= 1)
        {
            // 한번 발사일 경우
            if (count == 1)
            {
                launchProjectile(_skill, 0, targetPos, firePos, false);
            }
            // 중복 발사일 경우
            else
            {
                StartCoroutine(multiLuanch(_skill, count, targetPos, firePos));
            }
        }
        // 발사체가 2개 이상인경우
        // 1. 첫번째 발사체가 나가고 그자리에서 스폰
        // 2. 나갈때 발사체가 2개 이상인 경우
        else
        {
            // 1. 첫번째 발사체가 나가고 그자리에서 스폰
            StartCoroutine(notSingleProjectileLaunch(_skill, targetPos, firePos));
        }
    }
    // 실질적으로 발사 코루틴으르 호출하는 함수
    /*
    * 발사체의 개수 각도에 따른 원뿔형 발사 구현 입니다.(만약 각도가 없는 경우는 다른 타입으로 빼야됨)
    * luanchCount = 발사체의 발사 개수 + (static 변수)전체적인 발사체의 발사 개수(레벨업으로 인한)
     * angle = 발사될 발사체의 각도입니다.(즉 발사체끼리의 각도)
    * luanchCount만큼 발사가 되고, 발사될때마다 각도만큼 벌려줍니다.(원뿔형)
    */
    private void launchProjectile(Skill mSkill, int mProjectileIndex, Vector3 mTargetPos, Vector3 mFirePos, bool mNotSingle)
    {
        int launchCount = TileDict[mSkill][mProjectileIndex].Spec.Count + Projectile.AddProjectilesCount;
        int angle = TileDict[mSkill][mProjectileIndex].Spec.Angle;
        for (int i = 0; i < launchCount; i++)
        {
            StartCoroutine(
                LaunchCorutines(
                    (launchCount == 1 ? 0 : -((launchCount - 1) * angle / 2) + angle * i),
                    TileDict[mSkill][mProjectileIndex].gameObject.name,
                    mTargetPos,
                    mFirePos, mNotSingle));
        }
    }
    IEnumerator multiLuanch(Skill _skill, int _count, Vector3 _target, Vector3 _fire)
    {
        for (int i = 0; i < _count; i++)
        {
            launchProjectile(_skill, 0, _target, _fire, false);
            yield return new WaitForSeconds(0.4f); // 추후에 여러번 발사일 경우 해당 데이터값 입력
        }
    }
    // 하드 코딩 상태(발사체 2가지만 구현)
    IEnumerator notSingleProjectileLaunch(Skill _skill, Vector3 _target, Vector3 _fire)
    {
        launchProjectile(_skill, 0, _target, _fire, true);
        yield return new WaitWhile(() => firstProjectile.activeInHierarchy);
        launchProjectile(_skill, 1, _target, firstProjectile.GetComponent<Projectile>().MyPos, false);
    }
    /*
* _angle : 추가 각도 설정입니다.
* _name : 해당 발사체오브젝트의 name입니다.
*/
    IEnumerator LaunchCorutines(float _angle, string _name, Vector3 _targetPos, Vector3 _firePos, bool _notSingle)
    {
        Vector3 temp = new Vector3(1, 1, 0);//TO-DO 플레이어 방향에 따라 나갈수있도록 value 제공해주는 api만들기
        GameObject obj = ObjectPoolManager.Instance.EnableGameObject(_name);
        if (_notSingle) firstProjectile = obj;
        float keepTime = obj.GetComponent<Projectile>().Spec.SpawnTime;
        setProjectileData(ref obj);
        obj.GetComponent<Projectile>().CurrentPassCount = 0;
        obj.GetComponent<Projectile>().setEnable(_targetPos, _firePos, _angle);
        yield return new WaitForSeconds(keepTime);
        // 간헐적 disable문제때문에 해당 오브젝트의 Active가 true일 시만 disable되게 했습니다
        // 스폰시간이 다되기 전 발사체가 몬스터와 부딪힐 때 disable이 한번 호출되는데 혹시나 다른 객체로
        // 접근해서 disable해서 간헐적 문제가 발생할 수 있을거라 추측해서 조건문 걸었습니다.
        // 추후에 다른곳에서의 문제가 발견되면 삭제할 예정
        if (obj.activeInHierarchy)
        {
            obj.GetComponent<Projectile>().setDisable();
            ObjectPoolManager.Instance.DisableGameObject(obj);
        }
    }
}