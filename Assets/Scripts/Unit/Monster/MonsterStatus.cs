using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MonsterStatus : IStatus
{
    public int mId;
    public string mSpawnMap;
    [SerializeField]
    private string mMonsterInName;
    public bool mIsDieToKillCount = false;
    public bool mIsDieToGetExp = false;
    //public int MonsterAI;
    [SerializeField]
    private MonsterManager.MonsterGrade mMonsterGrade;

     
    public enum MonsterType
    {
        Normal,
        Boss
    };
    // Start is called before the first frame update
    void Start()
    {
        BaseDamage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //TO-DO 위치를 바꿔야함 의존성을 없애기 위해 IStatus에서 DamageHp를 수정을했는데 Player는 ObjectPool이 없기때문에 그쪽에 Disable코드를 넣을수 없음
        //어디서 disable 시킬지는 고민이 필요
        
    }

    public virtual MonsterManager.MonsterGrade MonsterGrade
    {
        /*
         *  TO-DO :player Attack에서 있어서 동기화가 되는지 확인필요
         */
        get { return mMonsterGrade; }
        set
        {
            mMonsterGrade = value;
        }
    }
    public string MonsterInName
    {
        get { return mMonsterInName;}
        set
        {
            mMonsterInName = value;
        }
    }

}
