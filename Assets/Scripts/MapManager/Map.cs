using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField]
    private int mID;
    public int ID
    {
        set
        {
            mID = value;
        }
        get
        {
            return mID;
        }
    }
    [SerializeField]
    private int mWidth;
    public int Width
    {
        set
        {
            mWidth = value;
        }
        get
        {
            return mWidth;
        }
    }
    [SerializeField]
    private int mHeight;
    public int Height
    {
        set
        {
            mHeight = value;
        }
        get
        {
            return mHeight;
        }
    }
    [SerializeField]
    private bool mIsBossRelay;
    public bool IsBossRelay
    {
        get { return mIsBossRelay; }
        set { mIsBossRelay = value; }
    }
}
