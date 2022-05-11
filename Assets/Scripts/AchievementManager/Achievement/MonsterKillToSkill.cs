using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKillToSkill : Achievement
{
    public override void CheckComplete()
    {
        base.CheckComplete();
        mCurrentValue = mReward.Player.GetComponent<LobbyPlayerData>().AchieveData.KillToSkill[mSkillCondition];

        if (mCurrentValue >= mSecondIntCondition)
        {
            mCurrentValue = mSecondIntCondition;
            WaitForCompleted();
        }
    }
}
