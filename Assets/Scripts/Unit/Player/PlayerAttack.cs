using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : IAttack
{
    public GameObject mAutoAttack;
    
    // Start is called before the first frame update
    void Start()
    {
        TileDict = new Dictionary<string, GameObject>();

        //setTileDict(mWeapone.name, mWeapone);
        ObjectPoolManager.Instance.CreateDictTable(mAutoAttack, 10, 10); //TO-DO 무기생성 Create를 여기서하는건 옳지않음. PlayerManager에서 무기종류가 확실히 정해지면 거기서 Create하는걸로 수정
        mAutoAttackSpeed = mAutoAttack.GetComponent<Projectile>().Spec.SpawnTime; //TO-DO 임시로 넣어놓음. 실제 공속은 무엇?
        mAutoAttackCheckTime = mAutoAttackSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (mAutoAttackCheckTime > mAutoAttackSpeed) {

            StartCoroutine(AutoAttackCorutines());
            /*
             * 발사체의 개수 각도에 따른 원뿔형 발사 구현 입니다.(만약 각도가 없는 경우는 다른 타입으로 빼야됨)
             * luanchCount = 발사체의 발사 개수 + (static 변수)전체적인 발사체의 발사 개수(레벨업으로 인한)
             * angle = 발사될 발사체의 각도입니다.(즉 발사체끼리의 각도)
             * luanchCount만큼 발사가 되고, 발사될때마다 각도만큼 벌려줍니다.(원뿔형)
             */
            //{
            //    int launchCount = mAutoAttack.GetComponent<Projectile>().Spec.Count + Projectile.AddProjectilesCount;
            //    int angle = mAutoAttack.GetComponent<Projectile>().Spec.Angle;
            //    for (int i = 0; i < launchCount; i++)
            //    {
            //        StartCoroutine(AutoAttackCorutines(launchCount == 1 ? 0 : -((launchCount - 1) * angle / 2) + angle * i));
            //    }
            //}
            mAutoAttackCheckTime = 0f;
        }
        mAutoAttackCheckTime += Time.deltaTime;

    }

    IEnumerator AutoAttackCorutines()
    {
        Vector3 temp = new Vector3(1, 1, 0);//TO-DO 플레이어 방향에 따라 나갈수있도록 value 제공해주는 api만들기
        GameObject obj = ObjectPoolManager.Instance.EnableGameObject(mAutoAttack.name);
        //Debug.Log(MonsterManager.Instance.GetNearestMonsterPos(firePosition.transform.position));
        obj.GetComponent<Projectile>().setEnable(MonsterManager.Instance.GetNearestMonsterPos(firePosition.transform.position)
                , firePosition.transform.position, 0);
        yield return new WaitForSeconds(mAutoAttackSpeed);
        obj.GetComponent<Projectile>().setDisable();
        ObjectPoolManager.Instance.DisableGameObject(obj);
    }
    /*
    * _angle : 추가 각도 설정입니다.
    */
    //IEnumerator AutoAttackCorutines(float _angle)
    //{
    //    Vector3 temp = new Vector3(1, 1, 0);//TO-DO 플레이어 방향에 따라 나갈수있도록 value 제공해주는 api만들기
    //    GameObject obj = ObjectPoolManager.Instance.EnableGameObject(mAutoAttack.name);
    //    Debug.Log(MonsterManager.Instance.GetNearestMonsterPos(firePosition.transform.position));
    //    obj.GetComponent<Projectile>().setEnable(MonsterManager.Instance.GetNearestMonsterPos(firePosition.transform.position)
    //            , firePosition.transform.position, _angle);
    //    yield return new WaitForSeconds(mAutoAttackSpeed);
    //    obj.GetComponent<Projectile>().setDisable();
    //    ObjectPoolManager.Instance.DisableGameObject(obj);
    //}
}
