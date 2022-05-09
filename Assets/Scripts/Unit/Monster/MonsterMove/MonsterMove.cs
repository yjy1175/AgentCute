using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : IMove
{
    private bool DEBUG = true;
    private Vector3[] mVectorDir = new Vector3[8] { Vector3.up, Vector3.down, Vector3.left, Vector3.right,
            Vector3.up+ Vector3.left, Vector3.up+ Vector3.right, Vector3.down+ Vector3.left, Vector3.down+ Vector3.right};

    [SerializeField]
    private GameObject mTarget;

    [SerializeField]
    private bool mIsDie;
    public bool IsDie
    {
        set { mIsDie = value; }
        get { return mIsDie; }
    }


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        mSpeed = gameObject.GetComponent<IStatus>().MoveSpeed;
    }

    private void OnEnable()
    {
        gameObject.GetComponent<MonsterEventHandler>().registerIsDieObserver(registerMonsterDie);
    }
    

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(PlayerManager.Instance.Player.transform.position, transform.position) < 0.5f)
        {
            MovingPattern1();
        }
        else { 
            //보스의 경우 BFS알고리즘만 사용
            if (gameObject.GetComponent<MonsterStatus>().MonsterGrade == MonsterManager.MonsterGrade.Boss || SpawnManager.Instance.WaveCount >=2)
            {
                MovingPattern2();
            }
            else
            {
                MovingPattern1();
            }
        }
    }

    private void FixedUpdate()
    {
        // 모든 물리연산은 FixedUpdate 에서
        if (mMoveable && !mIsDie) {
            transform.Translate(mDir * mSpeed * Time.deltaTime);
            int size = gameObject.GetComponent<IStatus>().Size;
            if (gameObject.transform.position.x < PlayerManager.Instance.Player.transform.position.x)
            {
                gameObject.transform.localScale = new Vector3(size, size, size);
            }
            else
            {
                gameObject.transform.localScale = new Vector3(-1 * size, size, size);
            }
        }
    }

    public void registerMonsterDie(bool _die, GameObject _obj)
    {
        mIsDie = _die;
    }

    public override void StopStiffTime(float _time)
    {
        base.StopStiffTime(_time);
    }

    //플레이어와 몬스터 객체의 단위 벡터방향으로 움직이는 길찾기 알고리즘
    void MovingPattern1()
    {
        mDir = PlayerManager.Instance.Player.transform.position - transform.position;
        mDir.Normalize();
    }


    //Player가 업데이트한 BFS노드를 확인하여 가장 가중치가 낮은곳으로 이동
    public void MovingPattern2()
    {
        mTarget = PlayerManager.Instance.Player;
        int value = 99999;
        Vector3 dir = Vector3.right;

        //8방향에 대해 PlayerBfs에서 가장 가중치가 낮은 곳으로 이동
        for (int i = 0; i < 8; i++)
        {
            Vector3 nextPosition = Vector3Int.FloorToInt(transform.position) + mVectorDir[i];
            nextPosition.x += 0.5f;
            nextPosition.y += 0.5f;
            if (!PlayerManager.Instance.Player.GetComponent<PlayerMove>().BfsMap.ContainsKey(nextPosition))
                continue;
            if(PlayerManager.Instance.Player.GetComponent<PlayerMove>().BfsMap[nextPosition].isMove
                    && PlayerManager.Instance.Player.GetComponent<PlayerMove>().BfsMap[nextPosition].value< value)
            {
                value = PlayerManager.Instance.Player.GetComponent<PlayerMove>().BfsMap[nextPosition].value;
                dir = nextPosition;

            }
        }
        mDir = dir - transform.position;
        mDir.Normalize();

        //Player의 BFS탐색 범위 밖이면  Moving Pattern1을 적용
        if (value == 99999) {
            MovingPattern1();
        }

    }
}
