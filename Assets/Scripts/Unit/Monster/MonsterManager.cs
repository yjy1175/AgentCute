using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public string monsterGrade;
        public int monsterPhysicalDefense;
        public int monsterMagicDefense;
        public int monsterSpeed;
        public int closeAttackPower;
        public int closeAttackRange;
        public int standoffAttackPower;
        public int standoffAttackRange;
        //public int MonsterAI;
        public int monsterExp;

        //public int MonsterDrop1;
        //public int MonsterDrop2;
        //public int MonsterDrop3;
    }
    public List<MonsterData> dataSet;
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
        GameObject[] AllEnemy = GameObject.FindGameObjectsWithTag("Enemy");
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
        dataSet = new List<MonsterData>();
        List<Dictionary<string, object>> spawnData = CSVReader.Read("CSVFile\\MonsterData");
        for (int idx = 0; idx < spawnData.Count; idx++)
        {

            MonsterData md = new MonsterData();
            md.id = (int)spawnData[idx]["ID"];
            md.monsterSpawnMap = spawnData[idx]["MonsterSpawnMap"].ToString();
            md.monsterInName = spawnData[idx]["MonsterInName"].ToString();
            md.monsterSize = (int)spawnData[idx]["MonsterSize"];
            md.monsterName = spawnData[idx]["MonsterName"].ToString();
            md.monsterGrade = spawnData[idx]["MonsterGrade"].ToString();
            md.monsterPhysicalDefense = (int)spawnData[idx]["MonsterPhysicalDefense"];
            md.monsterMagicDefense = (int)spawnData[idx]["MonsterMagicDefense"];
            md.monsterSpeed = (int)spawnData[idx]["MonsterSpeed"];
            md.closeAttackPower = (int)spawnData[idx]["CloseAttackPower"];
            md.closeAttackRange = (int)spawnData[idx]["CloseAttackRange"];
            md.standoffAttackPower = (int)spawnData[idx]["StandoffAttackPower"];
            md.standoffAttackRange = (int)spawnData[idx]["StandoffAttackRange"];
            md.monsterExp = (int)spawnData[idx]["MonsterExp"];
            //ds.CloseAttackAnimation = (int)spawnData[idx]["CloseAttackAnimation"];
            //ds.StandoffAttackAnimation = (int)spawnData[idx]["StandoffAttackAnimation"];
            //ds.HitAnimation = (int)spawnData[idx]["HitAnimation"];
            //ds.MonsterAI = (int)spawnData[idx]["MonsterAI"];
            //ds.spawnStartTime_2 = (int)spawnData[idx]["ChaseAnimation"];
            //ds.spawnStartTime_2 = (int)spawnData[idx]["DeathAnimation"];
            //ds.MonsterDrop1 = (int)spawnData[idx]["MonsterDrop1"];
            //ds.MonsterDrop2 = (int)spawnData[idx]["MonsterDrop2"];
            //ds.MonsterDrop3 = (int)spawnData[idx]["MonsterDrop3"];
            dataSet.Add(md);
        }
    }


}
