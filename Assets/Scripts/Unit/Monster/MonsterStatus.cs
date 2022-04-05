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
    public int mCloseAttackPower;
    public int mCloseAttackRange;
    public int mStandoffAttackPower;
    public int mStandoffAttackRange;
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
}
