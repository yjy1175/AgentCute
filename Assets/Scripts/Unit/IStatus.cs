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

    public int HP
    {
        get { return mHp; }
        set { mHp = value; }
    }

}
