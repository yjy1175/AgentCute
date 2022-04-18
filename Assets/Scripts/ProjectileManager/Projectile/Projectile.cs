using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    #region variable
    // 추가 관통 마리수
    [SerializeField]
    private static int addPassCount;
    public static int AddPassCount
    {
        get { return addPassCount; }
        set { addPassCount = value; }
    }
    // 추가 발사체 개수
    [SerializeField]
    private static int addProjectilesCount;
    public static int AddProjectilesCount
    {
        get { return addProjectilesCount; }
        set { addProjectilesCount = value; }
    }
    // 추가 발사체 범위
    [SerializeField]
    private static float addScaleCoefficient = 1f;
    public static float AddScaleCoefficient
    {
        get { return addScaleCoefficient; }
        set { addScaleCoefficient = value; }
    }
    [SerializeField]
    protected int damage;
    public virtual int Damage
    {
        get { return damage; }
        set { damage = value;}
    }
    [SerializeField]
    protected int currentPassCount;
    public int CurrentPassCount
    {
        get { return currentPassCount; }
        set { currentPassCount = value; }
    }
    [SerializeField]
    protected Vector3 myPos;
    public Vector3 MyPos
    {
        get { return myPos; }
    }

    //실제 active상태인지 체크
    [SerializeField]
    protected bool isActive = false;


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
        if (gameObject.CompareTag("PlayerProjectile") && collision.gameObject.CompareTag("Monster"))
        {
            // 관통 구현
            // -1 : 무한 관통
            if (isActive) {
                if (Spec.MaxPassCount != -1)
                {
                    currentPassCount++;
                    // 현재 관통한 마리수가 정해진 수치보다 커지면 disable
                    if (currentPassCount > (Spec.MaxPassCount + addPassCount))
                    {
                        setDisable();
                        ObjectPoolManager.Instance.DisableGameObject(gameObject);
                    }
                }
                collision.gameObject.GetComponent<IStatus>().DamageHp = damage;
            }
        }
        else if (gameObject.CompareTag("MonsterProjectile") && collision.gameObject.CompareTag("Player"))
        {
            // TO-DO : 플레이어가 발사체에 맞는 처리
        }
    }

    #region method
    protected abstract void destroySelf();
    protected abstract void launchProjectile();
    public abstract void setEnable(Vector3 _target, Vector3 _player, float _angle);
    public abstract void setDisable();
    #endregion
}

