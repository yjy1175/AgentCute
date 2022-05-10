using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorTimeToCostume : Achievement
{
    public override void CheckComplete()
    {
        base.CheckComplete();

        mCurrentValue = mReward.Player.GetComponent<LobbyPlayerData>().AchieveData.TimeToCostume[mFirstIntCondition];

        if (mCurrentValue >= mSecondIntCondition)
        {
            mCurrentValue = mSecondIntCondition;
            WaitForCompleted();
        }
    }
}
