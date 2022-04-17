using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : SingleToneMaker<ItemManager>
{
    public GameObject[] ItemList;
    public struct ItemData
    {
        public string itemInName;
        public int value;
        public int dropRate;
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
        for (int i = 0; i < ItemList.Length; i++)
        {
            ObjectPoolManager.Instance.CreateDictTable(ItemList[i], 3, 3);
        }
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
            md.value = int.Parse(itemDataCsv[idx]["Value"].ToString());
            md.dropRate = int.Parse(itemDataCsv[idx]["Droprate"].ToString());
            dataSet.Add(md);
        }
    }

    private void createObjectPool()
    {
        foreach(ItemData i in dataSet)
        {
            //ObjectPoolManager.Instance.CreateDictTable(i.ItemInName, 5, 5);
        }
    }
}
