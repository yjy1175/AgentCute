using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumeReward : Reward
{
    protected override void Awake()
    {
        base.Awake();
        mIcon = EquipmentManager.Instance.FindCostume(mRewardName).GetComponent<SpriteRenderer>().sprite;
    }
    public override void Give()
    {
        mPlayer.GetComponent<LobbyPlayerData>().Info.Costumelock[mRewardName] = false;
        EquipmentManager.Instance.FindCostume(mRewardName).GetComponent<Weapon>().IsLocked = false;
        WareHouseManager.Instance.ChangeCostumeUnlock(mRewardName, false);
    }
}
