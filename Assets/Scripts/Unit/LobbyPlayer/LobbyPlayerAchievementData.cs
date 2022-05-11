using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class LobbyPlayerAchievementData
{
    // 전체적인 업적 달성도
    [SerializeField]
    private StringState mProgress;
    public StringState Progress
    {
        get => mProgress;
        set
        {
            mProgress = value;
        }
    }
    // 모드 클리어
    [SerializeField]
    private List<int> mModeClear;
    public List<int> ModeClear
    {
        get => mModeClear;
        set
        {
            mModeClear = value;
        }
    }

    // 처치(무기)
    [SerializeField]
    private List<int> mKillToWeapon;
    public List<int> KillToWeapon
    {
        get => mKillToWeapon;
        set
        {
            mKillToWeapon = value;
        }
    }

    // 처치(스킬)
    [SerializeField]
    private StringInt mKillToSkill;
    public StringInt KillToSkill
    {
        get => mKillToSkill;
        set
        {
            mKillToSkill = value;
        }
    }

    // 생존 시간(무기)
    [SerializeField]
    private List<int> mTimeToWeapon;
    public List<int> TimeToWeapon
    {
        get => mTimeToWeapon;
        set
        {
            mTimeToWeapon = value;
        }
    }
    // 생존 시간(코스튬)
    [SerializeField]
    private List<int> mTimeToCostume;
    public List<int> TimeToCostume
    {
        get => mTimeToCostume;
        set
        {
            mTimeToCostume = value;
        }
    }

    // 보스 처치(무기)
    [SerializeField]
    private List<IntInt> mBossKillToWeapon;
    public List<IntInt> BossKillToWeapon
    {
        get => mBossKillToWeapon;
        set
        {
            mBossKillToWeapon = value;
        }
    }
    // 보스 처치(코스튬)
    [SerializeField]
    private List<IntInt> mBossKillToCostume;
    public List<IntInt> BossKillToCostume
    {
        get => mBossKillToCostume;
        set
        {
            mBossKillToCostume = value;
        }
    }

    // 웨이브 클리어(무기)
    [SerializeField]
    private List<IntInt> mWaveClearToWeapon;
    public List<IntInt> WaveClearToWeapon
    {
        get => mWaveClearToWeapon;
        set
        {
            mWaveClearToWeapon = value;
        }
    }

    // 보스 모드 클리어
    [SerializeField]
    private List<int> mBossModeWaveClear;
    public List<int> BossModeWaveClear
    {
        get => mBossModeWaveClear;
        set
        {
            mBossModeWaveClear = value;
        }
    }
}
