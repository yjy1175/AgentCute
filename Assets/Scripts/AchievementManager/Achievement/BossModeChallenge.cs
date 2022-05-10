using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossModeChallenge : Achievement
{
    public override void CheckComplete()
    {
        base.CheckComplete();
        mCurrentValue = mReward.Player.GetComponent<LobbyPlayerData>().AchieveData.BossModeWaveClear[FirstIntCondition];

        if (mCurrentValue >= mSecondIntCondition)
        {
            mCurrentValue = mSecondIntCondition;
            WaitForCompleted();
        }
    }
}
