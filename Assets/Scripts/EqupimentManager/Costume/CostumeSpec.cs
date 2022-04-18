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
    public override string Name
    {
        get { return mEquipName; }
        set { mEquipName = value; }
    }

    [SerializeField]
    private string mEquipDesc;
    public override string Desc
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
#endregion
}
