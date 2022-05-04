using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private int mHp;
    public int Hp
    {
        set { mHp = value; }
        get { return mHp; }
    }
    [SerializeField]
    private int mDamage;
    public int Damage
    {
        set { mDamage = value; }
        get { return mDamage; }
    }
    [SerializeField]
    private int mExp;
    [SerializeField]
    private int mGold;
    public int Gold
    {
        set { mGold = value; }
        get { return mGold; }
    }
    [SerializeField]
    float mScale;
    public float Scale
    {
        set {
            mScale = value;
            gameObject.GetComponent<Transform>().localScale = new Vector3(mScale, mScale, mScale);
        }
        get { return mScale; }
    }

    [SerializeField]
    private int mMustDrop;
    public int MustDrop
    {
        set
        {
            mMustDrop = value;
        }
        get { return mMustDrop; }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }




}
