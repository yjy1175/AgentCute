using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

    public List<SpawnData> dataSetNormal;
    public List<SpawnData> dataSetBoss;
    private float currentTime = 0f;

    public Transform[] spawnPoints;

    public GameObject[] TempMonster;

    /*
     *  임시로 스폰매니저에 변수 생성 Todo : 추후에 잡은 몬스터의 수는 플레이어 매니저나
     *  나중에 게임매니저에서 관리
     */
    // 전체 몬스터의 수
    public Text allMonsterText;
    public static int allMonsterCount = 0;
    // 필드에 소환된 몬스터의 수
    public Text currentAllMonsterText;
    // 현재 잡은 몬스터의 수
    public Text currentKillMonsterText;
    public static int currentKillMosterCount = 0;

    private const int SPAWN_MINUT = 60;

    public void init()
    {
        allMonsterCount = 0;
        currentKillMosterCount = 0;
    }
    void Awake()
    {
        InitAllSpawnData();
    }

    // Start is called before the first frame update
    void Start()
    {
        //TO-DO 어디서 create 할지 정해야함.
        currentTime = 0f;
        for (int i = 0; i < TempMonster.Length; i++)
        {
            ObjectPoolManager.Instance.CreateDictTable(TempMonster[i], 5, 5);
        }
        spawnBossMonster();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnNormalMonster();
        allMonsterText.text = "전체 : " + allMonsterCount.ToString() + " 마리";
        currentKillMonsterText.text = "킬 : " + currentKillMosterCount.ToString() + " 마리";
        currentAllMonsterText.text = "필드 : " + (allMonsterCount - currentKillMosterCount).ToString() +" 마리";
    }

    private void SpawnNormalMonster()
    {
        for (int i = 0; i < dataSetNormal.Count; i++)
        {
            SpawnData temp = dataSetNormal[i];
            if (dataSetNormal[i].realStartSpawnTime < currentTime && dataSetNormal[i].currentTime > dataSetNormal[i].spawnTime)
            {
                List<int> spawnZone = RandSpawnList(dataSetNormal[i].spawnNumber);
                for (int j = 0; j < spawnZone.Count; j++)
                {
                    GameObject monster = ObjectPoolManager.Instance.EnableGameObject(dataSetNormal[i].spawnMonster);
                    // 타일맵과 일치시키려면 스폰 포지션을 int화해야 합니다.
                    
                    Vector3Int spawnPos = GameObject.Find("Grid").GetComponent<Grid>().WorldToCell(spawnPoints[spawnZone[j]].position);
                    if (monster != null && MapManager.Instance.SpawnalbePosition(spawnPos))
                    {
                        setMonsterData(ref monster);
                        monster.transform.position = spawnPos;
                        monster.GetComponent<MonsterStatus>().mIsDieToKillCount = false;
                        monster.GetComponent<MonsterStatus>().mIsDieToGetExp = false;
                        monster.SetActive(true);
                        // 스폰된 몬스터의 수 증가
                        allMonsterCount++;
                    }
                    // 우선은 안나오게 해놨습니다 재훈님이 따로 다른곳에서 나오게 수정 부탁드립니다.
                    else
                    {
                        //Debug.Log(spawnPos + " 가 장애물 지역이라 소환 안됨");
                    }
                }
                temp.currentTime = 0;
            }
            /*
             * TO-DO : C#의 특정상 구조체를 구조체의 값에만 접근할수 없어서 복사형태로 짜놈. 나중에 curretTime관련 배열만들어서 사용하는게 날듯
             */
            currentTime += Time.deltaTime;
            temp.currentTime += Time.deltaTime;
            dataSetNormal[i] = temp;
        }
    }

    private void spawnBossMonster()
    {
        for (int i = 0; i < dataSetBoss.Count; i++)
        {
            StartCoroutine(spawnBoss(i));
        }
    }

    
    private void setMonsterData(ref GameObject monster)
    {
        MonsterManager.MonsterData md = MonsterManager.Instance.GetMonsterData(monster.name);
        monster.GetComponent<MonsterStatus>().Hp = md.monsterHp;
        monster.GetComponent<MonsterStatus>().Size = md.monsterSize;
        monster.GetComponent<MonsterStatus>().MonsterGrade= md.monsterGrade;
        monster.GetComponent<MonsterStatus>().MonsterInName = md.monsterInName;
        monster.GetComponent<MonsterAttack>().CloseAttackPower = md.closeAttackPower;
        monster.GetComponent<MonsterStatus>().Speed = md.monsterSpeed;
        //TO-DO : monster가 생기는 event를 유저가 구독하여 hp register는 Player에서 구독하도록 변경이 필요.
        monster.GetComponent<MonsterEventHandler>().registerHpObserver(GameObject.Find("PlayerObject").GetComponent<PlayerStatus>().registerMonsterHp);
    }

    private void InitAllSpawnData()
    {
        dataSetNormal = new List<SpawnData>();
        dataSetBoss = new List<SpawnData>();
        List<Dictionary<string, object>> spawnData = CSVReader.Read("CSVFile\\SpawnData");
        for (int idx = 0; idx < spawnData.Count; idx++)
        {

            SpawnData ds = new SpawnData();
            ds.Id = (int)spawnData[idx]["ID"];
            ds.spawnMonster = spawnData[idx]["SpawnMonster"].ToString();
            ds.spawnMap = spawnData[idx]["SpawnMap"].ToString();
            ds.spawnOrder = (int)spawnData[idx]["SpawnOrder"];
            ds.spawnNumber = (int)spawnData[idx]["SpawnNumber"];
            ds.spawnStartTime_1 = (int)spawnData[idx]["SpawnStartTime1"];
            ds.spawnStartTime_2 = (int)spawnData[idx]["SpawnStartTime2"];
            ds.realStartSpawnTime = UnityEngine.Random.Range(ds.spawnStartTime_1 * SPAWN_MINUT, ds.spawnStartTime_2 * SPAWN_MINUT);
            ds.spawnTime = (int)spawnData[idx]["SpawnTime"];
            ds.currentTime = 0f;
            if (MonsterManager.Instance.GetMonsterData(ds.spawnMonster).monsterGrade ==  MonsterManager.MonsterGrade.Boss)
            {
                dataSetBoss.Add(ds);
            }
            else if(MonsterManager.Instance.GetMonsterData(ds.spawnMonster).monsterGrade == MonsterManager.MonsterGrade.Normal)
            {
                dataSetNormal.Add(ds);
            }
        }
    }
    private List<int> RandSpawnList(int cnt)
    {
        List<int> list = new List<int>();
        while (list.Count < cnt)
        {
            int idx = UnityEngine.Random.Range(0, spawnPoints.Length);
            if (!list.Contains(idx))
            {
                list.Add(idx);
            }
        }
        return list;
    }

    IEnumerator spawnBoss(int i)
    {
        yield return new WaitForSeconds(dataSetBoss[i].realStartSpawnTime);
        GameObject monster = ObjectPoolManager.Instance.EnableGameObject(dataSetBoss[i].spawnMonster);
        setMonsterData(ref monster);
        if (monster != null)
        {
            monster.GetComponent<MonsterEventHandler>().registerHpObserver(GameObject.Find("PlayerObject").GetComponent<PlayerStatus>().registerMonsterHp);
            int index = UnityEngine.Random.Range(0, spawnPoints.Length);
            monster.transform.position = spawnPoints[index].position;
            monster.SetActive(true);

            //TO-DO : 보스 광폭화 설정. 일단은 하드코딩해놓은 상태.
            //csv에서 읽어오소 해당 시간에 따라 광포하하도록 수정필요
            //디버그모드를 위해 보스생성 룰 3개를위해 이부분 피쳐처리 및 수정 필요
            yield return new WaitForSeconds(30);
            if (monster.activeInHierarchy)
            {
                monster.GetComponent<SpriteRenderer>().color = Color.red;
                monster.GetComponent<MonsterMove>().Speed = 10f;
                monster.GetComponent<MonsterAttack>().CloseAttackPower = 30;
            }
        }
    }
}
