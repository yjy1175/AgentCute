using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{


    #region variable
    private static int addProjectilesCount;
    public static int AddProjectilesCount
    {
        get { return addProjectilesCount; }
        set { addProjectilesCount = value; }
    }
    private static float addScaleCoefficient = 1f;

    protected int damage;
    public virtual int Damage
    {
        get { return damage; }
        set { damage = value;}
    }
    public static float AddScaleCoefficient
    {
        get { return addScaleCoefficient; }
        set { addScaleCoefficient = value; }
    }
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.CompareTag("PlayerProjectile") &&  collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<IStatus>().Hp -= damage;
            ObjectPoolManager.Instance.DisableGameObject(gameObject);
        }
    }



    #region method
    protected abstract void destroySelf();
    protected abstract void launchProjectile();
    public abstract void setEnable(Vector3 _target, Vector3 _player, float _angle);
    public abstract void setDisable();
    #endregion
}

