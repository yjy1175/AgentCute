using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : IMove
{
    private Animator mAnim;
    void Awake()
    {
        mAnim = transform.GetChild(0).GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        mSpeed = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMove();
    }


    protected override void UpdateMove()
    {
        //TO-DO : 핸드폰 키입력으로 변환 필요
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        
        //TO-DO : 이동방식은 어떻게 이루어지게?
        // 좌우 반전
        if(h < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (h > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        // 변화량이 있으면 애니메이션 재생
        if(h != 0 || v != 0)
        {
            mAnim.SetBool("isRun", true);
        }
        else
        {
            mAnim.SetBool("isRun", false);
        }

        mDir = new Vector3(h, v, 0);
        transform.Translate(mDir.normalized * mSpeed * Time.deltaTime);

        //TO-DO : 유저의 움직임에 따라 카메라도 따라다니도록 수정 필요
        //Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        //if (pos.x < 0f) pos.x = 0f;
        //if (pos.x > 1f) pos.x = 1f;
        //if (pos.y < 0f) pos.y = 0f;
        //if (pos.y > 1f) pos.y = 1f;
        //transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

}
