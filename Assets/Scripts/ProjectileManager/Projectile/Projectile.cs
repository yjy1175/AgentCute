using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{


    #region variable
    // 추가 관통 마리수
    private static int addPassCount;
    public static int AddPassCount
    {
        get { return addPassCount; }
        set { addPassCount = value; }
    }
    // 추가 발사체 개수
    private static int addProjectilesCount;
    public static int AddProjectilesCount
    {
        get { return addProjectilesCount; }
        set { addProjectilesCount = value; }
    }
    private static float addScaleCoefficient = 1f;
    public static float AddScaleCoefficient
    {
        get { return addScaleCoefficient; }
        set { addScaleCoefficient = value; }
    }
    protected int damage;
    public virtual int Damage
    {
        get { return damage; }
        set { damage = value;}
    }
    [SerializeField]
    protected int currentPassCount;

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

    //void OnTrrigerEnter2D(Collider2D collision)
    //{
    //    if (gameObject.CompareTag("PlayerProjectile") && collision.gameObject.CompareTag("Enemy"))
    //    {
    //        collision.gameObject.GetComponent<IStatus>().Hp -= damage;
    //        ObjectPoolManager.Instance.DisableGameObject(gameObject);
    //    }
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("PlayerProjectile") && collision.gameObject.CompareTag("Enemy"))
        {
            // 관통 구현
            // -1 : 무한 관통
            if(Spec.MaxPassCount != -1)
            {
                currentPassCount++;
                // 현재 관통한 마리수가 정해진 수치보다 같거나 커지면 disable
                if (currentPassCount >= (Spec.MaxPassCount + addPassCount) - 1)
                {
                    currentPassCount = 0;
                    setDisable();
                    ObjectPoolManager.Instance.DisableGameObject(gameObject);
                }
            }
            collision.gameObject.GetComponent<IStatus>().Hp -= damage;
            MessageBoxManager.Instance.createMessageBox(MessageBoxManager.BoxType.MonsterDamage, damage.ToString(), collision.gameObject.transform.position);
        }
    }

    #region method
    protected abstract void destroySelf();
    protected abstract void launchProjectile();
    public abstract void setEnable(Vector3 _target, Vector3 _player, float _angle);
    public abstract void setDisable();
    #endregion
}

