using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPlayerMove : MonoBehaviour
{
    [SerializeField]
    private float mSpeed;
    [SerializeField]
    private Vector3 mDir;
    [SerializeField]
    private bool mMoveable;
    [SerializeField]
    private Animator mAnim;
    [SerializeField]
    private VertualJoyStick mJoyStick;
    [SerializeField]
    private Text mInteractionText;

    [SerializeField]
    private bool mIsTriggerInWareHouse;
    public bool IsTriggerInWareHouse
    {
        get { return mIsTriggerInWareHouse; }
    }

    [SerializeField]
    private bool mIsTriggerInStartZone;
    public bool IsTriggerInStartZone
    {
        get { return mIsTriggerInStartZone; }
    }
    void Awake()
    {
        mMoveable = true;
        mIsTriggerInWareHouse = false;
        mIsTriggerInStartZone = false;
        mAnim = transform.GetChild(1).GetComponent<Animator>();
        mJoyStick = GameObject.Find("Canvas").transform.Find("JoyStick").GetComponent<VertualJoyStick>();
        mInteractionText = GameObject.Find("InteractionButton").transform.GetChild(0).GetChild(1).GetComponent<Text>();
        GetComponent<LobbyPlayerEventHendler>().resgisterMoveSpeedObsever(ResgisterMoveSpeedObsever);
    }

    private void ResgisterMoveSpeedObsever(float _speed)
    {
        mSpeed = _speed;
    }
    // Update is called once per frame
    void Update()
    {
        UpdateMove();
    }


    private  void UpdateMove()
    {
        if (mMoveable)
        {
            //TO-DO : 핸드폰 키입력으로 변환 필요
            float h = mJoyStick.GetHorizontalValue();
            float v = mJoyStick.GetVerticalValue();
            if (h == 0 && v == 0)
            {
                h = Input.GetAxis("Horizontal");
                v = Input.GetAxis("Vertical");
            }

            //TO-DO : 이동방식은 어떻게 이루어지게?
            // 좌우 반전
            if (h < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (h > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            // 변화량이 있으면 애니메이션 재생
            if (h != 0 || v != 0)
            {
                mAnim.SetFloat("RunState", 0.5f);
                mAnim.SetBool("Run", true);
            }
            else
            {
                mAnim.SetFloat("RunState", 0f);
                mAnim.SetBool("Run", false);
            }
            mDir = new Vector3(h, v, 0).normalized;
        }
    }

    private void FixedUpdate()
    {
        // 모든 물리연산은 FixedUpdate에서
        if (mMoveable)
            transform.Translate(mDir * mSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WareHouse"))
        {
            mIsTriggerInWareHouse = true;
            mInteractionText.text = "창고 열기";
        }
        if (collision.CompareTag("StartZone"))
        {
            mIsTriggerInStartZone = true;
            mInteractionText.text = "던전 입장";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("WareHouse"))
        {
            mIsTriggerInWareHouse = false;
            mInteractionText.text = "상호 작용";
        }
        if (collision.CompareTag("StartZone"))
        {
            mIsTriggerInStartZone = false;
            mInteractionText.text = "상호 작용";
        }
    }
}
