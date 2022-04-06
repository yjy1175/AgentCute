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
    public abstract string Name
    {
        get;
        set;
    }
    public abstract string Desc
    {
        get;
        set;
    }
    public abstract int Rank
    {
        get;
        set;
    }
    #endregion
}

