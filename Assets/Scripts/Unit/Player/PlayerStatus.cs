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
    private Costume playerCurrentCostume;



    // Start is called before the first frame update
    void Start()
    {
        //TO-DO : 플레이어 스텟들 하드코딩. csv파일 받으면 수정필요.
        Hp = 100;
        mMaxHp = 100;
        mPlayerMaxExp = 100;
        PlayerExp = 0;
        PlayerLevel = 1;
        BaseDamage = Random.Range(30, 50);
    }

    // Update is called once per frame
    void Update()
    {

        /*
         * TO-DO : UI관련 클래스 만들어 그곳에서 관리하도록 수정.EventHandler를 통해 값이 업데이트 될때마다 수정하도록 변경 필요
         */


        if (Input.GetKey(KeyCode.Z))
        {
            StartCoroutine(InvincibilityCorutine(3f));
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
        else if (_md == MonsterManager.MonsterGrade.Normal)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}