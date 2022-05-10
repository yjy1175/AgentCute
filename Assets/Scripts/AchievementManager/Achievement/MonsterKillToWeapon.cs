using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKillToWeapon : Achievement
{
    public override void CheckComplete()
    {
        base.CheckComplete();
        if (mFirstIntCondition == -1) 
            for (int i = 0; i < mReward.Player.GetComponent<LobbyPlayerData>().AchieveData.KillToWeapon.Count; i++)
                mCurrentValue += mReward.Player.GetComponent<LobbyPlayerData>().AchieveData.KillToWeapon[i];
        else
            mCurrentValue = mReward.Player.GetComponent<LobbyPlayerData>().AchieveData.KillToWeapon[mFirstIntCondition];

        if (mCurrentValue >= mSecondIntCondition)
        {
            mCurrentValue = mSecondIntCondition;
            WaitForCompleted();
        }
    }
}
