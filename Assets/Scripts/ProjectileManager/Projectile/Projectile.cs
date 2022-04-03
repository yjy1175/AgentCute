using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    #region variable
    public abstract ProjectileSpec Spec
    {
        get;
        set;
    }
    public abstract Vector3 Target
    {
        get;
        set;
    }
    #endregion

    #region method
    protected abstract void destroySelf();
    protected abstract void launchProjectile();
    public abstract void setEnable(Vector3 _target, Vector3 _player);
    public abstract void setDisable();
    #endregion
}

