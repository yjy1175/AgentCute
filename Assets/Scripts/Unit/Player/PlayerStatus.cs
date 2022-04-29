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
    private int mGold;
    [SerializeField]
    private int mGainGold;

    [SerializeField]
    private Costume mPlayerCurrentCostume;
    public Costume MPlayerCurrentCostume
    {
        get { return mPlayerCurrentCostume; }
        // 스텟적용도 여기서?
        set { mPlayerCurrentCostume = value; }
    }



    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //TO-DO : 플레이어 스텟들 하드코딩. csv파일 받으면 수정필요.
        mMaxHp = mHp;
        mPlayerMaxExp = 100;
        PlayerExp = 0;
        PlayerLevel = 1;
    }

    // Update is called once per frame
    void Update()
    {

        /*
         * TO-DO : UI관련 클래스 만들어 그곳에서 관리하도록 수정.EventHandler를 통해 값이 업데이트 될때마다 수정하도록 변경 필요
         */


        //if (Input.GetKey(KeyCode.Z))
        //{
        //    StartCoroutine(InvincibilityCorutine(3f));
        //}


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
            mObjectDamage = (int)(((mBaseDamage + currentWeapon.Spec.WeaponDamage) * (ran + mAddAttackPoint)) * mCriticalDamage);
            GetComponent<IEventHandler>().ChangeAttackPoint(mObjectDamage, gameObject);
            return true;
        }
        // 기본 공격
        else
        {
            mObjectDamage = (int)((mBaseDamage + currentWeapon.Spec.WeaponDamage) * (ran + mAddAttackPoint));
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

    public int PlayerGetExp
    {
        set
        {
            mPlayerGetExp = value;
            PlayerExp += mPlayerGetExp;
            while (PlayerExp >= PlayerMaxExp)
            {
                //TO-DO LevelUp effect는?
                PlayerExp -= PlayerMaxExp;
                PlayerLevel += 1;
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
            mPlayerExp = value;

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
            return (int)(0.7 * mPlayerMaxExp);
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