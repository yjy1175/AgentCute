using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class IStatus : MonoBehaviour
{
    [SerializeField]
    protected int mHp;
    [SerializeField]
    protected int mDamaged;
    [SerializeField]
    protected int mMaxHp;

    public int mPhysicalDefense;
    public int mMagicDefense;
    [SerializeField]
    private bool mIsInvincibility = false;
    [SerializeField]
    private int mSize;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

}
