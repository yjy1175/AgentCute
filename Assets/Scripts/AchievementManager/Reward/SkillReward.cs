using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillReward : Reward
{
    protected override void Awake()
    {
        base.Awake();
        mIcon = Resources.Load<Sprite>("UI/SkillIcon/" + mRewardName);
    }
    public override void Give()
    {
        mPlayer.GetComponent<LobbyPlayerData>().Info.Skilllock[mRewardName] = false;
        SkillManager.Instance.FindSkill(mRewardName).GetComponent<Skill>().Spec.IsLocked = false;
    }
}
