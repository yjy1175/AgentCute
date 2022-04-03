using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : SingleToneMaker<SpawnManager>
{

    private float currentTime = 0f;
    private float minTime = 1f;
    private float maxTime = 3f;

    public Transform[] spawnPoints;

    /*
     * TO-DO : 일반몬스터의 종류는 4가지, 그에따른 스폰 타임,시작시간, 스폰간격등이 정해져있음.
     *         그 정보를 어디서 저장할지 고민.
     */
    public GameObject MonsterA;
    public string Spawn_Map;
    public int SpawnOrder;
    public int SpawnNumber = 3;
    public float SpawnTime = 1f;

    void Awake()
    {


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
        ObjectPoolManager.Instance.CreateDictTable(MonsterA, 5, 5);
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * TO-DO데이터 타입이 정해지면 스폰정보를 리스트로 저장해서 매번 for문 순회하면서 object들 소환시키기
         *
         */
        currentTime += Time.deltaTime;
        if (currentTime > SpawnTime)
        {
            for(int i = 0; i < SpawnNumber; i++) { 
                GameObject enemy = ObjectPoolManager.Instance.EnableGameObject(MonsterA.name);
                if (enemy != null)
                {
                    int index = UnityEngine.Random.Range(0, spawnPoints.Length);
                    enemy.transform.position = spawnPoints[index].position;
                    enemy.SetActive(true);
                }
                currentTime = 0;
            }
        }
    }
}
