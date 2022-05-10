using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Achievement : MonoBehaviour
{
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
    protected AState mState;
    public AState State => mState;

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
    protected int mCurrentValue;
    public int CurrentValue => mCurrentValue;

    [Header("Reward")]
    [SerializeField]
    protected Reward mReward;

    public void WaitForCompleted()
    {
        if (mState != AState.WaitForComplete)
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
        if (mState == AState.Complete || mState == AState.Unactive)
            return;
    }
    private void NextAchievementActive()
    {
        if (AchievementManager.Instance.FindNextAchievement(this) is null)
            return;
        AchievementManager.Instance.FindNextAchievement(this).Inactive();
    }
}
