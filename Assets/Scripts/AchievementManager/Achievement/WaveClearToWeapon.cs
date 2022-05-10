using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveClearToWeapon : Achievement
{
    public override void CheckComplete()
    {
        base.CheckComplete();
        if (mFirstIntCondition == -1)
            for (int i = 0; i < mReward.Player.GetComponent<LobbyPlayerData>().AchieveData.WaveClearToWeapon.Count; i++)
                mCurrentValue += mReward.Player.GetComponent<LobbyPlayerData>().AchieveData.WaveClearToWeapon[i][mSecondIntCondition];
        else if (mFirstIntCondition == 4)
        {
            mCurrentValue += mReward.Player.GetComponent<LobbyPlayerData>().AchieveData.WaveClearToWeapon[0][mSecondIntCondition];
            mCurrentValue += mReward.Player.GetComponent<LobbyPlayerData>().AchieveData.WaveClearToWeapon[1][mSecondIntCondition];
        }
        else if (mFirstIntCondition == 5)
        {
            mCurrentValue += mReward.Player.GetComponent<LobbyPlayerData>().AchieveData.WaveClearToWeapon[2][mSecondIntCondition];
            mCurrentValue += mReward.Player.GetComponent<LobbyPlayerData>().AchieveData.WaveClearToWeapon[3][mSecondIntCondition];
        }
        else
            mCurrentValue = mReward.Player.GetComponent<LobbyPlayerData>().AchieveData.WaveClearToWeapon[mFirstIntCondition][mSecondIntCondition];

        if (mCurrentValue >= mThirdIntCondition)
        {
            mCurrentValue = mThirdIntCondition;
            WaitForCompleted();
        }
    }
}
