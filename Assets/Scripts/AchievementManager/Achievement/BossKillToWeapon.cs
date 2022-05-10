using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKillToWeapon : Achievement
{
    public override void CheckComplete()
    {
        base.CheckComplete();
        if (mFirstIntCondition == -1)
            for (int i = 0; i < mReward.Player.GetComponent<LobbyPlayerData>().AchieveData.BossKillToWeapon.Count; i++)
                mCurrentValue += mReward.Player.GetComponent<LobbyPlayerData>().AchieveData.BossKillToWeapon[i][mSecondIntCondition];
        else
            mCurrentValue = mReward.Player.GetComponent<LobbyPlayerData>().AchieveData.BossKillToWeapon[mFirstIntCondition][mSecondIntCondition];

        if (mCurrentValue >= mThirdIntCondition)
        {
            mCurrentValue = mThirdIntCondition;
            WaitForCompleted();
        }
    }
}
