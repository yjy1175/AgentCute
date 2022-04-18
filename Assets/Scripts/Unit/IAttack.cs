using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAttack : MonoBehaviour
{
    // 예를 들어 한 스킬에 발사체 2개 이상인데
    // 첫발사체가 disable된 position에서 enable
    protected GameObject firstProjectile;


    //TO-Do value가 List<Projectile>로 변경 
    protected SkillDic TileDict;
    public GameObject firePosition;

    [SerializeField]
    protected float mAutoAttackSpeed;
    protected float mAutoAttackCheckTime;

    [SerializeField]
    private Skill currentBaseSkill;
    public Skill CurrentBaseSkill
    {
        get { return currentBaseSkill; }
        set
        {
            currentBaseSkill = value;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        // key : 스킬 게임오브젝트 value : 각 스킬의 발사체 오브젝트
    }

    // Update is called once per frame
    void Update()
    {

    }

      
    public void setTileDict(Skill _skill, List<Projectile> _projectiles)
    {
        TileDict.Add(_skill, _projectiles);
    }

    // 받아온 스킬의 발사체 리스트를 발사체매니저를 통해 받아온다
    //  <스킬, 발사체리스트> 타입의 Dic에 추가
    protected void pushProjectile(Skill _skill)
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



    // 실질적으로 발사 코루틴으르 호출하는 함수
    /*
    * 발사체의 개수 각도에 따른 원뿔형 발사 구현 입니다.(만약 각도가 없는 경우는 다른 타입으로 빼야됨)
    * luanchCount = 발사체의 발사 개수 + (static 변수)전체적인 발사체의 발사 개수(레벨업으로 인한)
     * angle = 발사될 발사체의 각도입니다.(즉 발사체끼리의 각도)
    * luanchCount만큼 발사가 되고, 발사될때마다 각도만큼 벌려줍니다.(원뿔형)
    */
    protected void launchProjectile(Skill mSkill, int mProjectileIndex, Vector3 mTargetPos, Vector3 mFirePos, bool mNotSingle)
    {
        int launchCount = TileDict[mSkill][mProjectileIndex].Spec.Count + Projectile.AddProjectilesCount;
        int angle = TileDict[mSkill][mProjectileIndex].Spec.Angle;
        // 각도가 없을 경우 한발만 발사
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

    /*
    * _angle : 추가 각도 설정입니다.
    * _name : 해당 발사체오브젝트의 name입니다.
    */
    protected IEnumerator LaunchCorutines(float _angle, string _name, Vector3 _targetPos, Vector3 _firePos, bool _notSingle)
    {
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


    protected IEnumerator multiLuanch(Skill _skill, int _count, Vector3 _target, Vector3 _fire)
    {
        for (int i = 0; i < _count; i++)
        {
            launchProjectile(_skill, 0, _target, _fire, false);
            yield return new WaitForSeconds(0.4f); // 추후에 여러번 발사일 경우 해당 데이터값 입력
        }
    }

    // 발사체 데미지를 설정합니다.
    private void setProjectileData(ref GameObject obj)
    {
        obj.GetComponent<Projectile>().Damage = 
            (int)((gameObject.GetComponent<IStatus>().BaseDamage + gameObject.GetComponent<IStatus>().getCurrentWeponeDamage()) * obj.GetComponent<Projectile>().Spec.ProjectileDamage);
    }

    protected void createObjectPool()
    {
        foreach (Skill key in TileDict.Keys)
        {
            for (int i = 0; i < TileDict[key].Count; i++)
            {
                ObjectPoolManager.Instance.CreateDictTable(TileDict[key][i].gameObject, 10, 10);
            }
        }
    }

}
