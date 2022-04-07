using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class IStatus : MonoBehaviour
{
    public int mHp;
    public int mSpeed;
    public int mPhysicalDefense;
    public int mMagicDefense;
    private bool mIsInvincibility = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual int Hp
    {
        /*
         *  TO-DO :player Attack에서 있어서 동기화가 되는지 확인필요
         */
        get { return mHp; }
        set {
            mHp = value;
        }
    }
    public virtual int DamageHp
    {
        /*
         *  TO-DO :player Attack에서 있어서 동기화가 되는지 확인필요
         */
        get { return mHp; }
        set
        {
            if (mIsInvincibility)
                value = 0;
            MessageBoxManager.BoxType bt = (MessageBoxManager.BoxType)Enum.Parse(typeof(MessageBoxManager.BoxType), gameObject.tag + "Damage");
            MessageBoxManager.Instance.createMessageBox(bt, value.ToString(), gameObject.transform.position);
            mHp = Mathf.Max(0, mHp-value);
        }
    }


    public virtual int Speed
    {
        get { return Speed; }
        set { mSpeed = value; }
    }

    protected IEnumerator InvincibilityCorutine(float time)
    {
        mIsInvincibility = true;
        yield return new WaitForSeconds(time);
        mIsInvincibility = false;
    }

}
