using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : IAttack
{
    protected int mCloseAttackPower;
    protected int mCloseAttackRange;
    protected int mStandoffAttackPower;
    protected int mStandoffAttackRange;

    void Awake()
    {
        //TO-DO 모든 몬스터는 공속이 다 1f?
        mAutoAttackSpeed = 1f;
        mAutoAttackCheckTime = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        /*
         * TO-DO montser attack data set들어오면 구현완료할 예정 
        if(gameObject.GetComponent<MonsterStatus>().MonsterGrade == MonsterManager.MonsterGrade.Boss) {
            TileDict = new SkillDic();
            CurrentBaseSkill = SkillManager.Instance.FindBaseSkill("ms").GetComponent<Skill>();
            pushProjectile(CurrentBaseSkill);
            createObjectPool();
        }
        */
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
         * TO-DO montser attack data set들어오면 구현완료할 예정 
        if (gameObject.GetComponent<MonsterStatus>().MonsterGrade == MonsterManager.MonsterGrade.Boss)
        {
            launchProjectile(
                       CurrentBaseSkill,
                       0,
                       GameObject.Find("PlayerObject").transform.position,
                       gameObject.transform.position,
                       false);
        }
        */
        mAutoAttackCheckTime = Mathf.Max(mAutoAttackCheckTime - Time.deltaTime, 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (mAutoAttackCheckTime.Equals(0f))
            {
                collision.gameObject.GetComponent<PlayerStatus>().DamageHp = mCloseAttackPower;
                mAutoAttackCheckTime = mAutoAttackSpeed;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {

    }


    public int CloseAttackPower {
        get { return mCloseAttackPower;}
        set { mCloseAttackPower = value;}
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
