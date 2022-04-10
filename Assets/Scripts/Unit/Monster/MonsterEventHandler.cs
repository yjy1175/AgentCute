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
        if (_hp <= 0)
        {
            //TO-DO IEventHandler로 event화 시켜놓을것
            SpawnManager.currentKillMosterCount++;
            ObjectPoolManager.Instance.DisableGameObject(gameObject);
        }
        
        
        base.ChangeHp(_hp, _obj);
    }
}
