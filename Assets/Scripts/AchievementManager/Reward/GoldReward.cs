using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldReward : Reward
{
    public override void Set()
    {
        base.Set();
        mIcon = Resources.Load<Sprite>("UI/UIAsset/" + mRewardName);
    }
    public override void Give()
    {
        mPlayer.GetComponent<LobbyPlayerData>().Info.Gold += mRewardQuantity;
    }
}
