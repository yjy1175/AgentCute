using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAttack : MonoBehaviour
{

    private bool DEBUG = true;

    public enum SkillLaunchType
    {
        //멀티샷 공격
        MULTISHOT,
        //projectile이 1가지일경우 
        NORMAL,
        //포물선공격
        THROW,
        //멀티 런치샷 ex 드래곤 2번스킬
        LAUNCHMULTISHOT,
        //NORMAL공격을 딜레이를 두어 발사한다.
        DELAYSHOT,
        //DELAYSHOT을 멀티샷으로 발사한다.
        DELAYMULTISHOT,
        //특정 위치로 돌진
        RUSH,
        //waring projectile생성이후 공격
        WARINGSHOT
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
    protected GameObject mFirstProjectile;


    protected SkillDic TileDict;
    public GameObject mFirePosition;

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

    [SerializeField]
    private bool mIsRush;
    [SerializeField]
    private Vector3 mRushDir;
    [SerializeField]
    private float mRushSpeed;


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

    virtual protected void  FixedUpdate()
    {

        if (mIsRush)
        {
            transform.Translate(mRushDir * mRushSpeed * Time.fixedDeltaTime);
        }

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


    public void SetTileDict(Skill _skill, List<Projectile> _projectiles)
    {
        TileDict.Add(_skill, _projectiles);
    }

    // 받아온 스킬의 발사체 리스트를 발사체매니저를 통해 받아온다
    //  <스킬, 발사체리스트> 타입의 Dic에 추가
    protected void PushProjectile(Skill _skill)
    {
        List<Projectile> newList = new List<Projectile>();
        for (int i = 0; i < _skill.Spec.getProjectiles().Count; i++)
        {
            string projectile = _skill.Spec.getProjectiles()[i];
            Projectile newProjectile = ProjectileManager.Instance.allProjectiles[projectile].GetComponent<Projectile>();
            newList.Add(newProjectile);
        }
        SetTileDict(_skill, newList);
    }



    // 실질적으로 발사 코루틴으르 호출하는 함수
    /*
    * 발사체의 개수 각도에 따른 원뿔형 발사 구현 입니다.(만약 각도가 없는 경우는 다른 타입으로 빼야됨)
    * luanchCount = 발사체의 발사 개수 + (static 변수)전체적인 발사체의 발사 개수(레벨업으로 인한)
     * angle = 발사될 발사체의 각도입니다.(즉 발사체끼리의 각도)
    * luanchCount만큼 발사가 되고, 발사될때마다 각도만큼 벌려줍니다.(원뿔형)
    * 
    * return List<gameObject> : 발사된 projectile의 Gameobject list를 반환
    */
    protected List<GameObject> launchProjectile(Skill _skill, int _projectileIndex, Vector3 _targetPos, Vector3 _firePos, bool _notSingle)
    {
        List<GameObject> objList = new List<GameObject>();
        // 기본공격만 개수 증가
        int launchCount;
        if (_skill.Spec.Type == "B")
            launchCount = TileDict[_skill][_projectileIndex].Spec.Count + mProjectileCount;
        else
            launchCount = TileDict[_skill][_projectileIndex].Spec.Count;
        int angle = TileDict[_skill][_projectileIndex].Spec.Angle;
        // 각도가 없을 경우 한발만 발사
        for (int i = 0; i < launchCount; i++)
        {
            objList.Add(
                LaunchCorutines(
                (launchCount == 1 ? 0 : -((launchCount - 1) * angle / 2) + angle * i),
                TileDict[_skill][_projectileIndex].gameObject.name,
                _targetPos,
                _firePos, _notSingle)
                );
        }
        return objList;
    }

    /*
    * _angle : 추가 각도 설정입니다.
    * _name : 해당 발사체오브젝트의 name입니다.
    * return GameObject : 발사된 projectile Gameobject를 반환
    */
    protected GameObject LaunchCorutines(float _angle, string _name, Vector3 _targetPos, Vector3 _firePos, bool _notSingle)
    {
        GameObject obj = ObjectPoolManager.Instance.EnableGameObject(_name);
        if (_notSingle) mFirstProjectile = obj;
        float keepTime = obj.GetComponent<Projectile>().Spec.SpawnTime;
        setProjectileData(ref obj);
        obj.GetComponent<Projectile>().CurrentPassCount = 0;
        obj.GetComponent<Projectile>().setEnable(_targetPos, _firePos, _angle);
        obj.GetComponent<Projectile>().setDisableWaitForTime(keepTime);
        return obj;
    }

    //프로젝타일의 ProjectileDelayTime만큼 기다렸다가 projectile이 발사된다.
    protected IEnumerator DelayLaunchProjectile(Skill _skill, int _projectileIndex, Vector3 _targetPos, Vector3 _firePos, bool _notSingle)
    {
        // 기본공격만 개수 증가
        int launchCount = TileDict[_skill][_projectileIndex].Spec.Count;
        int angle = TileDict[_skill][_projectileIndex].Spec.Angle;
        if (DEBUG)
            Debug.Log(_projectileIndex + "번째 projectile인" + TileDict[_skill][_projectileIndex].gameObject.name + "을 " + launchCount + "번, " + angle + "각도로 발사중");
        
        for (int i = 0; i < launchCount; i++)
        {
            LaunchCorutines(
                 (launchCount == 1 ? 0 : -((launchCount - 1) * angle / 2) + angle * i),
                 TileDict[_skill][_projectileIndex].gameObject.name,
                 _targetPos,
                 _firePos, _notSingle);
            yield return new WaitForSeconds(TileDict[_skill][_projectileIndex].Spec.ProjectileDelayTime);
        }
    }


    protected IEnumerator multiLuanch(Skill _skill, int _count, Vector3 _target, Vector3 _fire)
    {
        for (int i = 0; i < _count; i++)
        {
            // 기본공격의 경우 추가되었을때, 예전 방향에서 나가는 버그를 수정하기위해
            // 여기서 매 발사시, firePosition을 갱신
            _fire = mFirePosition.transform.position;
            launchProjectile(_skill, 0, _target, _fire, false);
            yield return new WaitForSeconds(_skill.Spec.SkillCountTime);
        }
    }

    //_skill의 _projectileIndex번째 스킬을 멀티샷으로 날립니다.
    //_obj의 position에서 스킬이 발사됩니다.
    protected IEnumerator multiLuanch(Skill _skill, int _projectileIndex, int _count, Vector3 _target, GameObject _obj)
    {
        for (int i = 0; i < _count; i++)
        {
            launchProjectile(_skill, _projectileIndex, _target, _obj.GetComponent<Transform>().position, false);
            yield return new WaitForSeconds(_skill.Spec.SkillCountTime);
        }
    }

    //첫번째 발사된 projectile기준으로 multiLuanch가 발사된다
    //드래곤 2번 스킬 참고
    private void LaunchInMultilaunchSkil(Skill _skill, int _count, Vector3 _target, Vector3 _fire)
    {
        //첫번째 프로젝타일은 쭈욱 발사된다.
        List<GameObject> projectileList = launchProjectile(_skill, 0, _target, _fire, true);

        //두번째 프로젝 타일은 첫번째 프로젝 타일 기준으로 multi launch를 발사한다
        //드래곤 2번스킬용으로 미사일이 1가지만 나갈경우
        //TO-DO : 다른 프로젝타일도 발사해야하는 상황이라면 for문으로 모든 에셋에 대해 추가관리 필요
        StartCoroutine(multiLuanch(_skill, 1, _count, _target, projectileList[0]));

    }

    //런치된 미사일 기준으로 멀티 런치샷이 나간다.
    //이프리트 0번 스킬 참고
    protected IEnumerator DelayMultiLuanch(Skill _skill, int _projectileIndex, int _count, Vector3 _target, Vector3 _fire)
    {
        for (int i = 0; i < _count; i++)
        {
            StartCoroutine(DelayLaunchProjectile(_skill, _projectileIndex, _target, _fire, false));
            yield return new WaitForSeconds(_skill.Spec.SkillCountTime);
        }
    }

    //_time 시간내에 gameobject의 position에서 _target으로 이동
    protected IEnumerator RushAndLuanch(Skill _skill, int _projectileIndex, Vector3 _target, float _time)
    {
        mIsRush = true;
        mRushDir = _target - transform.position;
        mRushDir.Normalize();
        mRushSpeed = _skill.Spec.SKillRushSpeed;

        if (DEBUG)
        {
            Debug.Log("mRushDir : " + mRushDir + ",거리 : " + Vector3.Distance(_target, transform.position) + ",mRushSpeed : " + mRushSpeed);
        }
        yield return new WaitForSeconds(_time);

        launchProjectile(_skill, _projectileIndex, _target, gameObject.transform.position, false);
        mIsRush = false;
    }


    //Waring projectile 이후 나머지 발사체가 순서대로 발사됨
    IEnumerator WaringAndLuanch(Skill _skill, Vector3 _target, Vector3 _fire)
    {
        GameObject obj = launchProjectile(_skill, 0, _target, _fire, true)[0];
        if (DEBUG)
        {
            Debug.Log("첫번째 waring의 위치 : " + obj.transform.position+" ,target의 위치 :"+ _target+",fireposition의 위치 :"+_fire);
        }

        for (int i = 1; i < TileDict[_skill].Count; i++)
        {
            yield return new WaitWhile(() => obj.activeInHierarchy);
            if (DEBUG)
            {
                Debug.Log(i+ "번째 스킬이 발사, target의 위치: "+ _target+", fireposition의 위치: "+ obj.transform.position);
            }
            Vector3 nextFire = obj.transform.position;
            obj = launchProjectile(_skill, i, _target, nextFire, false)[0];
            if (DEBUG)
            {
                Debug.Log("그 이후 obj의 위치"+obj.transform.position);
            }

        }
    }


    // 발사체 데미지를 설정합니다.
    private void setProjectileData(ref GameObject obj)
    {
        // 발사체가 발사 될때 마다 데미지 설정하는 api호출(크리티컬 여부 반환)
        obj.GetComponent<Projectile>().IsCriticalDamage = GetComponent<IStatus>().AttackPointSetting(gameObject);
        obj.GetComponent<Projectile>().Damage = 
            (int)(mObjectDamage * obj.GetComponent<Projectile>().Spec.ProjectileDamage);

        Vector3 size = new Vector3(
            obj.GetComponent<Projectile>().Spec.ProjectileSizeX * (1 + mProjectileScale),
            obj.GetComponent<Projectile>().Spec.ProjectileSizeY * (1 + mProjectileScale),
            1);
        obj.GetComponent<Projectile>().setSize(size);

        //caller pos 위치 저장
        obj.GetComponent<Projectile>().CallerPos = gameObject.transform.position;


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


    protected void FireSkillLaunchType(SkillLaunchType _enum, Skill _skill, int _count, Vector3 _target, Vector3 _fire, bool _notSingle, float _time)
    {
        if (DEBUG)
            Debug.Log(gameObject.name + "가 " + _enum.ToString() + "타입의 "+ _skill.name+ "을 "+_count+"번 사용합니다");
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
            case SkillLaunchType.LAUNCHMULTISHOT:
                LaunchInMultilaunchSkil(_skill, _count, _target, _fire);
                break;
            case SkillLaunchType.DELAYSHOT:
                StartCoroutine(DelayLaunchProjectile(_skill, 0, _target, _fire, _notSingle));
                break;
            case SkillLaunchType.DELAYMULTISHOT:
                StartCoroutine(DelayMultiLuanch(_skill, 0, _count, _target, _fire));
                break;
            case SkillLaunchType.RUSH:
                StartCoroutine(RushAndLuanch(_skill, 0, _target, _time));
                break;
            case SkillLaunchType.WARINGSHOT:
                StartCoroutine(WaringAndLuanch(_skill, _target, _fire));
                break;
            default:
                Debug.Log("잘못된 Enum타입 " + _enum.ToString() + "이 들어왔습니다");
                break;
        }
    }
}
