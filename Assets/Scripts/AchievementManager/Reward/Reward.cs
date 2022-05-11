using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Reward : MonoBehaviour
{
    [SerializeField]
    protected GameObject mPlayer;
    public GameObject Player => mPlayer;
    [SerializeField]
    protected string mRewardName;
    public string RewardName
    {
        get => mRewardName;
        set
        {
            mRewardName = value;
        }
    }
    [SerializeField]
    protected int mRewardQuantity;
    public int RewardQuantity
    {
        get => mRewardQuantity;
        set
        {
            mRewardQuantity = value;
        }
    }
    [SerializeField]
    protected Sprite mIcon;
    public Sprite Icon => mIcon;
    public  virtual void Set()
    {
        mPlayer = GameObject.Find("LobbyPlayer");
    }
    public abstract void Give();
}
