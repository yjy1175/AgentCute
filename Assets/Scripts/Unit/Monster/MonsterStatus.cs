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
        
    }

    public override int Hp
    {
        get { return mHp; }
        set
        {
            mHp = Mathf.Max(0, value);
            if(mHp == 0)
            {
                ObjectPoolManager.Instance.DisableGameObject(gameObject);
            }
        }
    }
}
