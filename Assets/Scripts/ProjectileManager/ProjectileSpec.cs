using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProjectileSpec : EquipSpec
{
    #region variable
    [SerializeField]
    private string type;
    public override string Type
    {
        get { return type; }
        set { type = value; }
    }

    [SerializeField]
    private string typeName;
    public override string TypeName
    {
        get { return typeName; }
        set { typeName = value; }
    }

    [SerializeField]
    private string equipName;
    public override string Name
    {
        get { return equipName; }
        set { equipName = value; }
    }

    [SerializeField]
    private string equipDesc;
    public override string Desc
    {
        get { return equipDesc; }
        set { equipDesc = value; }
    }

    [SerializeField]
    private int equipRank;
    public override int Rank
    {
        get { return equipRank; }
        set { equipRank = value; }
    }

    [SerializeField]
    private float projectileDamage;
    public float ProjectileDamage
    {
        get { return projectileDamage; }
        set { projectileDamage = value; }
    }

    [SerializeField]
    private float speed;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    [SerializeField]
    private int count;
    public int Count
    {
        get { return count; }
        set { count = value; }
    }

    [SerializeField]
    private int angle;
    public int Angle
    {
        get { return angle; }
        set { angle = value; }
    }

    [SerializeField]
    private float spawnTime;
    public float SpawnTime
    {
        get { return spawnTime; }
        set { spawnTime = value; }
    }

    [SerializeField]
    private int maxPassCount;
    public int MaxPassCount
    {
        get { return maxPassCount; }
        set { maxPassCount = value; }
    }
    #endregion
}

