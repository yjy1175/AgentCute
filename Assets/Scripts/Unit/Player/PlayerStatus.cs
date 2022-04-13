using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStatus : IStatus
{
    [SerializeField]
    private int mPlayerExp;
    [SerializeField]
    private int mPlayerExpMax;
    [SerializeField]
    private int mPlayerLevel;
    private GameObject mHpBar;
    private GameObject mExpBar;
    
    // Start is called before the first frame update
    void Start()
    {
        //TO-DO : 플레이어 스텟들 하드코딩. csv파일 받으면 수정필요.
        mHp = 100;
        mMaxHp = 100;
        mPlayerExp = 0;
        mPlayerExpMax = 100;
        mHpBar = GameObject.Find("HpBar");
        mExpBar = GameObject.Find("ExpBar");
        mPlayerLevel = 1;
    }

    // Update is called once per frame
    void Update()
    {

        /*
         * TO-DO : UI관련 클래스 만들어 그곳에서 관리하도록 수정.EventHandler를 통해 값이 업데이트 될때마다 수정하도록 변경 필요
         */
        mHpBar.transform.Find("Hp").GetComponent<Image>().fillAmount = ((float)mHp / (float)mMaxHp);
        mHpBar.transform.Find("HpText").GetComponent<Text>().text = mHp + "/" + mMaxHp;


        mExpBar.transform.Find("Exp").GetComponent<Image>().fillAmount = ((float)mPlayerExp / (float)mPlayerExpMax);
        mExpBar.transform.Find("ExpText").GetComponent<Text>().text = mPlayerExp + "/" + mPlayerExpMax;


        if (Input.GetKey(KeyCode.Z))
        {
            StartCoroutine(InvincibilityCorutine(3f));
        }


    }


    public void registerMonsterHp(int _hp, GameObject _obj)
    {
        if (_hp <= 0)
        {
            MonsterManager.MonsterGrade md = _obj.GetComponent<MonsterStatus>().MonsterGrade;
            PlayerExp = GetMonsterExp(md);
        }
    }

    public int PlayerExp
    {
        set{
            mPlayerExp += value;
            while (mPlayerExp >= mPlayerExpMax)
            {
                //TO-DO LevelUp effect는?
                mPlayerExp -= mPlayerExpMax;
                mPlayerLevel++;
                // TO-DO 레벨업 시 능력업 창 뜨게 하기
                gameObject.GetComponent<PlayerEventHandler>().ChangeLevel(mPlayerLevel);
            }
        }
        get{
            return mPlayerExp;
        }
    }



    void OnCollisionEnter2D(Collision2D collision)
    {

    }

    void OnCollisionStay2D(Collision2D collision)
    {

    }

    void OnCollisionExit2D(Collision2D collision)
    {

    }

    private int GetMonsterExp(MonsterManager.MonsterGrade _md)
    {
        if (_md == MonsterManager.MonsterGrade.Boss)
        {
            return (int)(0.7 * mPlayerExpMax);
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
