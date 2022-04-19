using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MonsterManager : SingleToneMaker<MonsterManager>
{
 
    //TO-DO : 어떤식으로 관리할지는 고민 class로 빼야 다른쪽에서도 객체 생성하기 용이할거로 보임 아님 
    public struct MonsterData
    {
        public int id;
        public string monsterSpawnMap;
        public string monsterInName;
        public int monsterSize;
        public string monsterName;
        public MonsterGrade monsterGrade;
        public int monsterHp;
        public int monsterPhysicalDefense;
        public int monsterMagicDefense;
        public int monsterSpeed;
        public int closeAttackPower;
        public int closeAttackRange;
        public int standoffAttackPower;
        public int standoffAttackRange;
        public string dropItem;
        //public int MonsterAI;
        //public int MonsterDrop1;
        //public int MonsterDrop2;
        //public int MonsterDrop3;
    }
    public enum MonsterGrade
    {
        Normal,
        Boss
    }


    [SerializeField]
    private Dictionary<string, MonsterData> dataSet;

    private void Awake()
    {
        InitAllSpawnData();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    /*
     * Pos를 인자로 넣어주면 Ingame에 있는 가장 가까운 Enemy Tag를 가진 적의 Position을 Return 시켜준다.
     */
    public Vector3 GetNearestMonsterPos(Vector3 Pos)
    {
        Vector3 dir = new Vector3(0, 0, 0);
        GameObject[] AllEnemy = GameObject.FindGameObjectsWithTag("Monster");
        float diff = 99999999;

        foreach (GameObject enemy in AllEnemy)
        {
            float tempDiff = Mathf.Abs(enemy.transform.position.x - Pos.x) + Mathf.Abs(enemy.transform.position.y - Pos.y);
            if (tempDiff < diff)
            {
                diff = tempDiff;
                dir = enemy.transform.position;
                //dir.x = enemy.transform.position.x - Pos.x;
                //dir.y = enemy.transform.position.y - Pos.y;
            }
        }
        return dir;
    }



    /*
     * 몬스터 string을 str로 넣어주면 몬스터 data를 return 해준다.
     */
    
    public MonsterData GetMonsterData(string str)
    {
        /*
         * TO-DO 없는 key값에 대한 예외처리 필요
         * */
        return dataSet[str];
    }

    /*
     * 현재 맵에 따른 몬스터 List를 return 해준다. 
     */
    public List<GameObject> GetMonsterList()
    {
        List<GameObject> temp = new List<GameObject>();

        return temp;
    }

    /*
     * CSVFile\\MonsterData의 data를 읽어서 dataSet을 만듬
     */
    private void InitAllSpawnData()
    {
        dataSet = new Dictionary<string, MonsterData>();
        List<Dictionary<string, object>> monsterDataCsv = CSVReader.Read("CSVFile\\MonsterData");
        for (int idx = 0; idx < monsterDataCsv.Count; idx++)
        {
            string key;
            MonsterData md = new MonsterData();
            key = monsterDataCsv[idx]["MonsterInName"].ToString();

            md.id = int.Parse(monsterDataCsv[idx]["ID"].ToString());
            md.monsterSpawnMap = monsterDataCsv[idx]["MonsterSpawnMap"].ToString();
            md.monsterSize = int.Parse(monsterDataCsv[idx]["MonsterSize"].ToString());
            md.monsterName = monsterDataCsv[idx]["MonsterName"].ToString();
            md.monsterGrade = (MonsterGrade)Enum.Parse(typeof(MonsterGrade), monsterDataCsv[idx]["MonsterGrade"].ToString());
            md.monsterHp = int.Parse(monsterDataCsv[idx]["MonsterHp"].ToString());
            md.monsterPhysicalDefense = int.Parse(monsterDataCsv[idx]["MonsterPhysicalDefense"].ToString());
            md.monsterMagicDefense = int.Parse(monsterDataCsv[idx]["MonsterMagicDefense"].ToString());
            md.monsterSpeed = int.Parse(monsterDataCsv[idx]["MonsterSpeed"].ToString());
            md.closeAttackPower = int.Parse(monsterDataCsv[idx]["CloseAttackPower"].ToString());
            md.closeAttackRange = int.Parse(monsterDataCsv[idx]["CloseAttackRange"].ToString());
            md.standoffAttackPower = int.Parse(monsterDataCsv[idx]["StandoffAttackPower"].ToString());
            md.standoffAttackRange = int.Parse(monsterDataCsv[idx]["StandoffAttackRange"].ToString());
            md.dropItem = monsterDataCsv[idx]["DropItem"].ToString();
            dataSet[key] = md;
        }
    }


}
