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

    [SerializeField]
    private float mMagnetPower;
    public float MagnetPower
    {
        get { return mMagnetPower; }
        set { mMagnetPower = value;
            gameObject.GetComponent<PlayerEventHandler>().ChangeMagnetPower(mMagnetPower);
        }
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

    public void registerMonsterDie(bool _die, GameObject _obj)
    {
        if (_die)
        {
            MonsterManager.MonsterGrade md = _obj.GetComponent<MonsterStatus>().MonsterGrade;
            PlayerGetExp = GetMonsterExp(md);
        }
    }

    public void Resurrection()
    {
        Hp = MaxHP;
        Invincibility(1f);
        IsFirstDie = false;
    }

    public int PlayerGetExp
    {
        set
        {
            if (PlayerLevel >= mPlayerMaxLevel)
                return;

            mPlayerGetExp = value;
            PlayerExp += mPlayerGetExp;
            StartCoroutine(LevelUpCheck());
            if (PlayerLevel >= mPlayerMaxLevel)
            {
                PlayerMaxExp = 0;
                PlayerExp = 0;
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
            gameObject.GetComponent<PlayerEventHandler>().ChangeExp();
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
            gameObject.GetComponent<PlayerEventHandler>().ChangeExp();
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
        if (_md == MonsterManager.MonsterGrade.Normal || _md == MonsterManager.MonsterGrade.Range)
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

    IEnumerator LevelUpCheck()
    {
        while (PlayerExp >= PlayerMaxExp && PlayerLevel < mPlayerMaxLevel)
        {
            PlayerExp -= PlayerMaxExp;
            PlayerLevel += 1;
            PlayerMaxExp = PlayerManager.Instance.mLevelData[PlayerLevel];
            UIManager.Instance.StatusSelectPannelOn();
            yield return new WaitWhile(() => UIManager.Instance.StatusSelectPannel.activeInHierarchy);
        }
    }

}