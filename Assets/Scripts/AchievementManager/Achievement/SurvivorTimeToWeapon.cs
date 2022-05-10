using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorTimeToWeapon : Achievement
{
    public override void CheckComplete()
    {
        base.CheckComplete();
        if (mFirstIntCondition == -1)
            for (int i = 0; i < mReward.Player.GetComponent<LobbyPlayerData>().AchieveData.TimeToWeapon.Count; i++)
                mCurrentValue += mReward.Player.GetComponent<LobbyPlayerData>().AchieveData.TimeToWeapon[i];
        else if (mFirstIntCondition == 4)
        {
            mCurrentValue += mReward.Player.GetComponent<LobbyPlayerData>().AchieveData.TimeToWeapon[0];
            mCurrentValue += mReward.Player.GetComponent<LobbyPlayerData>().AchieveData.TimeToWeapon[1];
        }
        else if (mFirstIntCondition == 5)
        {
            mCurrentValue += mReward.Player.GetComponent<LobbyPlayerData>().AchieveData.TimeToWeapon[2];
            mCurrentValue += mReward.Player.GetComponent<LobbyPlayerData>().AchieveData.TimeToWeapon[3];
        }
        else
            mCurrentValue = mReward.Player.GetComponent<LobbyPlayerData>().AchieveData.TimeToWeapon[mFirstIntCondition];

        if (mCurrentValue >= mSecondIntCondition)
        {
            mCurrentValue = mSecondIntCondition;
            WaitForCompleted();
        }
    }
}
