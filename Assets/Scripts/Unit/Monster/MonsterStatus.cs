using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatus : IStatus
{
    protected string mSpawnMap;
    protected string mInName;
    protected string mName;
    protected MonsterType Monster_Grade;
    protected int mAttackPower;
    protected float mAttackRange;
    protected int mStandoffAttackPower;
    protected float mStandoffAttackRange;

    /*
     * Close_Attack_Animation
     * Standoff_Attack_Animation
     * Hit_Animation
     */

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
