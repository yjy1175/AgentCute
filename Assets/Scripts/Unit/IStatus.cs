using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public abstract class IStatus : MonoBehaviour
{
    //현재 Hp
    [SerializeField]
    protected int mHp;
    public virtual int Hp
    {
        get { return mHp; }
        set
        {
            mHp = value;
            gameObject.GetComponent<IEventHandler>().ChangeHp(mHp, gameObject);
        }
    }

    //최대 HP
    [SerializeField]
    protected int mMaxHp;
    public virtual int MaxHP
    {
        get { return mMaxHp; }
        set
        {
            mMaxHp = value;
        }
    }

    [SerializeField]
    protected bool mIsDie;
    public virtual bool IsDie
    {
        get { return mIsDie; }
        set
        {
            if (mIsDie != value)
            {
                mIsDie = value;
                gameObject.GetComponent<IEventHandler>().ChangeIsDie(mIsDie, gameObject);
            }
        }

    }

    // 사이즈
    [SerializeField]
    private int mSize;
    public virtual int Size
    {
        get { return mSize; }
        set
        {
            mSize = value;
            gameObject.transform.localScale = new Vector3(mSize, mSize, mSize);
        }
    }


    [SerializeField]
    protected int mCloseDamaged;
    public int CloseDamaged
    {
        get { return mCloseDamaged; }
        set
        {
            mCloseDamaged = value;
            if (mIsInvincibility)
                mCloseDamaged = 0;
            mHp = Mathf.Max(0, mHp - mCloseDamaged);
            gameObject.GetComponent<IEventHandler>().ChangeHp(mHp, gameObject);
            MessageBoxManager.BoxType bt =(MessageBoxManager.BoxType)Enum.Parse(typeof(MessageBoxManager.BoxType), gameObject.tag + "Damage");
            MessageBoxManager.Instance.createMessageBox(bt, mCloseDamaged == 0 ? "무적": mCloseDamaged.ToString(), transform.position);
        }
    }

    //직전에 입은 데미지(발사체)
    [SerializeField]
    protected int mDamaged;
    public  int DamageHp
    {
        get { return mDamaged; }
        set
        {
            mDamaged = value;
            mHp = Mathf.Max(0, mHp - mDamaged);
            gameObject.GetComponent<IEventHandler>().ChangeHp(mHp, gameObject);
        }
    }

    //직전에 얻은 포션
    [SerializeField]
    protected int mPotionHp;
    public virtual int PotionHp
    {
        get { return mPotionHp; }
        set
        {
            mPotionHp = value;
            mHp = Mathf.Min(MaxHP, mHp + mPotionHp);
            gameObject.GetComponent<IEventHandler>().ChangeHp(mHp, gameObject);
            MessageBoxManager.Instance.createMessageBox(MessageBoxManager.BoxType.PlayerHpPotion, value.ToString(), gameObject.transform.position);
        }
    }

    //Object 속도
    [SerializeField]
    protected float mMoveSpeed;
    public float MoveSpeed
    {
        get
        {
            return mMoveSpeed;
        }
        set
        {
            mMoveSpeed = value;
            GetComponent<IEventHandler>().ChangeMoveSpeed(mMoveSpeed, gameObject);
        }

    }
    // Object 속도 계수(아이템 및 스킬)
    [SerializeField]
    private float mMoveSpeedRate = 1f;
    public float MoveSpeedRate
    {
        get
        {
            return mMoveSpeedRate;
        }
        set
        {
            mMoveSpeedRate = value;
            GetComponent<IEventHandler>().ChangeMoveSpeed(mMoveSpeed * (mMoveSpeedRate + mAddSpeed), gameObject);
        }

    }
    // 이동 속도증가량(레벨업시)
    [SerializeField]
    private float mAddSpeed; 
    public float AddSpeed
    {
        get { return mAddSpeed; }
        set
        {
            mAddSpeed = value;
            GetComponent<IEventHandler>().ChangeMoveSpeed(mMoveSpeed * (mMoveSpeedRate + mAddSpeed), gameObject);
        }
    }

    // 기본 공격 속도
    [SerializeField]
    private float mAttackSpeed;
    public float AttackSpeed
    {
        get 
        { 
            return mAttackSpeed ; 
        }
        set 
        { 
            mAttackSpeed = value;
            GetComponent<IEventHandler>().ChangeAttackSpeed(mAttackSpeed, gameObject);
        }
    }
    // 기본공격 속도 증가량
    [SerializeField]
    private float mAddAttackSpeed; 
    public float AddAttackSpeed
    {
        get { return mAddAttackSpeed; }
        set
        {
            mAddAttackSpeed = value;
            GetComponent<IEventHandler>().ChangeAttackSpeed(mAttackSpeed - mAddAttackSpeed, gameObject);

        }
    }

    // 기본 데미지
    [SerializeField]
    protected int mBaseDamage;
    public virtual int BaseDamage
    {
        get { return mBaseDamage; }
        set
        {
            mBaseDamage = value;
            GetComponent<IEventHandler>().ChangeAttackPoint(mBaseDamage, gameObject);
        }
    }

    // 플레이어가 장착중인 무기
    [SerializeField]
    protected Weapon currentWeapon;
    public Weapon PlayerCurrentWeapon
    {
        set
        {
            currentWeapon = value;
            GameObject.Find("Canvas").transform.Find("PausePannel").transform.GetChild(1).GetChild(1).GetComponent<Text>().text = currentWeapon.Spec.TypeName;
        }
        get
        {
            return currentWeapon;
        }
    }
    // 기본 데미지 증가량
    [SerializeField]
    protected float mAddAttackPoint;
    public float AddAttackPoint
    {
        get { return mAddAttackPoint; }
        set 
        { 
            mAddAttackPoint = value;
        }
    }
    // 최종 데미지
    [SerializeField]
    protected int mObjectDamage;

    // 기본 치명타 확률
    [SerializeField]
    protected float mCriticalChance;
    public float CriticalChance
    {
        get { return mCriticalChance; }
        set { mCriticalChance = value; }
    }
    // 기본 치명타 확률 추가량
    [SerializeField]
    protected float mAddCriticalChance;
    public float AddCriticalChance
    {
        get { return mAddCriticalChance; }
        set { mAddCriticalChance = value; }
    }

    // 기본 치명타 데미지
    [SerializeField]
    protected float mCriticalDamage;
    public float CriticalDamage
    {
        get { return mCriticalDamage; }
        set { mCriticalDamage = value; }
    }
    [SerializeField]
    private int mAddProjectileCount; // 기본 공격 발사체 개수
    public int AddProjectileCount
    {
        get
        {
            return mAddProjectileCount;
        }
        set
        {
            mAddProjectileCount = value;
            GetComponent<IEventHandler>().ChangeProjectileCount(mAddProjectileCount, gameObject);
        }
    }

    // 검,창 스텟
    // 기본 공격 범위
    [SerializeField]
    private float mAddAttackRange; 
    public float AddAttackRange
    {
        get
        {
            return mAddAttackRange;
        }
        set
        {
            mAddAttackRange = value;
            GetComponent<IEventHandler>().ChangeProjectileScale(mAddAttackRange, gameObject);
            
        }
    }
    // 기본 공격 경직 시간
    [SerializeField]
    private float mAddStiffTime; 
    public float AddStiffTime
    {
        get
        {
            return mAddStiffTime;
        }
        set
        {
            mAddStiffTime = value;
            GetComponent<IEventHandler>().ChangeStiffTime(mAddStiffTime, gameObject);
        }
    }

    // 보우건, 스태프 스텟
    [SerializeField]
    // 기본공격 횟수
    private int mAddRAttackCount; 
    public int AddRAttackCount
    {
        get
        {
            return mAddRAttackCount;
        }
        set
        {
            mAddRAttackCount = value;
            GetComponent<IEventHandler>().ChangeRAttackCount(mAddRAttackCount, gameObject);
        }
    }
    // 기본 공격 관통
    [SerializeField]
    private  int mAddPassCount;
    public int AddPassCount
    {
        get
        {
            return mAddPassCount;
        }
        set
        {
            mAddPassCount = value;
            GetComponent<IEventHandler>().ChangePassCount(mAddPassCount, gameObject);
        }
    }

    [SerializeField]
    private bool mIsLaunch;
    public bool IsLaunch
    {
        get
        {
            return mIsLaunch;
        }
        set
        {
            mIsLaunch = value;
            GetComponent<IEventHandler>().ChangeIsLaunch(mIsLaunch, gameObject);
        }
    }

    public int mPhysicalDefense;
    public int mMagicDefense;
    [SerializeField]
    private bool mIsInvincibility = false;

    protected virtual void Start()
    {
        // 기본 레벨업 스텟 초기화
        mAddAttackPoint = 0;
        mAddSpeed = 0;
        mAddAttackSpeed = 0;
        mAddCriticalChance = 0;
        mAddProjectileCount = 0;
        mAddAttackRange = 0;
        mAddStiffTime = 0;
        mAddRAttackCount = 0;
        mAddPassCount = 0;
}

    /*
     *  매 공격 호출마다 불러와서 공격력 세팅
     *  리턴값은 true일 경우 크리티컬, false일 경우 기본
     */
    public abstract bool AttackPointSetting(GameObject _obj);

    public void LevelUpStatus(Stat _stat)
    {
        switch (_stat.Type)
        {
            // null
            case LevelUpStatusManager.StatType.Null:
                break;
            // 기본 공격 피해량 증가
            case LevelUpStatusManager.StatType.AutoAttack:
                AddAttackPoint += _stat.StatIncrease;
                break;
            // 치명타 확률 증가
            case LevelUpStatusManager.StatType.CriticalChance:
                AddCriticalChance += _stat.StatIncrease;
                break;
            // 기본 공격 속도 증가
            case LevelUpStatusManager.StatType.AutoAttackSPD:
                AddAttackSpeed += _stat.StatIncrease;
                break;
            // 이동 속도 증가
            case LevelUpStatusManager.StatType.MoveSPD:
                AddSpeed += _stat.StatIncrease;
                break;
            // 발사체 개수 증가(근거리)
            case LevelUpStatusManager.StatType.AutoAttackTimesMelee:
                AddProjectileCount += (int)_stat.StatIncrease;
                break;
            // 발사체 개수 증가(원거리)
            case LevelUpStatusManager.StatType.AutoAttackTimesRange:
                AddProjectileCount += (int)_stat.StatIncrease;
                break;
            // 기본 공격 범위 증가
            case LevelUpStatusManager.StatType.AutoAttackRange:
                AddAttackRange += _stat.StatIncrease;
                break;
            // 기본 공격 경직 증가
            case LevelUpStatusManager.StatType.AutoAttackStiff:
                AddStiffTime += _stat.StatIncrease;
                break;
            // 발사체 횟수 증가
            case LevelUpStatusManager.StatType.AutoLaunchSpread:
                AddRAttackCount += (int)_stat.StatIncrease;
                break;
            // 발사체 관통 증가
            case LevelUpStatusManager.StatType.AutoLaunchThrough:
                AddPassCount += (int)_stat.StatIncrease;
                break;
            // HP회복
            case LevelUpStatusManager.StatType.RecoverHP:
                PotionHp = (int)_stat.StatIncrease;
                break;
        }
    }
    public void Invincibility(float _time)
    {
        StartCoroutine(InvincibilityCorutine(_time));
    }
    protected IEnumerator InvincibilityCorutine(float _time)
    {
        mIsInvincibility = true;
        yield return new WaitForSeconds(_time);
        mIsInvincibility = false;
    }
    public void ChangeStatusForSkill(Skill.ESkillBuffType _type, float value)
    {
        // 속도증가 버프
        if (_type == Skill.ESkillBuffType.PlayerSPD)
        {
            MoveSpeedRate += value;
            Debug.Log(value + "만큼 증가");
        }
        // 순간이동
        else if (_type == Skill.ESkillBuffType.PlayerPosition || _type == Skill.ESkillBuffType.PlayerDash)
        {
            Vector3 currentDir = GetComponent<PlayerAttack>().Target;
            if (currentDir == Vector3.zero)
            {
                currentDir = Vector3.right;
            }
            currentDir = (currentDir - transform.position).normalized * value;
            RaycastHit2D ray = Physics2D.Raycast(
                new Vector2(transform.position.x + currentDir.x, transform.position.y + currentDir.y),
                Vector3.zero, 1f, LayerMask.GetMask("Tilemap"));
            // 벽이 없으면 순간이동
            if(ray.collider == null)
            {
                gameObject.transform.position = gameObject.transform.position + currentDir;
            }
            // 있으면 거리 줄이기
            else
            {
                ChangeStatusForSkill(_type, value - 1);
            }  
        }
        else if (_type == Skill.ESkillBuffType.PlayerWeaponSprite)
        {
            List<GameObject> wList =  EquipmentManager.Instance.FindWepaonList("WS");
            PlayerManager.Instance.getPlayerWeaponSprite().sprite = wList[0].GetComponent<SpriteRenderer>().sprite;
        }
    }
    public void ChangeStatusForSkillOff(Skill.ESkillBuffType _type, float value)
    {
        if(_type == Skill.ESkillBuffType.PlayerSPD)
        {
            MoveSpeedRate -= value;
            Debug.Log(value + "만큼 감소");
        }
        else if(_type == Skill.ESkillBuffType.PlayerWeaponSprite)
        {
            PlayerManager.Instance.getPlayerWeaponSprite().sprite = currentWeapon.GetComponent<SpriteRenderer>().sprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Projectile projectile = collision.GetComponent<Projectile>();
            int damage = projectile.IsDamage(gameObject);
            if (projectile.IsActive  && damage !=0)
            {
                // 넉백 확인
                if(projectile.Spec.Knockback > 0)
                {
                    RaycastHit2D ray = Physics2D.Raycast(
                        transform.position + (transform.position - projectile.transform.position).normalized * (GetComponent<CapsuleCollider2D>().size.x * projectile.Spec.Knockback),
                        Vector2.zero,
                        1f,
                        LayerMask.GetMask("Tilemap"));
                    if(ray.collider == null)
                    {
                        
                        transform.Translate(
                            (transform.position - projectile.transform.position).normalized * 
                            GetComponent<CapsuleCollider2D>().size.x * projectile.Spec.Knockback);
                            
                    }
                }
            }
            // 데미지 입히기 및 데미지 박스 띄우기(타입별)
            if(!mIsInvincibility)
                ChangeHpForDamage(projectile, projectile.Spec.ProjectileDamageType, damage);
        }
    }

    private void ChangeHpForDamage(Projectile obj, ProjectileManager.DamageType _type, int _damage)
    {
        //현 게임 컨셉상 데미지 0이하는 데미지 박스및 데미지가 발생하지 않음
        if (_damage <= 0)
        {
            return;
        }

        switch (_type)
        {
            // 일반형
            case ProjectileManager.DamageType.Normal:
                DamageHp = _damage;
                DrawDamageBox(obj.IsCriticalDamage, _damage, transform.position);
                break;
            // 스플릿형
            case ProjectileManager.DamageType.SplitByNumber:
                int num = obj.Spec.ProjectileDamageSplit;
                for (int i = 0; i < num; i++)
                {
                    DamageHp = _damage / num;
                    DrawDamageBox(obj.IsCriticalDamage, _damage / num, transform.position + Vector3.up * i * 0.5f);
                }
                break;
            // 도트형
            case ProjectileManager.DamageType.SplitByTime:
                int splitnum = (int)(obj.Spec.SpawnTime / obj.Spec.ProjectileDamageSplitSec);
                int damage = _damage / splitnum;
                StartCoroutine(CoSplitByTime(obj.Spec.ProjectileDamageSplitSec, splitnum, damage, obj.IsCriticalDamage));
                break;
        }
    }

    private void DrawDamageBox(bool _isCritical, int _damage, Vector3 _pos)
    {
        if (_isCritical)
        {
            MessageBoxManager.Instance.createMessageBox(
                MessageBoxManager.BoxType.CriticalDamage, _damage.ToString(), _pos);
        }
        else
        {
            MessageBoxManager.BoxType bt = 
                (MessageBoxManager.BoxType)Enum.Parse(typeof(MessageBoxManager.BoxType), gameObject.tag + "Damage");
            MessageBoxManager.Instance.createMessageBox(bt, _damage.ToString(), _pos);
        }
    }

    IEnumerator CoSplitByTime(float _time, int _splitNum, int _damage, bool _isCritical)
    {
        int num = _splitNum;
        while (num > 0)
        {
            num--;
            DamageHp = _damage;
            DrawDamageBox(_isCritical, _damage, transform.position);
            yield return new WaitForSeconds(_time);
        }
    }
}
