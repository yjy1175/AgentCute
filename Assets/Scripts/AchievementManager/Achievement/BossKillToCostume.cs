using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKillToCostume : Achievement
{
    public override void CheckComplete()
    {
        base.CheckComplete();
        mCurrentValue = mReward.Player.GetComponent<LobbyPlayerData>().AchieveData.BossKillToCostume[mFirstIntCondition][mSecondIntCondition];

        if (mCurrentValue >= mThirdIntCondition)
        {
            mCurrentValue = mThirdIntCondition;
            WaitForCompleted();
        }
    }
}
