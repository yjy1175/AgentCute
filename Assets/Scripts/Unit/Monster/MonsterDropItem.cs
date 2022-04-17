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
                int itemId = int.Parse(itemStr);
                randSum += ItemManager.Instance.GetItemData(itemId).dropRate;
                if (randNum <= randSum)
                {
                    GameObject itemObj = ObjectPoolManager.Instance.EnableGameObject(ItemManager.Instance.GetItemData(itemId).itemInName);
                    setItemData(ref itemObj, itemId);
                    itemObj.transform.position = gameObject.transform.position;
//                    itemObj.GetComponent<Item>.
                    itemObj.SetActive(true);
                    //그거에 맞는 객체 생성
                    break;
                }
                
            }

        }
    }

    private void setItemData(ref GameObject _item, int _id)
    {
        _item.GetComponent<Item>().Hp = ItemManager.Instance.GetItemData(_id).hp;
        _item.GetComponent<Item>().Gold = ItemManager.Instance.GetItemData(_id).gold;

        float scale = ItemManager.Instance.GetItemData(_id).Scale;
        _item.GetComponent<Transform>().localScale = new Vector3(scale, scale, scale);
    }
}
