using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    int mHp;
    [SerializeField]
    int mDamage;
    [SerializeField]
    int mExp;
    [SerializeField]
    int mGold;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
            ObjectPoolManager.Instance.DisableGameObject(gameObject);
            if(Hp!=0)
                collision.gameObject.GetComponent<PlayerStatus>().PotionHp = Hp;
            if(Gold!=0)
                collision.gameObject.GetComponent<PlayerStatus>().Gold += Gold;
        }
    }

    public int Hp
    {
        set
        {
            mHp = value;
            Debug.Log(gameObject.name + "¿¡ HpSet" + mHp);
        }
        get
        {
            return mHp;
        }
    }
    public int Gold
    {
        set
        {
            mGold = value;
        }
        get
        {
            return mGold;
        }
    }
    public int Damage
    {
        set
        {
            mDamage = value;
        }
        get
        {
            return mDamage;
        }
    }
}
