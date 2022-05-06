using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEventHandler : IEventHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ChangeHp(int _hp, GameObject _obj)
    {
        if (_hp <= 0 && !GetComponent<MonsterStatus>().mIsDieToKillCount)
        {
            GetComponent<MonsterStatus>().mIsDieToKillCount = true;
            //TO-DO IEventHandler로 event화 시켜놓을것
            SpawnManager.currentKillMosterCount++;            
            StartCoroutine(MonsterDie(_obj));

        }


        base.ChangeHp(_hp, _obj);
    }

    IEnumerator MonsterDie(GameObject _obj)
    {
        transform.GetComponent<Animator>().SetTrigger("Die");
        _obj.GetComponent<MonsterMove>().IsDie = true;
        _obj.GetComponent<MonsterAttack>().enabled = false;
        _obj.GetComponent<BoxCollider2D>().enabled = false;

        //TO-DO 몬스터가 죽어도 UseSkill에서 Moveable를 walk로 바꾸면서 true로 움직이는 부분이 있어
        //스킬사용 후 사망시 몬스터가 움직이는 버그가 발생.  handler쪽에서 코루틴을 중지시켜줬지만 MonsterAttack에서 
        //매번 hpeventhandler구독하기는 요소가 큰것같아 사망 handler를 만든이후 MonsterAttack에서 stop하도록 수정필요
        _obj.GetComponent<MonsterAttack>().StopAllCoroutines();


        for (int i = 10; i >= 0; i--)
        {
            float transparency = i / 10f;
            Color monsterColor = _obj.GetComponent<SpriteRenderer>().color;
            monsterColor.a = transparency;
            _obj.GetComponent<SpriteRenderer>().color = monsterColor;
            yield return new WaitForSeconds(0.15f);
        }

        ObjectPoolManager.Instance.DisableGameObject(_obj);
    }
}
