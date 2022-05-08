using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : IMove
{
    private bool DEBUG = false;

    private Animator mAnim;
    public Animator MAnim
    {
        get { return mAnim; }
        set { mAnim = value; }
    }

    [SerializeField]
    private VertualJoyStick mJoyStick;

    public struct Node
    {
        public bool isMove;
        public int value;
    }

    private Dictionary<Vector3, Node> mBfsMap = new Dictionary<Vector3, Node>();
    public Dictionary<Vector3, Node> BfsMap
    {
        get { return mBfsMap; }
    }

    private Vector3[] mVectorDir = new Vector3[8] { Vector3.up, Vector3.down, Vector3.left, Vector3.right,
            Vector3.up+ Vector3.left, Vector3.up+ Vector3.right, Vector3.down+ Vector3.left, Vector3.down+ Vector3.right};
    private float raydist = 0.49f;
    private Vector3 mRayPosition;


    void Awake()
    {
        mAnim = transform.GetChild(1).GetComponent<Animator>();
        mJoyStick = GameObject.Find("Canvas").transform.Find("JoyStick").GetComponent<VertualJoyStick>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMove();

        MovingStartegy();
    }


    protected override void UpdateMove()
    {
        if (mMoveable)
        {
            //TO-DO : 핸드폰 키입력으로 변환 필요
            float h = mJoyStick.GetHorizontalValue();
            float v = mJoyStick.GetVerticalValue();
            if(h == 0 && v == 0)
            {
                h = Input.GetAxis("Horizontal");
                v = Input.GetAxis("Vertical");
            }

            //TO-DO : 이동방식은 어떻게 이루어지게?
            // 좌우 반전
            if (h < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (h > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            // 변화량이 있으면 애니메이션 재생
            if (h != 0 || v != 0)
            {
                mAnim.SetFloat("RunState", 0.5f);
                mAnim.SetBool("Run", true);
            }
            else
            {
                mAnim.SetFloat("RunState", 0f);
                mAnim.SetBool("Run", false);
            }
            mDir = new Vector3(h, v, 0).normalized;
        }
    }

    private void FixedUpdate()
    {
        // 모든 물리연산은 FixedUpdate에서
        if(mMoveable)
            transform.Translate(mDir * mSpeed * Time.fixedDeltaTime);
    }

    public override void StopStiffTime(float _time)
    {
        base.StopStiffTime(_time);
        StartCoroutine(CoStiffAnimation(_time));
    }

    IEnumerator CoStiffAnimation(float _time)
    {
        mAnim.SetFloat("RunState", 1f);
        yield return new WaitForSeconds(_time);
        mAnim.SetFloat("RunState", 0f);
    }

    private void MovingStartegy()
    {
        mBfsMap.Clear();
        mBfsMap = new Dictionary<Vector3, Node>();
        MakeMap();
        GetBFS();
    }

    //MakeMap에 의해 만들어진 맵에서 BFS를 돌려 유저기준 최단거리를 구해놓는다.
    //몬스터가 구해진 BFS값을 확인하여 8방향에서 유저에게 가장 가까운 위치를 체크할수 있음
    private void GetBFS()
    {
        Dictionary<Vector3, bool> check = new Dictionary<Vector3, bool>();
        Queue<Vector3> q = new Queue<Vector3>();
        Vector3 startPos = Vector3Int.FloorToInt(transform.position);
        startPos.x += 0.5f;
        startPos.y += 0.5f;
        Node startnode = new Node();
        startnode.isMove = false;
        startnode.value = 0;

        q.Enqueue(startPos);
        mBfsMap[startPos] = startnode;
        while (q.Count != 0)
        {
            Vector3 nowPosition = q.Dequeue();
            for (int i = 0; i < 4; i++)
            {
                Vector3 nextPosition = nowPosition + mVectorDir[i];
                if (mBfsMap.ContainsKey(nextPosition))
                {
                    if (mBfsMap[nextPosition].isMove && !check.ContainsKey(nextPosition))
                    {
                        check[nextPosition] = true;
                        Node nextNode = mBfsMap[nextPosition];
                        nextNode.value = mBfsMap[nowPosition].value + 1;
                        mBfsMap[nextPosition] = nextNode;
                        q.Enqueue(nextPosition);
                    }
                }
            }
        }
    }

    //RayCast를 쏘아 유저기준 Size크기의 Vector3를 key로 갖는 딕셔너리를 만든다.
    private void MakeMap()
    {
        //TO-DO frame drop이 없는 선 안에서 모든 몬스터가 쫓아오도록 size 조정필요
        int Size = 25;
        int count = 0;
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                Node node = new Node();
                //node.isMove = CustomRayCastManager.Instance.NomarlizeMoveableWithRay(transform.position, Size / 2 - i, Size / 2 - j, 0.49f, 0.49f, true, ref mRayPosition); 
                node.isMove = MoveableWithRay(Size / 2 - i, Size / 2 - j);
                node.value = 99999;
                mBfsMap[mRayPosition] = node;
                if (DEBUG)
                {
                    if (!node.isMove)
                    {
                        count++;
                    }
                }
            }
        }
        if (DEBUG)
        {
            Debug.Log("장애물 개수" + count);
            Debug.Log("MakeMap 이후 mBfsMap의크기" + mBfsMap.Count);
        }
    }

    //object위치에서 x,y거리만큼 떨어진곳에 장애물이 있는지 확인
    //타일의 정중앙에서 rayCaset를 발사하여 장애물이 있는지 체크
    //장애물이 없으면 true , 있으면 false를 리턴
    private bool MoveableWithRay(int x, int y)
    {
        Vector3 targetPosition = transform.position;
        targetPosition.x += x;
        targetPosition.y += y;
        float distance = Vector3.Distance(targetPosition, transform.position);


        Vector3 dir = (targetPosition - transform.position).normalized * distance;
        Vector2 rayTargetPosition = new Vector2(transform.position.x + dir.x, transform.position.y + dir.y);

        //rayTargetPosition의 위치를 tile의 한가운데로 위치시켜줌
        rayTargetPosition = Vector2Int.FloorToInt(rayTargetPosition);
        rayTargetPosition.x += 0.5f;
        rayTargetPosition.y += 0.5f;
        mRayPosition = rayTargetPosition;

        for (int i = 0; i < 4; i++)
        {
            RaycastHit2D ray = Physics2D.Raycast(rayTargetPosition, mVectorDir[i], raydist, LayerMask.GetMask("Tilemap"));
            if (ray.collider != null)
            {
                if (DEBUG)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        Vector3 debugVector = mVectorDir[j] * raydist;
                        Debug.DrawRay(new Vector2(rayTargetPosition.x, rayTargetPosition.y), debugVector, Color.red);
                    }
                }
                return false;
            }
        }
        return true;

    }

}
