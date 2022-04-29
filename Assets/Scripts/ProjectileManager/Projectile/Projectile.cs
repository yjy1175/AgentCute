using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    #region variable
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
    protected bool mIsActive = false;
    public bool IsActive
    {
        get { return mIsActive; }
        set { mIsActive = value; }
    }

    [SerializeField]
    private float mAttackSpeedCheckTime = 1;

    // 각 공격당 크리티컬 데미지인지 체크
    [SerializeField]
    private bool mIsCriticalDamage;
    public bool IsCriticalDamage
    {
        get { return mIsCriticalDamage; }
        set { mIsCriticalDamage = value; }
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
    private void OnDisable()
    {
        mAttackSpeedCheckTime = 1;
    }
    protected virtual void FixedUpdate() 
    {
        if(mIsActive)
            mAttackSpeedCheckTime += Time.fixedDeltaTime;
    }
    // 지속데미지를 위해서 Stay로 바꿈
    // 하지만 0.02초단위로 데미지가 너무 많이 들어가서 우선 0.5초 단위로 바꿈
    // 컨트롤할 bool 형 변수 선언
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster") || collision.gameObject.CompareTag("Player"))
        {
                // 관통 구현
            // -1 : 무한 관통
            if (mIsActive)
            {
                if (Spec.MaxPassCount != -1)
                {
                    currentPassCount++;
                    // 현재 관통한 마리수가 정해진 수치보다 커지면 disable
                    if (currentPassCount > (Spec.MaxPassCount + 
                        (collision.gameObject.CompareTag("Monster") ? GameObject.Find("PlayerObject").GetComponent<IAttack>().PassCount : 0)))
                    {
                        setDisable();
                        ObjectPoolManager.Instance.DisableGameObject(gameObject);
                    }
                }
                // 경직 확인
                float tmpStiffTime = collision.gameObject.CompareTag("Monster") ? GameObject.Find("PlayerObject").GetComponent<IAttack>().StiffTime : 0f;
                if (Spec.StiffTime + tmpStiffTime > 0)
                {
                    // 기본공격일 경우
                    if (Spec.Type == GameObject.Find("PlayerObject").GetComponent<IAttack>().CurrentBaseSkill.Spec.getProjectiles()[0])
                    {
                        collision.GetComponent<IMove>().StopStiffTime(Spec.StiffTime + tmpStiffTime);
                    }
                    // 타 스킬일 경우
                    else
                        collision.GetComponent<IMove>().StopStiffTime(Spec.StiffTime);
                }
            }
        }
    }

    #region method
    protected abstract void launchProjectile();
    public abstract void setEnable(Vector3 _target, Vector3 _player, float _angle);
    public abstract void setDisable();
    public void setSize(Vector3 _size)
    {
        transform.localScale = _size;
    }
    public void setDisableWaitForTime(float _time)
    {
        StartCoroutine(CoDisableWaitForTime(_time));
    }
    IEnumerator CoDisableWaitForTime(float _time)
    {
        yield return new WaitForSeconds(_time);
        if (gameObject.activeInHierarchy)
        {
            setDisable();
            ObjectPoolManager.Instance.DisableGameObject(gameObject);
        }
    }
    #endregion
}

