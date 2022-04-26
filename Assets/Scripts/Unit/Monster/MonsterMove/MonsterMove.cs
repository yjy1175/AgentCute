using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : IMove
{
    private GameObject target;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        mSpeed = gameObject.GetComponent<IStatus>().MoveSpeed;
    }

    // Update is called once per frame
    void Update()
    { 
        /*
         * TO-DO : 몬스터 무브가 다양해지면 MonsterMoveStrategy 클래스를 만들어
         *         Stragegy패턴을 적용해서 추가할것
         *         우선 Player에게 다가가는 형태만으로 구성.
        */
        MovingPattern1();
    }

    void MovingPattern1()
    {
        target = GameObject.Find("UnitRoot");
        if (target != null)
        {
            
           

            mDir = target.transform.position - transform.position;
            mDir.Normalize();
        }
    }

    private void FixedUpdate()
    {
        // 모든 물리연산은 FixedUpdate 에서
        if(target != null && mMoveable) { 
            transform.Translate(mDir * mSpeed * Time.deltaTime);
            int size = gameObject.GetComponent<IStatus>().Size;
            if(gameObject.transform.position.x < GameObject.Find("PlayerObject").transform.position.x)
            {
                gameObject.transform.localScale = new Vector3(size, size, size);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(-1*size, size, size);
            }
        }
    }
    public override void StopStiffTime(float _time)
    {
        base.StopStiffTime(_time);
    }
}
