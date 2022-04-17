using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDropItem : MonoBehaviour
{
    private bool mIsDropItem;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        gameObject.GetComponent<MonsterEventHandler>().registerHpObserver(registerMonsterHp);
        mIsDropItem = false;
    }

    public void registerMonsterHp(int _hp, GameObject _obj)
    {
        if (_hp <= 0 && !mIsDropItem)
        {
            mIsDropItem = true;
            string[] itemList = MonsterManager.Instance.GetMonsterData(gameObject.name).dropItem.Split('/');
            int rateTotalSum = 0;
            int randSum = 0;
            int randNum;
            foreach (string item in itemList)
            {
                int num = int.Parse(item);
                rateTotalSum += ItemManager.Instance.GetItemData(num).dropRate;
            }
            randNum = Random.Range(0, rateTotalSum);
            
            foreach (string itemStr in itemList)
            {
                int num = int.Parse(itemStr);
                randSum += ItemManager.Instance.GetItemData(num).dropRate;
                if (randNum <= randSum)
                {
                    GameObject itemObj = ObjectPoolManager.Instance.EnableGameObject(ItemManager.Instance.GetItemData(num).itemInName);
                    itemObj.transform.position = gameObject.transform.position;
                    itemObj.SetActive(true);
                    //그거에 맞는 객체 생성
                    break;
                }
                
            }

        }
    }
}
