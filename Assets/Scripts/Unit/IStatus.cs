using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class IStatus : MonoBehaviour
{
    //현재 Hp
    [SerializeField]
    protected int mHp;

    //최대 HP
    [SerializeField]
    protected int mMaxHp;

    //직전에 입은 데미지
    [SerializeField]
    protected int mDamaged;

    //직전에 얻은 포션
    [SerializeField]
    protected int mPotionHp;

    //Object 속도
    [SerializeField]
    private float mSpeedRate = 1f;
    public float SpeedRate
    {
        set
        {
            mSpeedRate = value;
        }
        get 
        { 
            return mSpeedRate; 
        }
    }
    [SerializeField]
    protected float mSpeed;
    public float Speed
    {
        set
        {
            mSpeed = value;
        }
        get
        {
            return mSpeed;
        }
    }

    [SerializeField]
    protected int mBaseDamage;

    public int mPhysicalDefense;
    public int mMagicDefense;
    [SerializeField]
    private bool mIsInvincibility = false;
    [SerializeField]
    private int mSize;

    [SerializeField]
    protected Weapon currentWeapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public Weapon PlayerCurrentWeapon
    {
        set
        {
            currentWeapon = value;
        }
        get
        {
            return currentWeapon;
        }
    }

    public virtual int MaxHP
    {
        get { return mMaxHp; }
        set
        {
            mMaxHp = value;
        }
    }

    public virtual int Hp
    {
        /*
         *  TO-DO :player Attack에서 있어서 동기화가 되는지 확인필요
         */
        get { return mHp; }
        set {
            mHp = value;
            gameObject.GetComponent<IEventHandler>().ChangeHp(mHp, gameObject);
        }
    }
    public virtual int DamageHp
    {
        /*
         *  TO-DO :player Attack에서 있어서 동기화가 되는지 확인필요
         */
        get { return mDamaged; }
        set
        { 
            if (mIsInvincibility)
                value = 0;
            mDamaged = value;
            mHp = Mathf.Max(0, mHp - mDamaged);
            gameObject.GetComponent<IEventHandler>().ChangeHp(mHp, gameObject);
            MessageBoxManager.BoxType bt = (MessageBoxManager.BoxType)Enum.Parse(typeof(MessageBoxManager.BoxType), gameObject.tag + "Damage");
            MessageBoxManager.Instance.createMessageBox(bt, value.ToString(), gameObject.transform.position);
        }
    }

    public virtual int PotionHp
    {
        get { return mPotionHp; }
        set
        {
            mPotionHp = value;
            mHp = Mathf.Min(MaxHP, mHp + mPotionHp);
            gameObject.GetComponent<IEventHandler>().ChangeHp(mHp, gameObject);
            MessageBoxManager.Instance.createMessageBox(MessageBoxManager.BoxType.PlayerHpPotion, 
                value < 0 ? "Hp " + value.ToString() : "Hp +" + value.ToString(), gameObject.transform.position);
        }
    }


    public virtual int BaseDamage
    {
        get { return mBaseDamage; }
        set
        {
            mBaseDamage = value;
        }
    }

    public virtual int Size
    {
        get { return mSize; }
        set
        {
            mSize = value;
            gameObject.transform.localScale = new Vector3(mSize, mSize, mSize);
        }
    }

    protected IEnumerator InvincibilityCorutine(float time)
    {
        mIsInvincibility = true;
        yield return new WaitForSeconds(time);
        mIsInvincibility = false;
    }

    public int getCurrentWeponeDamage()
    {
        return currentWeapon==null ?0:currentWeapon.Spec.WeaponDamage;
    }

    public void ChangeStatusForCostume(Costume.CostumeBuffType _key, Costume _item)
    {
        // 플레이어 체력 상승
        if(_key == Costume.CostumeBuffType.PlayerHP)
        {
            Hp = Hp + _item.GetBuffValue(_key);
            MaxHP = Hp;
        }
        // 플레이어 이속 상승
        else if (_key == Costume.CostumeBuffType.PlayerSPD)
        {
            mSpeedRate += _item.GetBuffValue(_key);
        }
    }

    public void ChangeStatusForSkill(Skill.ESkillBuffType _type, float value)
    {
        // 속도증가 버프
        if (_type == Skill.ESkillBuffType.PlayerSPD)
        {
            mSpeedRate += value;
        }
        // 순간이동
        else if (_type == Skill.ESkillBuffType.PlayerPosition)
        {
            Vector3 currentDir = GetComponent<PlayerAttack>().Target;
            currentDir = (currentDir - transform.position) * value;
            gameObject.transform.position = gameObject.transform.position + currentDir;
        }
        else if (_type == Skill.ESkillBuffType.PlayerWeaponSprite)
        {
            List<GameObject> wList =  EquipmentManager.Instance.FindWepaonList("sw");
            PlayerManager.Instance.getPlayerWeaponSprite().sprite = wList[(int)value].GetComponent<SpriteRenderer>().sprite;
        }
    }
    public void ChangeStatusForSkillOff(Skill.ESkillBuffType _type, float value)
    {
        if(_type == Skill.ESkillBuffType.PlayerSPD)
        {
            mSpeedRate -= value;
        }
        else if(_type == Skill.ESkillBuffType.PlayerWeaponSprite)
        {
            mSpeedRate -= currentWeapon.Spec.WeaponAddSpeed;
            EquipmentManager.Instance.ChangeWeapon(currentWeapon.Spec.Type);
        }
    }
}
