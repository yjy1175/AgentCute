using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CostumeSpec : EquipSpec
{
    #region varialbe
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
    #endregion
}
