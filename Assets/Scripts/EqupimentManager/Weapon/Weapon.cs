using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    #region variable
    [SerializeField]
    private WeaponSpec spec = new WeaponSpec();
    public WeaponSpec Spec
    {
        get { return spec; }
        set { spec = value; }
    }
    [SerializeField]
    private bool isLocked;
    public bool IsLocked
    {
        get { return isLocked; }
        set { isLocked = value; }
    }
    #endregion
    #region method
    #endregion
}


