using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    #region variable
    private string projectileType;
    public string ProjectileType
    {
        get { return projectileType; }
        set { projectileType = value; }
    }
    private int coolTime;
    public int CoolTime
    {
        get { return coolTime; }
        set { coolTime = value; }
    }
    private int coefficient;
    public int Coefficient
    {
        get { return coefficient; }
        set { coefficient = value; }
    }
    private bool isLocked;
    public bool IsLocked
    {
        get { return isLocked; }
        set { isLocked = value; }
    }
    #endregion
}


