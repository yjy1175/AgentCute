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
        gameObject.GetComponent<MonsterEventHandler>().registerIsDieObserver(RegisterBossIsDieObserver);
    }

    public void RegisterBossIsDieObserver(bool _die, GameObject _obj)
    {
        if (_die)
        {
            Debug.Log("∫∏Ω∫∏ÛΩ∫≈Õ(µÂ∑°∞Ô) ªÁ∏¡");
            UIManager.Instance.OpenSecondEndingPannel();
        }
    }
}
