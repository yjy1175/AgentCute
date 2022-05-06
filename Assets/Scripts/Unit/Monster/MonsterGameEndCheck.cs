using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGameEndCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        PlayerManager.Instance.Player.GetComponent<PlayerEventHandler>().registerHpObserver(RegisterHpObserver);
        gameObject.GetComponent<MonsterEventHandler>().registerHpObserver(RegisterBossHp);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void RegisterHpObserver(int _hp, GameObject _obj)
    {
        if (_hp <= 0)
        {
            //TO-DO
            Debug.Log("player »ç¸Á");

        }

    }

    public void RegisterBossHp(int _hp, GameObject _obj)
    {
        if (_hp <= 0)
        {
            Debug.Log("º¸½º¸ó½ºÅÍ »ç¸Á");
        }
    }
}
