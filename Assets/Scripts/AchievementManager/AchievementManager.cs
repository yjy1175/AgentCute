using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : SingleToneMaker<AchievementManager>
{
    public enum AchievementType
    {
        SurvivorTime,
        GoldCollect,
        MonsterKill,
        BossKill,
        ModeClear,
        WaveClear,
        CostumeCollect,
    }

    [SerializeField]
    private StringGameObject mAchievements;

    private void Start()
    {
        InitAchivementData();
    }
    private void InitAchivementData()
    {

    }
    public Achievement FindNextAchievement(Achievement _prev)
    {
        foreach(string key in mAchievements.Keys)
        {
            if(mAchievements[key].GetComponent<Achievement>().PreviousID == _prev.AchievementID)
            {
                return mAchievements[key].GetComponent<Achievement>();
            }
        }
        return null;
    }
}
