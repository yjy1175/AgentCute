using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IStatus : MonoBehaviour
{
    public int mHp;
    public int mSpeed;
    public int mPhysicalDefense;
    public int mMagicDefense;

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
            mHp = Mathf.Max(0, value);
        }
    }

    public virtual int Speed
    {
        get { return Speed; }
        set { mSpeed = value; }
    }

}
