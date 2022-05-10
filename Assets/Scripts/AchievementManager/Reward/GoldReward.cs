using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldReward : Reward
{
    protected override void Awake()
    {
        base.Awake();
        mIcon = Resources.Load<Sprite>("UI/UIAsset/" + mRewardName);
    }
    public override void Give()
    {
        mPlayer.GetComponent<LobbyPlayerData>().Info.Gold += mRewardQuantity;
    }
}
