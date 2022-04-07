using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStatus : IStatus
{

    public Text hpUI;


    // Start is called before the first frame update
    void Start()
    {
        //TO-DO : 임시로 체력은 100으로 세팅. 기획 어케할지 확인 및 문의 필요
        mHp = 100;
        hpUI.text = "User HP : " + Hp;
    }

    // Update is called once per frame
    void Update()
    {

        /*
         * TO-DO : HP가 달경우는 Delegate를 통해 나중에 만들어질 UIManager로 보내서 체력상태를 보낼예정
         */
        hpUI.text = "User HP : " + mHp;
        
        if (Input.GetKey(KeyCode.Z))
        {
            StartCoroutine(InvincibilityCorutine(3f));
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
