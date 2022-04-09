using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 
    한 가지 gameObject에 관하여 List를 사용하여 GameObject들을 저장해놓는다.  

 */
public class ObjectPool
{
    private Queue<GameObject> objectPool;
    private GameObject objectFactory;
    private int overAllocateCount; //해당 변수가 0이아니면, 풀이 부족하면 overAllocateCount만큼 objectPool증가

    public ObjectPool(GameObject objectFactory, int initCount, int overAllocateCount)
    {
        this.objectFactory = objectFactory;
        this.overAllocateCount = overAllocateCount;
        objectPool = new Queue<GameObject>();
        Allocate(initCount);
    }

    //TO-DO 현재 오브젝트풀이 생성되면 ObjectPoolSet이라는 빈 오브젝트에 넣고 있는데 이것을 어떻게 관리할지에 대해 고민이 필요

    /*
     * 오브젝트풀의 개수를 cnt개수만큼늘려준다.
     */
    public void Allocate(int cnt)
    {
        for (int i = 0; i < cnt; i++)
        {
            GameObject obj = GameObject.Instantiate(objectFactory, GameObject.Find("ObjectPoolSet").transform);
            obj.name = objectFactory.name;
            obj.gameObject.SetActive(false);
            objectPool.Enqueue(obj);
        }

    }
    
    public bool IsNull()
    {
        return objectPool.Count == 0 ? true : false;       
    }
    /*
     *  오브젝트풀에 들어있는 GameObject를 내본다.
     */
    public GameObject EnableObject()
    {

        if (objectPool.Count <= 0 && overAllocateCount <= 0)
        {
            Debug.Log(objectFactory.name+"를 Enable할수 없습니다.");
            return null;
        }
        /*
         * TO-DO: 
         * 우리게임에서는 자동으로 늘어나야할 오브젝이있을까? 몬스터의 경우 자동으로 늘어나게해야하나? 추가 메서드가 필요할까
         * 
         */
        else if (objectPool.Count <= 0 && overAllocateCount > 0)
        {
            //Debug.Log("오브젝트 전부 사용 " + overAllocateCount + "개를 추가합니다");
            Allocate(overAllocateCount);
        }

        GameObject retObj = objectPool.Dequeue();
        //objectPool.Remove(retObj);

        return retObj;
    }
    public void DisableObject(GameObject obj)
    {
        if (obj.activeInHierarchy)
        {
            objectPool.Enqueue(obj);
            obj.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("중복 호출 : " + obj.name);
        }

    }
}
