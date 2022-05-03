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
            GetComponent<MonsterMove>().Moveable = true;

            StartCoroutine(MonsterDie(_obj));
            //ObjectPoolManager.Instance.DisableGameObject(gameObject);
        }
        
        
        base.ChangeHp(_hp, _obj);
    }


    IEnumerator MonsterDie(GameObject _obj)
    {
        transform.GetComponent<Animator>().SetTrigger("Die");
        _obj.GetComponent<IMove>().Moveable = false;
        _obj.GetComponent<BoxCollider2D>().isTrigger = true;

        for (int i = 10; i >= 0; i--) {
            float transparency = i / 10f;
            Color monsterColor = _obj.GetComponent<SpriteRenderer>().color;
            monsterColor.a = transparency;
            _obj.GetComponent<SpriteRenderer>().color = monsterColor;
            yield return new WaitForSeconds(0.15f);
        }

        ObjectPoolManager.Instance.DisableGameObject(_obj);
    }
}
