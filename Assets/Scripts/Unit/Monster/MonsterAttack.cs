using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MonsterAttack : IAttack
{
    protected int mCloseAttackPower;
    protected int mCloseAttackRange;
    protected int mStandoffAttackPower;
    protected int mStandoffAttackRange;


    private float tempTime = 0f;
    private float tempTileMax = 2f;

    [SerializeField]
    private List<GameObject> mMonsterSkill = new List<GameObject>();

    void Awake()
    {
        //TO-DO 모든 몬스터는 공속이 다 1f?
        mAutoAttackSpeed = 1f;
        mAutoAttackCheckTime = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.GetComponent<MonsterStatus>().MonsterGrade == MonsterManager.MonsterGrade.Boss)
        {
            firePosition = transform.Find("FirePosition").gameObject;
            TileDict = new SkillDic();
            mMonsterSkill = SkillManager.Instance.FindMonsterSkill("Slime");
            for (int i = 0; i < mMonsterSkill.Count; i++)
            {
                pushProjectile(mMonsterSkill[i].GetComponent<Skill>());
            }
            //TO-DO 어디서 objectpool을 하는게 맞을지 고민. 여기서 하면 각 몬스터가 소환될때마다 create를 하는 문제가 발생한다.
            //objectpool 이 같은 object가 들어올때 재생성은 안하긴 하지만 1회만 호출하고싶은데 그런경우는 어디서 해야 옳을지 고민이필요.
            createObjectPool();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (tempTime.Equals(0f)) {
            if (gameObject.GetComponent<MonsterStatus>().MonsterGrade == MonsterManager.MonsterGrade.Boss)
            {
                //몬스터 스킬 랜덤선택 => 거리비교를 통해 사용해야할 스킨 번호를 메서드로 리턴
                MonsterManager.MonsterData md = MonsterManager.Instance.GetMonsterData(gameObject.name);
                //모션 TRIGGER를 호출 -> MonsterStatus의 모션명을 받아서 사용 
                //스킬 사용
                int skillNum = GetSkillNumber(md);
                if (skillNum != -1) { 
                    Debug.Log("랜덤스킬 넘버: "+ skillNum);
                    Skill skill = mMonsterSkill[skillNum].GetComponent<Skill>();
                    SkillLaunchType skillLaunchType =(SkillLaunchType)Enum.Parse(typeof(SkillLaunchType), skill.Spec.SkillLaunchType);
                    int count = skill.Spec.SkillCount;
                    FireSkillLaunchType(skillLaunchType, skill, count,
                            GameObject.Find("PlayerObject").transform.position, firePosition.transform.position, false);
                }
            }
            tempTime = tempTileMax;
        }
        tempTime = Mathf.Max(tempTime - Time.deltaTime, 0);
        
        mAutoAttackCheckTime = Mathf.Max(mAutoAttackCheckTime - Time.deltaTime, 0);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (mAutoAttackCheckTime.Equals(0f))
            {
                GameObject.Find("PlayerObject").GetComponent<PlayerStatus>().DamageHp = mCloseAttackPower;
                mAutoAttackCheckTime = mAutoAttackSpeed;
            }
        }
    }
    
    private int GetSkillNumber(MonsterManager.MonsterData _md)
    {
        //사용 가능한 스킬을 찾는다
        List<int> skillNumber = new List<int>();
        for (int i = 0; i < _md.skillAttackPowerRange.Count; i++)
        {
            if (Mathf.Abs(Vector3.Distance(GameObject.Find("PlayerObject").transform.position, gameObject.transform.position)) <= _md.skillAttackPowerRange[i])
            {
                skillNumber.Add(i);
            }
        }
        //스킬을 리스트에 대해 랜덤을 돌린다
        //리턴한다.
        return skillNumber.Count == 0 ? -1 : skillNumber[UnityEngine.Random.Range(0, skillNumber.Count)];
    }




    public int CloseAttackPower
    {
        get { return mCloseAttackPower; }
        set { mCloseAttackPower = value; }
    }
    public int CloseAttackRange
    {
        get { return mCloseAttackRange; }
        set { mCloseAttackRange = value; }
    }
    public int StandoffAttackPower
    {
        get { return mStandoffAttackPower; }
        set { mStandoffAttackPower = value; }
    }
    public int StandoffAttackRange
    {
        get { return mStandoffAttackRange; }
        set { mStandoffAttackRange = value; }
    }
}
