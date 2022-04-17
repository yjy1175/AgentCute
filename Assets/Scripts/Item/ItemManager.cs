using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : SingleToneMaker<ItemManager>
{
    public GameObject[] ItemList;
    public struct ItemData
    {
        public string itemInName;
        public int hp;
        public int gold;
        public int dropRate;
        public int damage;
        public int Scale;
    }
    [SerializeField]
    private List<ItemData> dataSet;

    

    private void Awake()
    {
        InitItemData();
    }

    // Start is called before the first frame update
    void Start()
    {
        createObjectPool();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public ItemData GetItemData(int num)
    {
        /*
         * TO-DO 없는 key값에 대한 예외처리 필요
         * */
        return dataSet[num];
    }

    private void InitItemData()
    {
        dataSet = new List<ItemData>();
        List<Dictionary<string, object>> itemDataCsv = CSVReader.Read("CSVFile\\ItemData");
        for (int idx = 0; idx < itemDataCsv.Count; idx++)
        {
            int key;
            ItemData md = new ItemData();
            key = int.Parse(itemDataCsv[idx]["ID"].ToString());
            md.itemInName = itemDataCsv[idx]["ItemInName"].ToString();
            md.hp = int.Parse(itemDataCsv[idx]["Hp"].ToString());
           // md.damage = int.Parse(itemDataCsv[idx]["Damage"].ToString());
            md.gold = int.Parse(itemDataCsv[idx]["Gold"].ToString());
            md.dropRate = int.Parse(itemDataCsv[idx]["Droprate"].ToString());
            md.Scale = int.Parse(itemDataCsv[idx]["Scale"].ToString());
            dataSet.Add(md);
        }
    }

    private void createObjectPool()
    {
        foreach (GameObject item in ItemList)
        {
            ObjectPoolManager.Instance.CreateDictTable(item, 3, 3);
        }
    }
}
