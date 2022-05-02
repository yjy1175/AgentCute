using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStatus : IStatus
{
    [SerializeField]
    private int mPlayerExp;
    [SerializeField]
    private int mPlayerMaxExp;
    [SerializeField]
    private int mPlayerGetExp;

    [SerializeField]
    private int mPlayerLevel;

    [SerializeField]
    private int mPlayerMaxLevel;

    [SerializeField]
    private int mGold;
    [SerializeField]
    private int mGainGold;
    public int GainGold
    {
        get { return mGainGold; }
    }

    [SerializeField]
    private int mDiamond;
    public int Diamond
    {
        get { return mDiamond; }
        set { mDiamond = value; }
    }

    [SerializeField]
    private bool mIsFirstDie = true;
    public bool IsFirstDie
    {
        get { return mIsFirstDie; }
        set { mIsFirstDie = value; }
    }

    protected override void Start()
    {
        base.Start();
        PlayerLevel = 1;  
        PlayerMaxExp = PlayerManager.Instance.mLevelData[mPlayerLevel];

        mPlayerMaxLevel = PlayerManager.Instance.mLevelData.Count;

        PlayerExp = 0;

    }

    void Update()
    {

    }


    public override bool AttackPointSetting(GameObject _obj)
    {
        // 추후에 무기별 계수를 데이터로 받아와서 조정
        float ran = UnityEngine.Random.Range(0.8f, 1.2f);
        // 크리티컬 확률에 따른 랜덤뽑기
        int criRan = UnityEngine.Random.Range(0, 100);

        // 크리티컬 공격
        if (criRan < (mCriticalChance + mAddCriticalChance) * 100)
        {
            mObjectDamage = (int)(((mBaseDamage) * (ran + mAddAttackPoint)) * mCriticalDamage);
            GetComponent<IEventHandler>().ChangeAttackPoint(mObjectDamage, gameObject);
            return true;
        }
        // 기본 공격
        else
        {
            mObjectDamage = (int)((mBaseDamage) * (ran + mAddAttackPoint));
            GetComponent<IEventHandler>().ChangeAttackPoint(mObjectDamage, gameObject);
            return false;
        }

    }

    public void registerMonsterHp(int _hp, GameObject _obj)
    {
        if (_hp <= 0 && !_obj.GetComponent<MonsterStatus>().mIsDieToGetExp)
        {
            _obj.GetComponent<MonsterStatus>().mIsDieToGetExp = true;
            MonsterManager.MonsterGrade md = _obj.GetComponent<MonsterStatus>().MonsterGrade;
            PlayerGetExp = GetMonsterExp(md);
        }
    }

    public void Resurrection()
    {
        Hp = MaxHP;
        IsFirstDie = false;
    }

    public int PlayerGetExp
    {
        set
        {
            if(PlayerLevel < mPlayerMaxLevel) { 
                mPlayerGetExp = value;
                PlayerExp += mPlayerGetExp;
                while (PlayerExp >= PlayerMaxExp && PlayerLevel < mPlayerMaxLevel)
                {
                    //TO-DO LevelUp effect는?
                    PlayerExp -= PlayerMaxExp;
                    PlayerMaxExp = PlayerManager.Instance.mLevelData[PlayerLevel + 1];
                    PlayerLevel += 1;         
                }

                if (PlayerLevel >= mPlayerMaxLevel)
                {
                    PlayerMaxExp = 0;
                    PlayerExp = 0;
                }

            }
        }
        get
        {
            return mPlayerExp;
        }
    }
    public int PlayerMaxExp
    {
        set
        {
            mPlayerMaxExp = value;
        }
        get
        {
            return mPlayerMaxExp;
        }
    }
    public int PlayerExp
    {
        set
        {
            mPlayerExp = value;
            gameObject.GetComponent<PlayerEventHandler>().ChangeExp(mPlayerExp);
        }
        get
        {
            return mPlayerExp;
        }
    }

    public int PlayerLevel
    {
        set
        {
            mPlayerLevel = value;
            gameObject.GetComponent<PlayerEventHandler>().ChangeLevel(mPlayerLevel);
        }
        get
        {
            return mPlayerLevel;
        }
    }

    private int GetMonsterExp(MonsterManager.MonsterGrade _md)
    {
        if (_md == MonsterManager.MonsterGrade.Boss)
        {
            return (int)(1 * mPlayerMaxExp);
        }
        else if (_md == MonsterManager.MonsterGrade.Normal || _md == MonsterManager.MonsterGrade.Range)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    public int Gold{
        set
        {
            mGold = value;
            mGainGold += mGold;
            MessageBoxManager.Instance.createMessageBox(MessageBoxManager.BoxType.PlayerCoin, value.ToString() + "gold", gameObject.transform.position);
            gameObject.GetComponent<PlayerEventHandler>().ChangeGold(mGainGold);
            mGold = 0;
        }
        get
        {
            return mGold;
        }
    }
}