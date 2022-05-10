using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCollect : Achievement
{
    public override void CheckComplete()
    {
        base.CheckComplete();
        mCurrentValue = mReward.Player.GetComponent<LobbyPlayerData>().Info.Gold;

        if (mCurrentValue >= mFirstIntCondition)
        {
            mCurrentValue = mFirstIntCondition;
            WaitForCompleted();
        }
    }
}
