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

    public override void ChangeIsDie(bool _dieCheck, GameObject _obj)
    {
        if (_dieCheck)
        {
            SpawnManager.currentKillMosterCount++;
            StartCoroutine(MonsterDie(_obj));
        }
        base.ChangeIsDie(_dieCheck, _obj);
    }


    IEnumerator MonsterDie(GameObject _obj)
    {
        transform.GetComponent<Animator>().SetTrigger("Die");
        _obj.GetComponent<MonsterAttack>().enabled = false;
        _obj.GetComponent<BoxCollider2D>().enabled = false;
        if(gameObject.GetComponent<MonsterStatus>().MonsterGrade == MonsterManager.MonsterGrade.Boss)
           _obj.GetComponent<CircleCollider2D>().enabled = false;
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
