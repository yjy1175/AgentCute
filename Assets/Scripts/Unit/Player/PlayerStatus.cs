using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStatus : IStatus
{

    private int mPlayerExp;
    private int mPlayerExpMax;

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

    }

    // Update is called once per frame
    void Update()
    {

        /*
         * TO-DO : UI관련 클래스 만들어 그곳에서 관리하도록 수정.EventHandler를 통해 값이 업데이트 될때마다 수정하도록 변경 필요
         */
        mHpBar.transform.Find("Hp").GetComponent<Image>().fillAmount = ((float)mHp/ (float)mMaxHp);
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
        Debug.Log("registerMonsterHp");
        if (_hp <= 0)
        {
            MonsterManager.MonsterGrade md = _obj.GetComponent<MonsterStatus>().MonsterGrade;
            if (md == MonsterManager.MonsterGrade.Boss)
            {
                mPlayerExp += (int)(0.7 * mPlayerExpMax);
            }
            else if (md == MonsterManager.MonsterGrade.Normal)
            {
                mPlayerExp += 1;
            }
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
}
