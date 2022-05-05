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
        if (collision.gameObject.CompareTag("Player") && gameObject.activeInHierarchy)
        {
            ObjectPoolManager.Instance.DisableGameObject(gameObject);
            if (Gold != 0) { 
                collision.gameObject.GetComponent<PlayerStatus>().Gold += Gold;
            }
        }
    }
}
