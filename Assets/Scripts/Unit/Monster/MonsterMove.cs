using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : IMove
{
    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        /*
         * TO-DO : MonsterManager로부터 읽어오도록 수정 필요
        */
    }

    // Update is called once per frame
    void Update()
    {
        mSpeed = GetComponent<MonsterStatus>().Speed;
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
        if(target != null)
            transform.Translate(mDir * mSpeed * Time.deltaTime);
    }
}
