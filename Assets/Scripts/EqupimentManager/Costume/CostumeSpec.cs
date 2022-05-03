using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CostumeSpec : EquipSpec
{
    #region varialbe
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
    private string mEquipRankDesc;
    public string EquipRankDesc
    {
        get { return mEquipRankDesc; }
        set { mEquipRankDesc = value; }
    }
    [SerializeField]
    private int mEquipRank;
    public override int Rank
    {
        get { return mEquipRank; }
        set { mEquipRank = value; }
    }
    [SerializeField]
    private string mEquipSource;
    public string EquipSource
    {
        get { return mEquipSource; }
        set { mEquipSource = value; }
    }
#endregion
}
