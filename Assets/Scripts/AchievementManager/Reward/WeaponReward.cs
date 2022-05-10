using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReward : Reward
{
    protected override void Awake()
    {
        base.Awake();
        mIcon = EquipmentManager.Instance.FindWeapon(mRewardName).GetComponent<SpriteRenderer>().sprite;
    }
    public override void Give()
    {
        mPlayer.GetComponent<LobbyPlayerData>().Info.Weaponlock[mRewardName] = false;
        EquipmentManager.Instance.FindWeapon(mRewardName).GetComponent<Weapon>().IsLocked = false;
        WareHouseManager.Instance.ChangeWeaponUnlock(mRewardName, false);
    }
}
