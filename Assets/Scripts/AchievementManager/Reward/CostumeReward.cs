using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumeReward : Reward
{
    public override void Set()
    {
        base.Set();
        mIcon = EquipmentManager.Instance.FindCostume(mRewardName).GetComponent<SpriteRenderer>().sprite;
    }
    public override void Give()
    {
        mPlayer.GetComponent<LobbyPlayerData>().Info.Costumelock[mRewardName] = false;
        EquipmentManager.Instance.FindCostume(mRewardName).GetComponent<Costume>().IsLocked = false;
        WareHouseManager.Instance.ChangeCostumeUnlock(mRewardName, false);
    }
}
