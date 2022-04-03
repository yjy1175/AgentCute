using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IStatus : MonoBehaviour
{
    protected int mHp;
    protected int mSpeed;
    protected int mPhysicalDefense;
    protected int mMonsterMagicDefense;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int propHp
    {
        get { return mHp; }
        set { mHp = value; }
    }

}
