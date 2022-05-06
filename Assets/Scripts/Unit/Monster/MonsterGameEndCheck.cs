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
        PlayerManager.Instance.Player.GetComponent<PlayerEventHandler>().registerIsDieObserver(RegisterPlayerIsDieObserver);
        gameObject.GetComponent<MonsterEventHandler>().registerIsDieObserver(RegisterBossIsDieObserver);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void RegisterPlayerIsDieObserver(bool _Die, GameObject _obj)
    {
        if (_Die)
        {
            //TO-DO
            Debug.Log("player ªÁ∏¡");

        }

    }

    public void RegisterBossIsDieObserver(bool _die, GameObject _obj)
    {
        if (_die)
        {
            Debug.Log("∫∏Ω∫∏ÛΩ∫≈Õ(µÂ∑°∞Ô) ªÁ∏¡");
        }
    }
}
