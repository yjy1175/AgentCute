using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SpawnManager : SingleToneMaker<SpawnManager>
{
    private bool DEBUG = false;

    //CSVFile/SpawnData.csv 데이터 저장용 구조체 
    //몬스터 spawnRule 정보
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

    //CSVFile/Wave.csv 데이터 저장용 구조체 
    //Wave별 몬스터 증가 stat정보
    public struct WaveData
    {
        public float hpScale;
        public float speedUp;
    }

    //CSVFile/BossBerserkerData.csv 데이터 저장용 구조체 
    //보스몬스터 버서커모드 관련 정보
    public struct BerserkerData
    {
        public int bossLimitTime;
        //보스몬스터 속도 배수
        public float speedUp;
        //보스몬스터 공격력 배수
        public float powerUp;
    }


    [SerializeField]
    private List<WaveData> mDataSetWave;
    [SerializeField]
    private List<SpawnData> mDataSetNormal;
    [SerializeField]
    private List<SpawnData> mDataSetBoss;
    [SerializeField]
    private Dictionary<string, BerserkerData> mDataSetBerserker;

    [SerializeField]
    private float currentTime = 0f;

    //다음에 나올 보스번호 체크 & WaveNumber
    [SerializeField]
    private int mBossNum = 0;

    //TO-DO UI쪽에서 관리하도록 수정필요
    IEnumerator bossMessageCoroutine;
    IEnumerator waveMessageCoroutine;

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

    [SerializeField]
    private MapManager.MapType mMapType;

    //spawn룰에 관련된 멤버 변수
    [SerializeField]
    private Transform[] mMonsterArea;
    [SerializeField]
    private Vector2 mMonsterAreadSize = new Vector2(9f, 5f);
    [SerializeField]
    private List<int> mMonsterAreaNum;
    [SerializeField]
    private Transform[,] mSpawnPoints;

    public void init()
    {
        allMonsterCount = 0;
        currentKillMosterCount = 0;
    }
    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //swapZone 초기화
        InitSwapZone();

        //데이터 읽어오기
        InitBerserkerData();
        InitWaveData();

        //boss 체크 초기화
        mBossNum = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerManager.Instance.IsGameStart)
        {

            string s ="";

            for (int i = 1; i < mMonsterArea.Length; i++) {
                mMonsterAreaNum[i-1] = Physics2D.OverlapBoxAll(mMonsterArea[i].transform.position, mMonsterAreadSize, 0, LayerMask.GetMask("Monster")).Length;
                if(DEBUG)
                    s = s+" "+i+"의 몬스터수: " + mMonsterAreaNum[i-1].ToString();
            }
            if(DEBUG)
                Debug.Log(s);

            SpawnMonster();
            currentTime += Time.deltaTime;
            allMonsterText.text = "전체 : " + allMonsterCount.ToString() + " 마리";
            currentKillMonsterText.text = "킬 : " + currentKillMosterCount.ToString() + " 마리";
            currentAllMonsterText.text = "필드 : " + (allMonsterCount - currentKillMosterCount).ToString() +" 마리";
        }
    }

    /*
    spawnzone 체크용 디버그 gizmos
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < mMonsterArea.Length; i++)
        {
            Gizmos.DrawWireCube(mMonsterArea[i].position, mMonsterAreadSize);
        }
    }
    */
    private void SpawnMonster()
    {
        SpawnNormalMonster();
        SpawnBossMonster();
    }

    //NormalMonster 스폰
    private void SpawnNormalMonster()
    {
        //노멀몹 스폰
        for (int i = 0; i < mDataSetNormal.Count; i++)
        {
            SpawnData monsterData = mDataSetNormal[i];
            if (mDataSetNormal[i].realStartSpawnTime < currentTime && mDataSetNormal[i].currentTime > mDataSetNormal[i].spawnTime)
            {
                List<Transform> spawnZone = GetRandomZoneList(mDataSetNormal[i].spawnNumber + mBossNum);//성욱님 요구사항으로 웨이브마다 +1마리씩 더 스폰 
                for (int j = 0; j < spawnZone.Count; j++)
                {
                    GameObject monster = ObjectPoolManager.Instance.EnableGameObject(mDataSetNormal[i].spawnMonster);
                    if (monster != null && spawnZone[j].GetComponent<SpawnZone>().Spawnable)
                    {
                        SpawnMonsterSet(ref monster, spawnZone[j].position);
                    }
                    else
                    {
                        //수정한상태. 이부분 로그가 나오면 문제
                        Debug.Log(spawnZone[j] + "번째 장애물 지역 소환 안됨");
                    }
                }
                monsterData.currentTime = 0;
            }
            monsterData.currentTime += Time.deltaTime;
            mDataSetNormal[i] = monsterData;
        }
    }


    //보스몬스터 스폰
    private void SpawnBossMonster()
    {
        if (mBossNum < mDataSetBoss.Count && mDataSetBoss[mBossNum].realStartSpawnTime < currentTime)
        {

            int childCnt = GameObject.Find("ObjectPoolSet").transform.childCount;
            for(int i=0;i< childCnt; i++)
            {
                GameObject obj = GameObject.Find("ObjectPoolSet").transform.GetChild(i).gameObject;
                for(int j=0;j< mDataSetNormal.Count; j++)
                {
                    if (obj.activeInHierarchy && mDataSetNormal[j].spawnMonster.Equals(obj.name))
                    {
                        ObjectPoolManager.Instance.DisableGameObject(obj);
                    }
                }
            }


            //몬스터 소환 time 초기화
            for (int i = 0; i < mDataSetNormal.Count; i++)
            {
                SpawnData temp = mDataSetNormal[i];
                temp.currentTime = 0f;
                mDataSetNormal[i] = temp;
            }

            GameObject monster = ObjectPoolManager.Instance.EnableGameObject(mDataSetBoss[mBossNum].spawnMonster);
            monster.GetComponent<MonsterEventHandler>().registerHpObserver(registerBossHp);

            List<Transform> spawnZone = GetRandomZoneList(1);
            SpawnMonsterSet(ref monster, spawnZone[0].position);

            mBossNum++;

            if (bossMessageCoroutine != null)
                StopCoroutine(bossMessageCoroutine);
            bossMessageCoroutine = SpawnMessage(GameObject.Find("MonsterStatusObject").transform.Find("Alarm").gameObject, "보스 등장", 6, 0);
            StartCoroutine(bossMessageCoroutine);

            //TO-DO 데이터 셋으로 받게 수정 필요
            currentTime = -60f;//다음 웨이브 시작시간 

            if (waveMessageCoroutine != null)
                StopCoroutine(waveMessageCoroutine);
            waveMessageCoroutine = SpawnMessage(GameObject.Find("MonsterStatusObject").transform.Find("Alarm").gameObject, "wave " + mBossNum, 6, 60);
            StartCoroutine(waveMessageCoroutine);

            //버서커모드 코루틴
            StartCoroutine(BossWaitBerserkerMode(monster));
        }
    }

    //스폰전 몬스터 status 설정
    private void SpawnMonsterSet(ref GameObject _monster, Vector3 _pos)
    {
        MonsterManager.MonsterData md = MonsterManager.Instance.GetMonsterData(_monster.name);
        if(md.monsterGrade == MonsterManager.MonsterGrade.Boss)
        {
            _monster.GetComponent<MonsterStatus>().Hp = md.monsterHp;
            _monster.GetComponent<MonsterStatus>().MaxHP = md.monsterHp;
            _monster.GetComponent<MonsterStatus>().MoveSpeed = md.monsterSpeed;

        }
        else {
            _monster.GetComponent<MonsterStatus>().Hp = (int)((float)md.monsterHp * mDataSetWave[mBossNum].hpScale);
            _monster.GetComponent<MonsterStatus>().MaxHP = (int)((float)md.monsterHp * mDataSetWave[mBossNum].hpScale);
            _monster.GetComponent<MonsterStatus>().MoveSpeed = md.monsterSpeed + mDataSetWave[mBossNum].speedUp;
        }

        _monster.GetComponent<MonsterStatus>().Size = md.monsterSize;
        _monster.GetComponent<MonsterStatus>().MonsterGrade= md.monsterGrade;
        _monster.GetComponent<MonsterStatus>().MonsterInName = md.monsterInName;
        _monster.GetComponent<MonsterStatus>().MoveSpeedRate = 1f;
        _monster.GetComponent<MonsterStatus>().mIsDieToKillCount = false;
        _monster.GetComponent<MonsterStatus>().mIsDieToGetExp = false;
        _monster.transform.position = _pos;


        _monster.GetComponent<MonsterAttack>().CloseAttackPower = md.closeAttackPower;

        _monster.GetComponent<IMove>().Moveable = true;
        _monster.GetComponent<BoxCollider2D>().isTrigger = false;
        Color monsterColor = _monster.GetComponent<SpriteRenderer>().color;
        monsterColor.a = 1f;
        _monster.GetComponent<SpriteRenderer>().color = monsterColor;

        //TO-DO : monster가 생기는 event를 유저가 구독하여 hp register는 Player에서 구독하도록 변경이 필요.
        _monster.GetComponent<MonsterEventHandler>().registerHpObserver(PlayerManager.Instance.Player.GetComponent<PlayerStatus>().registerMonsterHp);

        _monster.SetActive(true);
        allMonsterCount++;
    }

    //swap존 초기화
    private void InitSwapZone()
    {
        mMonsterAreaNum = new List<int>() { 0, 0, 0, 0 };
        mSpawnPoints = new Transform[GameObject.Find("SwapZone").transform.childCount, GameObject.Find("Zone0").transform.childCount];
        mMonsterArea = GameObject.Find("MonsterCountCheckObject").transform.GetComponentsInChildren<Transform>();
        for (int i = 0; i < GameObject.Find("SwapZone").transform.childCount; i++)
        {
            for (int j = 0; j < GameObject.Find("Zone" + i.ToString()).transform.childCount; j++)
            {
                mSpawnPoints[i, j] = GameObject.Find("Zone" + i.ToString()).transform.GetChild(j);
            }
        }
    }

    //Wave데이터 저장
    private void InitWaveData()
    {
        mDataSetWave = new List<WaveData>();
        List<Dictionary<string, object>> waveData = CSVReader.Read("CSVFile\\Wave");
        for (int idx = 0; idx < waveData.Count; idx++)
        {
            WaveData ws = new WaveData();
            ws.hpScale = float.Parse(waveData[idx]["HpScale"].ToString());
            ws.speedUp = float.Parse(waveData[idx]["SpeedUp"].ToString());
            mDataSetWave.Add(ws);
        }
    }

    //BerserkerData 저장

    private void InitBerserkerData()
    {
        mDataSetBerserker = new Dictionary<string, BerserkerData>();
        List<Dictionary<string, object>> waveData = CSVReader.Read("CSVFile\\BossBerserkerData");
        for (int idx = 0; idx < waveData.Count; idx++)
        {
            BerserkerData ws = new BerserkerData();
            string name = waveData[idx]["BossMonster"].ToString();

            ws.bossLimitTime = int.Parse(waveData[idx]["BossLimitTime"].ToString());
            ws.powerUp = float.Parse(waveData[idx]["SpeedUp"].ToString());
            ws.speedUp = float.Parse(waveData[idx]["PowerUp"].ToString());
            mDataSetBerserker[name] = ws;
        }
    }

    //스폰 데이터 저장
    public void InitAllSpawnData()
    {
        mMapType = MapManager.Instance.CurrentMapType;

        mDataSetNormal = new List<SpawnData>();
        mDataSetBoss = new List<SpawnData>();
        List<Dictionary<string, object>> spawnData = CSVReader.Read("CSVFile\\SpawnData");
        if (DEBUG)
            Debug.Log("현재 맵타입" + mMapType);
        
        for (int idx = 0; idx < spawnData.Count; idx++)
        {
            if ((MapManager.MapType)Enum.Parse(typeof(MapManager.MapType), spawnData[idx]["SpawnMap"].ToString()) == mMapType) { 
                SpawnData ds = new SpawnData();
                ds.Id = (int)spawnData[idx]["ID"];
                ds.spawnMonster = spawnData[idx]["SpawnMonster"].ToString();
                ds.spawnMap = spawnData[idx]["SpawnMap"].ToString();
                ds.spawnOrder = (int)spawnData[idx]["SpawnOrder"];
                ds.spawnNumber = (int)spawnData[idx]["SpawnNumber"];
                ds.spawnStartTime_1 = (int)spawnData[idx]["SpawnStartTime1"];
                ds.spawnStartTime_2 = (int)spawnData[idx]["SpawnStartTime2"];
                ds.realStartSpawnTime = UnityEngine.Random.Range(ds.spawnStartTime_1, ds.spawnStartTime_2);
                ds.spawnTime = (int)spawnData[idx]["SpawnTime"];
                ds.currentTime = 0f;
                GameObject obj = Resources.Load<GameObject>("Prefabs\\Monster\\" + mMapType.ToString() + "\\" + spawnData[idx]["SpawnMonster"].ToString());

                if (MonsterManager.Instance.GetMonsterData(ds.spawnMonster).monsterGrade ==  MonsterManager.MonsterGrade.Boss)
                {
                    mDataSetBoss.Add(ds);
                    ObjectPoolManager.Instance.CreateDictTable(obj, 1, 1);
                }
                else 
                {
                    ObjectPoolManager.Instance.CreateDictTable(obj, 100, 20);
                    mDataSetNormal.Add(ds);
                }
            }
        }
    }

    //return : cnt개수만큼 몬스터수가 적은 Zone에서 spawn
    private List<Transform> GetRandomZoneList(int _cnt)
    {
        //mMonsterAreaNum에서 몬스터가 가장 적은 위치 체크
        //해당 위치에서 랜덤으로 스폰
        int nextSpawnIdx = 0;
        int checkMonsterNum = int.MaxValue;
        int zoneLength;
        List<Transform> randomList = new List<Transform>();

        for (int i = 0; i < mMonsterAreaNum.Count; i++)
        {
            if (mMonsterAreaNum[i] < checkMonsterNum)
            {
                nextSpawnIdx = i;
                checkMonsterNum = mMonsterAreaNum[i];
            }
        }
        if (DEBUG)
            Debug.Log("Zone" + nextSpawnIdx.ToString() + "에서 스폰");

        zoneLength = GameObject.Find("Zone" + nextSpawnIdx.ToString()).transform.childCount;

        
        //모든 스폰존에서 생성이 안될수도 있기때문에 infiniteCheck
        int infiniteCheck = 0;

        while (randomList.Count < _cnt && infiniteCheck < 100)
        {
            int idx = UnityEngine.Random.Range(0, zoneLength);
            if (mSpawnPoints[nextSpawnIdx,idx].GetComponent<SpawnZone>().Spawnable)
            {
                randomList.Add(mSpawnPoints[nextSpawnIdx, idx]);
            }
            infiniteCheck++;
        }
        return randomList;
    }

    //보스 버서커모드 대기 코루틴
    IEnumerator BossWaitBerserkerMode (GameObject _obj)
    {
        BerserkerData ws;
        ws = mDataSetBerserker[_obj.name];
        if (DEBUG)
            Debug.Log("버서커모드 적용 대기시간"+ ws.bossLimitTime);

        yield return new WaitForSeconds(ws.bossLimitTime);

        if (_obj.activeInHierarchy)
        {
            
            if(DEBUG)
                Debug.Log("버서커모드 적용 " + gameObject.name + " speedUp:" + ws.speedUp + " , powerUp" + ws.powerUp);
            _obj.GetComponent<IStatus>().MoveSpeed *= ws.speedUp;
            _obj.GetComponent<MonsterAttack>().BerserkerModeScale = ws.powerUp;
        }

    }
    
    //TO-DO 보스몬스터 체력을 보여주는 UI, UIManager등에서 관리하도록 수정이 필요한지 고민필요
    public void registerBossHp(int _hp, GameObject _obj)
    {
        if (_hp <= 0)
        {
            if (currentTime < 0f)
            {
                if (waveMessageCoroutine != null)
                    StopCoroutine(waveMessageCoroutine);
                waveMessageCoroutine = SpawnMessage(GameObject.Find("MonsterStatusObject").transform.Find("Alarm").gameObject, "wave " + mBossNum, 6, 0);
                StartCoroutine(waveMessageCoroutine);
            }
            currentTime = Mathf.Max(0f, currentTime);
        }
    }


    //TO-DO 보스등장 메시지는 여기서하는게 맞을까?
    IEnumerator SpawnMessage(GameObject _obj, string _txt, int _cnt, float _time)
    {
        yield return new WaitForSeconds(_time);

        Debug.Log(_obj.name + _txt + _cnt);
        _obj.GetComponent<Text>().text = _txt;
        for (int i = 0; i < _cnt; i++)
        {
            yield return new WaitForSeconds(0.5f);
            _obj.SetActive(!_obj.activeInHierarchy);
        }
        _obj.SetActive(false);
    }
}
