using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatus : IStatus
{
    public int mId;
    public string mSpawnMap;
    public string mMonsterInName;
    public int mMonsterSize;
    public string mMmonsterName;
    public string mMmonsterGrade;
    //public int MonsterAI;
    public int monsterExp;

     
    public enum MonsterType
    {
        Normal,
        Boss
    };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //TO-DO 위치를 바꿔야함 의존성을 없애기 위해 IStatus에서 DamageHp를 수정을했는데 Player는 ObjectPool이 없기때문에 그쪽에 Disable코드를 넣을수 없음
        //어디서 disable 시킬지는 고민이 필요
        if (mHp == 0)
        {
            SpawnManager.currentKillMosterCount++;
            ObjectPoolManager.Instance.DisableGameObject(gameObject);
        }
    }
}
