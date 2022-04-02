using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipSpec
{
    #region varialbe
    public abstract string Type
    {
        get;
        set;
    }
    public abstract string TypeName
    {
        get;
        set;
    }
    public abstract string EquipName
    {
        get;
        set;
    }
    public abstract string EquipDesc
    {
        get;
        set;
    }
    public abstract int EquipRank
    {
        get;
        set;
    }
    #endregion
}

