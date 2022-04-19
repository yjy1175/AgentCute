using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    int mHp;
    public int Hp
    {
        set { mHp = value; }
        get { return mHp; }
    }
    [SerializeField]
    int mDamage;
    public int Damage
    {
        set { mDamage = value; }
        get { return mDamage; }
    }
    [SerializeField]
    int mExp;
    [SerializeField]
    int mGold;
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
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }




}
