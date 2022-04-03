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
        ObjectPoolManager.Instance.CreateDictTable(mAutoAttack, 5, 0); //TO-DO 무기생성 Create를 여기서하는건 옳지않음. PlayerManager에서 무기종류가 확실히 정해지면 거기서 Create하는걸로 수정
        mAutoAttackSpeed = mAutoAttack.GetComponent<Launch>().Spec.SpawnTime; //TO-DO 임시로 넣어놓음. 실제 공속은 무엇?
        mAutoAttackCheckTime = mAutoAttackSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (mAutoAttackCheckTime > mAutoAttackSpeed) { 
            StartCoroutine(AutoAttackCorutines());
            mAutoAttackCheckTime = 0f;
        }
        mAutoAttackCheckTime += Time.deltaTime;

    }


    IEnumerator AutoAttackCorutines()
    {
        Vector3 temp = new Vector3(1, 1, 0);//TO-DO 플레이어 방향에 따라 나갈수있도록 value 제공해주는 api만들기
        GameObject obj = ObjectPoolManager.Instance.EnableGameObject(mAutoAttack.name);
        Debug.Log(MonsterManager.Instance.GetNearestMonsterPos(firePosition.transform.position));
        obj.GetComponent<Launch>().setEnable(MonsterManager.Instance.GetNearestMonsterPos(firePosition.transform.position)
                , firePosition.transform.position);
        yield return new WaitForSeconds(mAutoAttackSpeed);
        obj.GetComponent<Launch>().setDisable();
        ObjectPoolManager.Instance.DisableGameObject(obj);
    }
}
