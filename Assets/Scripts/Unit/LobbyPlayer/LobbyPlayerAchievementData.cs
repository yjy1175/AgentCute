using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LobbyPlayerAchievementData
{
    // 전체적인 업적 달성도
    [SerializeField]
    private List<Tuple<string, Achievement.AState>> mProgress;
    public List<Tuple<string, Achievement.AState>> Progress
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
    private List<List<int>> mBossKillToWeapon;
    public List<List<int>> BossKillToWeapon
    {
        get => mBossKillToWeapon;
        set
        {
            mBossKillToWeapon = value;
        }
    }
    // 보스 처치(코스튬)
    [SerializeField]
    private List<List<int>> mBossKillToCostume;
    public List<List<int>> BossKillToCostume
    {
        get => mBossKillToCostume;
        set
        {
            mBossKillToCostume = value;
        }
    }

    // 웨이브 클리어(무기)
    [SerializeField]
    private List<List<int>> mWaveClearToWeapon;
    public List<List<int>> WaveClearToWeapon
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
