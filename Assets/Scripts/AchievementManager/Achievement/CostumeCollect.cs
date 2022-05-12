using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumeCollect : Achievement
{
    public override void CheckComplete()
    {
        base.CheckComplete();
        StringBoolean checkDic = mReward.Player.GetComponent<LobbyPlayerData>().Info.Costumelock;
        foreach (string key in checkDic.Keys)
        {
            if (!checkDic[key])
            {
                mCurrentValue++;
            }

        }

        if(mCurrentValue >= mFirstIntCondition)
        {
            mCurrentValue = mFirstIntCondition;
            WaitForCompleted();
        }

    }
}
