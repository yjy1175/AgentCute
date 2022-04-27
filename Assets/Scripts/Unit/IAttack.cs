using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAttack : MonoBehaviour
{

    public enum SkillLaunchType
    {
        //멀티샷 공격
        MULTISHOT,
        //projectile이 1가지일경우 
        NORMAL,
        //포물선공격
        THROW
    }
    // 최종 데미지
    [SerializeField]
    private int mObjectDamage;

    // 발사체 증가 수
    [SerializeField]
    private int mProjectileCount;

    // 발사체 범위 증가량
    [SerializeField]
    private float mProjectileScale;
    public float ProjectileScale
    {
        get { return mProjectileScale; }
    }

    // 기본 경직 시간 추가량
    [SerializeField]
    private float mStiffTime;
    public float StiffTime
    {
        get { return mStiffTime; }
    }

    // 기본 공격 횟수
    [SerializeField]
    protected int mRAttackCount;

    // 기본 공격 관통 수
    [SerializeField]
    private int mPassCount;
    public int PassCount
    {
        get { return mPassCount; }
    }

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
     protected virtual void Start()
    {
        // key : 스킬 게임오브젝트 value : 각 스킬의 발사체 오브젝트
        gameObject.GetComponent<IEventHandler>().registerAttackSpeedObserver(RegisterAttackSpeedObserver);
        gameObject.GetComponent<IEventHandler>().registerAttackPointObserver(RegisterAttackPointObserver);
        gameObject.GetComponent<IEventHandler>().registerProjectileCountObserver(RegisterProjectileCountObserver);
        gameObject.GetComponent<IEventHandler>().registerProjectileScaleObserver(RegisterProjectileScaleObserver);
        gameObject.GetComponent<IEventHandler>().registerStiffTimeObserver(RegisterStiffTimeObserver);
        gameObject.GetComponent<IEventHandler>().registerRAttackCountObserver(RegisterRAttackCountObserver);
        gameObject.GetComponent<IEventHandler>().registerPassCountObserver(RegisterPassCountObserver);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void RegisterAttackSpeedObserver(float _attackSpeed, GameObject _obj)
    {
        mAutoAttackSpeed = _attackSpeed;
    }

    private void RegisterAttackPointObserver(int _attackPoint, GameObject _obj)
    {
        mObjectDamage = _attackPoint;
    }
    private void RegisterProjectileCountObserver(int _count, GameObject _obj)
    {
        mProjectileCount = _count;
    }
    private void RegisterProjectileScaleObserver(float _scale, GameObject _obj)
    {
        mProjectileScale= _scale;
    }
    private void RegisterStiffTimeObserver(float _time, GameObject _obj)
    {
        mStiffTime = _time;
    }
    private void RegisterRAttackCountObserver(int _count, GameObject _obj)
    {
        mRAttackCount = _count;
    }
    private void RegisterPassCountObserver(int _count, GameObject _obj)
    {
        mPassCount = _count;
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
        // 기본공격만 개수 증가
        int launchCount;
        if (mSkill.Spec.Type == "B")
            launchCount = TileDict[mSkill][mProjectileIndex].Spec.Count + mProjectileCount;
        else
            launchCount = TileDict[mSkill][mProjectileIndex].Spec.Count;
        int angle = TileDict[mSkill][mProjectileIndex].Spec.Angle;
        // 각도가 없을 경우 한발만 발사
        for (int i = 0; i < launchCount; i++)
        {
            LaunchCorutines(
                (launchCount == 1 ? 0 : -((launchCount - 1) * angle / 2) + angle * i),
                TileDict[mSkill][mProjectileIndex].gameObject.name,
                mTargetPos,
                mFirePos, mNotSingle);
        }
    }

    /*
    * _angle : 추가 각도 설정입니다.
    * _name : 해당 발사체오브젝트의 name입니다.
    */
    protected void LaunchCorutines(float _angle, string _name, Vector3 _targetPos, Vector3 _firePos, bool _notSingle)
    {
        GameObject obj = ObjectPoolManager.Instance.EnableGameObject(_name);
        if (_notSingle) firstProjectile = obj;
        float keepTime = obj.GetComponent<Projectile>().Spec.SpawnTime;
        Vector3 size = new Vector3(
            obj.GetComponent<Projectile>().Spec.ProjectileSizeX * (1 + mProjectileScale),
            obj.GetComponent<Projectile>().Spec.ProjectileSizeY * (1 + mProjectileScale),
            1);
        obj.GetComponent<Projectile>().setSize(size);
        setProjectileData(ref obj);
        obj.GetComponent<Projectile>().CurrentPassCount = 0;
        obj.GetComponent<Projectile>().setEnable(_targetPos, _firePos, _angle);
        obj.GetComponent<Projectile>().setDisableWaitForTime(keepTime);
    }


    protected IEnumerator multiLuanch(Skill _skill, int _count, Vector3 _target, Vector3 _fire)
    {
        for (int i = 0; i < _count; i++)
        {
            // 기본공격의 경우 추가되었을때, 예전 방향에서 나가는 버그를 수정하기위해
            // 여기서 매 발사시, firePosition을 갱신
            _fire = firePosition.transform.position;
            launchProjectile(_skill, 0, _target, _fire, false);
            yield return new WaitForSeconds(_skill.Spec.SkillCountTime);
        }
    }

    // 발사체 데미지를 설정합니다.
    private void setProjectileData(ref GameObject obj)
    {
        // 발사체가 발사 될때 마다 데미지 설정하는 api호출(크리티컬 여부 반환)
        obj.GetComponent<Projectile>().IsCriticalDamage = GetComponent<IStatus>().AttackPointSetting(gameObject);
        obj.GetComponent<Projectile>().Damage = 
            (int)(mObjectDamage * obj.GetComponent<Projectile>().Spec.ProjectileDamage);
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


    protected void FireSkillLaunchType(SkillLaunchType _enum, Skill _skill, int _count, Vector3 _target, Vector3 _fire, bool _notSingle)
    {
        switch (_enum)
        {
            case SkillLaunchType.MULTISHOT:
                StartCoroutine(multiLuanch(_skill, _count, _target, _fire));
                break;
            case SkillLaunchType.NORMAL:
                launchProjectile(_skill, 0, _target, _fire, false);
                break;
            case SkillLaunchType.THROW:
                launchProjectile(_skill, 0, _target, _fire, false);
                break;
            default:
                Debug.Log("잘못된 Enum타입 " + _enum.ToString() + "이 들어왔습니다");
                break;
        }
    }
}
