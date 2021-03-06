using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MonsterAttack : IAttack
{
    private bool DEBUG = false;
    [SerializeField]
    private int DEBUG_SKILL_NUMBER=-1;

    [SerializeField]
    protected int mCloseAttackPower;

    public int CloseAttackPower
    {
        get { return mCloseAttackPower; }
        set { mCloseAttackPower = value; }
    }


    [SerializeField]
    protected float mBerserkerModeScale;
    public float BerserkerModeScale
    {
        get { return mBerserkerModeScale; }
        set { mBerserkerModeScale = value; }
    }


    private float tempTime = 0f;
    private float tempTileMax = 3f;

    private bool mIsUsingSkill;

    [SerializeField]
    private List<GameObject> mMonsterSkill = new List<GameObject>();

    [SerializeField]
    private List<float> mSkillCheckCoolTime;

    void Awake()
    {
        //TO-DO 모든 몬스터는 공속이 다 1f?
        mAutoAttackSpeed = 1f;
        mAutoAttackCheckTime = 0f;
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        // 몬스터의 데미지 설정을 위한 override
        base.Start();

        mBerserkerModeScale = 1f;

        //쿨타임 배열 설정
        if (gameObject.GetComponent<MonsterStatus>().MonsterGrade == MonsterManager.MonsterGrade.Boss)
            mSkillCheckCoolTime = new List<float> { 0f, 0f, 0f, 0f };
        else if (gameObject.GetComponent<MonsterStatus>().MonsterGrade == MonsterManager.MonsterGrade.Range)
            mSkillCheckCoolTime = new List<float> { 0f };

        if (UseSkillCheck())
        {
            MonsterManager.MonsterData md = MonsterManager.Instance.GetMonsterData(gameObject.name, gameObject.GetComponent<MonsterStatus>().MonsterRank);


            mFirePosition = transform.Find("FirePosition").gameObject;
            TileDict = new SkillDic();
            mMonsterSkill = SkillManager.Instance.FindMonsterSkill(md.monsterType);
            for (int i = 0; i < mMonsterSkill.Count; i++)
            {
                PushProjectile(mMonsterSkill[i].GetComponent<Skill>());
            }
            //TO-DO 어디서 objectpool을 하는게 맞을지 고민. 여기서 하면 각 몬스터가 소환될때마다 create를 하는 문제가 발생한다.
            //objectpool 이 같은 object가 들어올때 재생성은 안하긴 하지만 1회만 호출하고싶은데 그런경우는 어디서 해야 옳을지 고민이필요.
            createObjectPool();
        }
    }
    protected void OnEnable()
    {
        mIsUsingSkill = false;
        tempTime = 0f;
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        UseSkil();

        mAutoAttackCheckTime = Mathf.Max(mAutoAttackCheckTime - Time.deltaTime, 0);

        for(int i = 0; i < mSkillCheckCoolTime.Count; i++)
        {
            mSkillCheckCoolTime[i] = Mathf.Max(mSkillCheckCoolTime[i] - Time.deltaTime, 0f);
        }

        base.FixedUpdate();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (mAutoAttackCheckTime.Equals(0f))
            {
                PlayerManager.Instance.Player.GetComponent<PlayerStatus>().CloseDamaged = (int)((float)mCloseAttackPower * BerserkerModeScale);
                mAutoAttackCheckTime = mAutoAttackSpeed;
            }
        }
    }

    private void UseSkil()
    {
        if (!mIsUsingSkill && tempTime.Equals(0f))
        {
            /*
             * 
             * TO-DO 
             * Boss는 스킬이 4가지, Range는 스킬이 1가지라 일단은 이부분을 attack에 반영해서 코드를 짤것임. 이부분을
             * SkillAttackPower
             * SkillAttackPowerRange
             * SkillAttackAnimation
             * 이것들을 1,2,3,4로 둔게 아니라 /로 나눠서 한다음 list를 받는 형태로해서 각각 이 값에 대해 list형태로 받아서 해야 작업이 편할것으로 보임
             * 일단 현재 형태를 다 뒤집어 엎기는 어렵기 때문이 grade에 따라 따로 작업을 하는걸로 하고 이후 수정 
             */
            if (UseSkillCheck())
            {
                //몬스터 스킬 랜덤선택 => 거리비교를 통해 사용해야할 스킨 번호를 메서드로 리턴
                int skillNum = GetSkillNumber();
                if(DEBUG)
                    Debug.Log("사용스킬num :"+ skillNum);
                //스킬 사용
                if (skillNum != -1)
                {
                    StartCoroutine(UseSkill(skillNum));
                }
            }
            tempTime = tempTileMax;
        }
        tempTime = Mathf.Max(tempTime - Time.deltaTime, 0);
    }
    
    // -1 시 사용 가능스킬이 없음
    //랜덤으로 사용사능한 스킬번호 return 
    private int GetSkillNumber()
    {
        MonsterManager.MonsterData md = MonsterManager.Instance.GetMonsterData(gameObject.name, gameObject.GetComponent<MonsterStatus>().MonsterRank);
        int skillNumber = -1;
        if (md.monsterGrade == MonsterManager.MonsterGrade.Boss) {
            int priorityValue = 99;
            for (int i = 0; i < md.skillAttackPowerRange.Count; i++)
            {
                if (Mathf.Abs(Vector3.Distance(PlayerManager.Instance.Player.transform.position, gameObject.transform.position)) <= md.skillAttackPowerRange[i]
                        && mSkillCheckCoolTime[i].Equals(0f)
                        && mMonsterSkill[i].GetComponent<Skill>().Spec.SkillPriority < priorityValue)
                {
                    priorityValue = mMonsterSkill[i].GetComponent<Skill>().Spec.SkillPriority;
                    skillNumber = i;
                }
            }
            if(skillNumber!= -1)
            {
                mSkillCheckCoolTime[skillNumber] = mMonsterSkill[skillNumber].GetComponent<Skill>().Spec.getSkillCoolTime()[0];
            }
        }
        else if(md.monsterGrade == MonsterManager.MonsterGrade.Range)
        {
            if (Mathf.Abs(Vector3.Distance(PlayerManager.Instance.Player.transform.position, gameObject.transform.position)) <= md.skillAttackPowerRange[0]
                && mSkillCheckCoolTime[0].Equals(0f))
            {
                skillNumber = 0;
            }
        }
        //inspector창에서 특정스킬 디버그시 특정스킬만 나가도록 할 시 사용
        if (DEBUG_SKILL_NUMBER != -1)
        {
            return DEBUG_SKILL_NUMBER;
        }
        return skillNumber;
    }

    private bool UseSkillCheck()
    {
        return gameObject.GetComponent<MonsterStatus>().MonsterGrade == MonsterManager.MonsterGrade.Boss
                    || gameObject.GetComponent<MonsterStatus>().MonsterGrade == MonsterManager.MonsterGrade.Range;
    }


    //_skillNum으로 등록된 스킬을 사용합니다
    //애니메이션의 event작업을 지양하기 위해서 애니메이션중 스킬이 나가야하는 모션을 코드로 관리하도록 되어있습니다
    //1. 스킬 대미지 설정   -> TO DO : projectile로 넘길수 있는 방법이 있을지 확인해봐야함 만약 맵에 이전 스킬의 영향이 남아있으면 새 스킬 BaseDamage로 설정되는 버그가 발생할 수 있음
    //2. 스킬 사용시 멈춤설정
    //3. 스킬 모션 동작.
    //4. 모션 중 스킬이 발동되더야할 시점까지 wait
    //5. 스킬발사
    //6. 스킬 동작시간 동안 멈춤
    //7. 다시 움직이는 애니메이션 동작
    private IEnumerator UseSkill(int _skillNum)
    {
        if (DEBUG)
            Debug.Log(gameObject.name + "의 " + _skillNum + "번째 스킬사용시전");
        MonsterManager.MonsterData md = MonsterManager.Instance.GetMonsterData(gameObject.name, gameObject.GetComponent<MonsterStatus>().MonsterRank);
        Skill skill = mMonsterSkill[_skillNum].GetComponent<Skill>();
        //1. 스킬 대미지 설정
        SkillLaunchType skillLaunchType = (SkillLaunchType)Enum.Parse(typeof(SkillLaunchType), skill.Spec.SkillLaunchType);
        gameObject.GetComponent<MonsterStatus>().BaseDamage = (int)((float)md.skillAttackPower[_skillNum] * BerserkerModeScale);

        //2. 스킬 사용시 멈춤설정
        gameObject.GetComponent<IMove>().Moveable = false;

        if (DEBUG)
            Debug.Log(gameObject.name + " 대기시간" + skill.Spec.SkillMotionStartTime);

        //3. 스킬 모션 동작.
        transform.GetComponent<Animator>().SetTrigger(md.skillAttackAnimation[_skillNum]);

        //4. 모션 중 스킬이 발동되더야할 시점까지 wait
        yield return new WaitForSeconds(skill.Spec.SkillMotionStartTime);

        if (DEBUG)
            Debug.Log(gameObject.name + " 스킬발사");

        //5. 스킬발사
        FireSkillLaunchType(skillLaunchType, skill, skill.Spec.SkillCount,
                            PlayerManager.Instance.Player.transform.position, mFirePosition.transform.position, false, skill.Spec.SkillMotionRemainTime);
        mIsUsingSkill = true;

        if (DEBUG)
            Debug.Log(gameObject.name + " 스킬동작동안 멈춤시간 :" + skill.Spec.SkillMotionRemainTime);


        //6. 스킬 동작시간동안 멈춤
        yield return new WaitForSeconds(skill.Spec.SkillMotionRemainTime);

        if (DEBUG)
            Debug.Log(gameObject.name + " Walk로 전환");

        //7. 다시 움직이는 애니메이션 동작
        transform.GetComponent<Animator>().SetTrigger("Walk");

        //8. 딜타임
        yield return new WaitForSeconds(skill.Spec.SkillStopTime);

        gameObject.GetComponent<IMove>().Moveable = true;
        mIsUsingSkill = false;
    }

}
