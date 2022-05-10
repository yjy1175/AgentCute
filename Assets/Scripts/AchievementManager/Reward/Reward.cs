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
    [SerializeField]
    protected int mRewardQuantity;
    [SerializeField]
    protected Sprite mIcon;
    public Sprite Icon => mIcon;
    protected virtual void Awake()
    {
        mPlayer = GameObject.Find("LobbyPlayer");
    }
    public abstract void Give();
}
