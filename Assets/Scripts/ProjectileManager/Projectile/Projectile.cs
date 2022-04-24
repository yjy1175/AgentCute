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
        set { damage = value; }
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
    public static void init()
    {
        addPassCount = 0;
        addProjectilesCount = 0;
        addScaleCoefficient = 1f;
    }
    // 지속데미지를 위해서 Stay로 바꿈
    // 하지만 0.02초단위로 데미지가 너무 많이 들어가서 우선 0.5초 단위로 바꿈
    // 컨트롤할 bool 형 변수 선언
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(gameObject.tag + collision.tag);
        if ((gameObject.CompareTag("PlayerProjectile") && collision.gameObject.CompareTag("Monster"))) { 
            // 관통 구현
            // -1 : 무한 관통
            if (isActive)
            {
                if (Spec.MaxPassCount != -1)
                {
                    currentPassCount++;
                    // 현재 관통한 마리수가 정해진 수치보다 커지면 disable
                    if (currentPassCount > (Spec.MaxPassCount + GameObject.Find("PlayerObject").GetComponent<IAttack>().PassCount))
                    {
                        setDisable();
                        ObjectPoolManager.Instance.DisableGameObject(gameObject);
                    }
                }
                collision.gameObject.GetComponent<IStatus>().DamageHp = damage;
                // 경직 시간이 있으면
                if(Spec.StiffTime + GameObject.Find("PlayerObject").GetComponent<IAttack>().StiffTime > 0)
                {
                    collision.gameObject.GetComponent<IMove>().StopStiffTime(
                        Spec.StiffTime + GameObject.Find("PlayerObject").GetComponent<IAttack>().StiffTime);
                }

                // 넉백 수치가 있으면
                if(Spec.Knockback > 0)
                {
                    // 프로젝타일의 진행방향으로 플레이어 콜라이더 * 수치만큼 이동
                    collision.gameObject.transform.Translate(
                        (transform.position - Target).normalized * 
                        GameObject.Find("PlayerObject").GetComponent<BoxCollider2D>().size.x * 
                        Spec.Knockback);
                }
            }
        }
        else if (gameObject.CompareTag("MonsterProjectile") && collision.gameObject.CompareTag("Player"))
        {
            if (isActive)
            {
                if (Spec.MaxPassCount != -1)
                {
                    currentPassCount++;
                    // 현재 관통한 마리수가 정해진 수치보다 커지면 disable
                    if (currentPassCount > (Spec.MaxPassCount))
                    {
                        setDisable();
                        ObjectPoolManager.Instance.DisableGameObject(gameObject);
                    }
                }
                
                collision.gameObject.GetComponent<IStatus>().DamageHp = damage;
            }
        }
    }

    #region method
    protected abstract void destroySelf();
    protected abstract void launchProjectile();
    public abstract void setEnable(Vector3 _target, Vector3 _player, float _angle);
    public abstract void setDisable();
    #endregion
}

