using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProjectileSpec : EquipSpec
{
    #region variable
    [SerializeField]
    private string mType;
    public override string Type
    {
        get { return mType; }
        set { mType = value; }
    }

    [SerializeField]
    private string mTypeName;
    public override string TypeName
    {
        get { return mTypeName; }
        set { mTypeName = value; }
    }

    [SerializeField]
    private string mEquipName;
    public override string EquipName
    {
        get { return mEquipName; }
        set { mEquipName = value; }
    }

    [SerializeField]
    private string mEquipDesc;
    public override string EquipDesc
    {
        get { return mEquipDesc; }
        set { mEquipDesc = value; }
    }

    [SerializeField]
    private int mEquipRank;
    public override int Rank
    {
        get { return mEquipRank; }
        set { mEquipRank = value; }
    }

    [SerializeField]
    private float mProjectileDamage;
    public float ProjectileDamage
    {
        get { return mProjectileDamage; }
        set { mProjectileDamage = value; }
    }

    [SerializeField]
    private ProjectileManager.DamageType mProjectileDamageType;
    public ProjectileManager.DamageType ProjectileDamageType
    {
        get { return mProjectileDamageType; }
        set { mProjectileDamageType = value; }
    }

    [SerializeField]
    private int mProjectileDamageSplit;
    public int ProjectileDamageSplit
    {
        get { return mProjectileDamageSplit; }
        set { mProjectileDamageSplit = value; }
    }

    [SerializeField]
    private float mProjectileDamageSplitSec;
    public float ProjectileDamageSplitSec
    {
        get { return mProjectileDamageSplitSec; }
        set { mProjectileDamageSplitSec = value; }
    }

    [SerializeField]
    private float mProjectileAttackSpeed;
    public float ProjectileAttackSpeed
    {
        get { return mProjectileAttackSpeed; }
        set { mProjectileAttackSpeed = value; }
    }

    [SerializeField]
    private float mMoveSpeed;
    public float MoveSpeed
    {
        get { return mMoveSpeed; }
        set { mMoveSpeed = value; }
    }

    [SerializeField]
    private int mCount;
    public int Count
    {
        get { return mCount; }
        set { mCount = value; }
    }

    [SerializeField]
    private int mAngle;
    public int Angle
    {
        get { return mAngle; }
        set { mAngle = value; }
    }

    [SerializeField]
    private float mSpawnTime;
    public float SpawnTime
    {
        get { return mSpawnTime; }
        set { mSpawnTime = value; }
    }

    [SerializeField]
    private int mMaxPassCount;
    public int MaxPassCount
    {
        get { return mMaxPassCount; }
        set { mMaxPassCount = value; }
    }

    [SerializeField]
    private float mStiffTime;
    public float StiffTime
    {
        get { return mStiffTime; }
        set { mStiffTime = value; }
    }

    [SerializeField]
    private float mKnockback;
    public float Knockback
    {
        get { return mKnockback; }
        set { mKnockback = value; }
    }

    [SerializeField]
    private float mProjectileSizeX;
    public float ProjectileSizeX
    {
        get { return mProjectileSizeX; }
        set { mProjectileSizeX = value; }
    }

    [SerializeField]
    private float mProjectileSizeY;
    public float ProjectileSizeY
    {
        get { return mProjectileSizeY; }
        set { mProjectileSizeY = value; }
    }

    [SerializeField]
    private float mProjectileDelayTime;
    public float ProjectileDelayTime
    {
        get { return mProjectileDelayTime; }
        set { mProjectileDelayTime = value; }
    }
    #endregion
}

