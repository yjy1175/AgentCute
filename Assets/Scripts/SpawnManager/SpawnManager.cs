using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpawnManager : SingleToneMaker<SpawnManager>
{
    public struct SpawnData
    {
        public int Id;
        public string spawnMonster;
        public string spawnMap;
        public int spawnOrder;
        public int spawnNumber;
        public float spawnStartTime_1;
        public float spawnStartTime_2;
        public float realStartSpawnTime; //TO-DO : 매 게임이 시작되면 이부분은 reset할수 있도록 처리 필요
        public float spawnTime;
        public float currentTime;
    }
    /*
     * TO-DO Spawn_Map은 Enum으로 해서 관리 뭐뭐나올지 모르니 일단 skip
     */

    public List<SpawnData> dataSet;
    private float currentTime = 0f;

    public Transform[] spawnPoints;

    public GameObject[] TempMonster;

    void Awake()
    {
        initAllSpawnData();
        /*
         * TO-DO: 
         * 1. spawn point는 Awake에서 오브젝트를 찾아 읽어오는 방식
         * 2. MonsterManager로부터 몬스터 형태 받아오는 API 지금은 임시로 MonsterA에 오브젝트 넣어둠
         * 3. 몬스터별 스폰은 어떻게 구성하는게 좋을지 고민이 필요. 규칙성 -> 기획분들에게 문의가 필요한부분 -> 일단은 그냥 구현
         * 
         * */
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0f;
        for (int i = 0; i < TempMonster.Length; i++)
        {
            ObjectPoolManager.Instance.CreateDictTable(TempMonster[i], 5, 5);
        }
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < dataSet.Count; i++)
        {
            SpawnData temp = dataSet[i];
            if (dataSet[i].realStartSpawnTime < currentTime && dataSet[i].currentTime > dataSet[i].spawnTime)
            {
                for (int j = 0; j < dataSet[i].spawnNumber; j++)
                {
                    GameObject enemy = ObjectPoolManager.Instance.EnableGameObject(dataSet[i].spawnMonster);
                    if (enemy != null)
                    {
                        int index = UnityEngine.Random.Range(0, spawnPoints.Length);
                        enemy.transform.position = spawnPoints[index].position;
                        enemy.SetActive(true);
                    }
                }
                temp.currentTime = 0;
            }
            /*
             * TO-DO : C#의 특정상 구조체를 구조체의 값에만 접근할수 없어서 복사형태로 짜놈. 나중에 curretTime관련 배열만들어서 사용하는게 날듯
             */
            currentTime += Time.deltaTime;
            temp.currentTime += Time.deltaTime;
            dataSet[i] = temp;
        }
    }

    private void initAllSpawnData()
    {
        dataSet = new List<SpawnData>();
        List<Dictionary<string, object>> spawnData = CSVReader.Read("CSVFile\\SpawnData");
        for (int idx = 0; idx < spawnData.Count; idx++)
        {

            SpawnData ds = new SpawnData();
            ds.Id = (int)spawnData[idx]["ID"];
            ds.spawnMonster = spawnData[idx]["Spawn_Monster"].ToString();
            ds.spawnMap = spawnData[idx]["Spawn_Map"].ToString();
            ds.spawnOrder = (int)spawnData[idx]["Spawn_Order"];
            ds.spawnNumber = (int)spawnData[idx]["Spawn_Number"];
            ds.spawnStartTime_1 = (int)spawnData[idx]["Spawn_StartTime_1"];
            ds.spawnStartTime_2 = (int)spawnData[idx]["Spawn_StartTime_2"];
            ds.realStartSpawnTime = UnityEngine.Random.Range(ds.spawnStartTime_1, ds.spawnStartTime_2);
            ds.spawnTime = (int)spawnData[idx]["Spawn_Time"];
            ds.currentTime = 0f;
            dataSet.Add(ds);
        }
    }


}
