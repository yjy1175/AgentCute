using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : IMove
{
    private Animator mAnim;
    public Animator MAnim
    {
        get { return mAnim; }
        set { mAnim = value; }
    }
    [SerializeField]
    private VertualJoyStick mJoyStick;
    void Awake()
    {
        mAnim = transform.GetChild(1).GetComponent<Animator>();
        mJoyStick = GameObject.Find("Canvas").transform.Find("JoyStick").GetComponent<VertualJoyStick>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMove();
    }


    protected override void UpdateMove()
    {
        if (mMoveable)
        {
            //TO-DO : 핸드폰 키입력으로 변환 필요
            float h = mJoyStick.GetHorizontalValue();
            float v = mJoyStick.GetVerticalValue();
            if(h == 0 && v == 0)
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
        if(mMoveable)
            transform.Translate(mDir * mSpeed * Time.fixedDeltaTime);
    }

    public override void StopStiffTime(float _time)
    {
        base.StopStiffTime(_time);
        StartCoroutine(CoStiffAnimation(_time));
    }

    IEnumerator CoStiffAnimation(float _time)
    {
        mAnim.SetFloat("RunState", 1f);
        yield return new WaitForSeconds(_time);
        mAnim.SetFloat("RunState", 0f);
    }
}
