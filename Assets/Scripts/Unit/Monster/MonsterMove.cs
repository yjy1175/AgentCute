using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : IMove
{
    // Start is called before the first frame update
    void Start()
    {
        /*
         * TO-DO : MonsterManager로부터 읽어오도록 수정 필요
        */
        mSpeed = 1f;
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
        GameObject target = GameObject.Find("UnitRoot");
        if (target != null)
        {

            mDir = target.transform.position - transform.position;
            mDir.Normalize();
        }
        transform.position += mDir * mSpeed * Time.deltaTime;
    }
}
