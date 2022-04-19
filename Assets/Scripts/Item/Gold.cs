using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : Item
{
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
        Debug.Log("트리거는되니?");

        if (collision.gameObject.CompareTag("Player") && gameObject.activeInHierarchy)
        {
            Debug.Log("여기들어와?");
            gameObject.SetActive(false);
            ObjectPoolManager.Instance.DisableGameObject(gameObject);
            if (Gold != 0) { 
                Debug.Log("gold 얼마? : "+Gold);
                Debug.Log("충돌오브젝 이름: "+collision.gameObject.name);
                collision.gameObject.GetComponent<PlayerStatus>().Gold += Gold;
            }
        }
    }
}
