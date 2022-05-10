using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeChallenge : Achievement
{
    public override void CheckComplete()
    {
        base.CheckComplete();

        mCurrentValue = mReward.Player.GetComponent<LobbyPlayerData>().AchieveData.ModeClear[mFirstIntCondition];

        if (mCurrentValue >= mSecondIntCondition)
        {
            mCurrentValue = mSecondIntCondition;
            WaitForCompleted();
        }
    }
}
