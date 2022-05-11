using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Achievement : MonoBehaviour
{
    private const int NONE = -999;
    public enum AState
    {
        Unactive,
        Inactive,
        WaitForComplete,
        Complete,
    }
    [Header("Info")]
    [SerializeField]
    protected string mAchievementID;
    public string AchievementID
    {
        get => mAchievementID;
        set
        {
            mAchievementID = value;
        }
    }
    [SerializeField]
    protected string mAchievementName;
    public string AchievementName
    {
        get => mAchievementName;
        set
        {
            mAchievementName = value;
        }
    }
    [SerializeField]
    protected string mAchievementDesc;
    public string AchievementDesc
    {
        get => mAchievementDesc;
        set
        {
            mAchievementDesc = value;
        }
    }
    [SerializeField]
    protected AState mState = AState.Unactive;
    public AState State
    {
        get => mState;
        set
        {
            mState = value;
        }
    }

    [Header("Previous")]
    [SerializeField]
    protected string mPreviousID;
    public string PreviousID
    {
        get => mPreviousID;
        set
        {
            mPreviousID = value;
        }
    }


    [Header("Condition")]
    [SerializeField]
    protected int mFirstIntCondition;
    public int FirstIntCondition
    {
        get => mFirstIntCondition;
        set
        {
            mFirstIntCondition = value;
        }
    }
    [SerializeField]
    protected int mSecondIntCondition;
    public int SecondIntCondition
    {
        get => mSecondIntCondition;
        set
        {
            mSecondIntCondition = value;
        }
    }
    [SerializeField]
    protected int mThirdIntCondition;
    public int ThirdIntCondition
    {
        get => mThirdIntCondition;
        set
        {
            mThirdIntCondition = value;
        }
    }
    [SerializeField]
    protected string mSkillCondition;
    public string SkillCondition
    {
        get => mSkillCondition;
        set
        {
            mSkillCondition = value;
        }
    }
    [SerializeField]
    protected int mCurrentValue = 0;
    public int CurrentValue => mCurrentValue;

    [Header("Reward")]
    [SerializeField]
    protected Reward mReward;
    public Reward Reward
    {
        get => mReward;
        set
        {
            mReward = value;
        }
    }
    public void WaitForCompleted()
    {
        if (mState == AState.Inactive)
            mState = AState.WaitForComplete;
    }
    public void Completed()
    {
        if (mState != AState.Complete)
        {
            mState = AState.Complete;
            mReward.Give();
            NextAchievementActive();
        }
    }
    public void Inactive()
    {
        if (mState != AState.Inactive)
            mState = AState.Inactive;
    }
    public virtual void CheckComplete()
    {
        mCurrentValue = 0;
        if (mState == AState.Complete || mState == AState.Unactive || mState == AState.WaitForComplete)
            return;
    }
    private void NextAchievementActive()
    {
        if (AchievementManager.Instance.FindNextAchievement(this) is null)
            return;
        AchievementManager.Instance.FindNextAchievement(this).Inactive();
    }

    public int FinalCondition()
    {
        if (mSecondIntCondition == NONE)
            return mFirstIntCondition;
        else if (mThirdIntCondition == NONE)
            return mSecondIntCondition;
        else
            return mThirdIntCondition;
    }
}
