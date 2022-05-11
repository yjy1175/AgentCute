using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class AchievementManager : SingleToneMaker<AchievementManager>
{
    private const string NULL = "null";

    [SerializeField]
    private StringGameObject mAchievements;
    public StringGameObject Achievements => mAchievements;

    [SerializeField]
    private GameObject mNoneObject;

    [SerializeField]
    private GameObject mAchievementPrefab;
    [SerializeField]
    private GameObject mAchievementContainer;
    [SerializeField]
    private StringGameObject mAchievementButtons;
    private void Start()
    {
        InitAchivementData();
    }
    private void InitAchivementData()
    {

        List<Dictionary<string, object>> achievementData = CSVReader.Read("CSVFile/Achievement");

        for(int i = 0; i < achievementData.Count; i++)
        {
            GameObject newItem = Instantiate(mNoneObject, transform);
            Type achivementType = Type.GetType(achievementData[i]["AchievementType"].ToString());
            

            string achieveID = achievementData[i]["AchievementID"].ToString();
            newItem.name = achieveID;
            Achievement newAchv = newItem.AddComponent(achivementType) as Achievement;

            newAchv.AchievementID = achieveID;
            newAchv.AchievementName = achievementData[i]["AchievementName"].ToString();
            newAchv.AchievementDesc = achievementData[i]["AchievementDesc"].ToString();

            newAchv.PreviousID = achievementData[i]["PrevAchievementID"].ToString();
            if (newAchv.PreviousID == NULL)
                newAchv.Inactive();

            try
            {
                newAchv.FirstIntCondition = int.Parse(achievementData[i]["AchievementCondition1"].ToString());
            }
            catch
            {
                newAchv.SkillCondition = achievementData[i]["AchievementCondition1"].ToString();
            }
            newAchv.SecondIntCondition = int.Parse(achievementData[i]["AchievementCondition2"].ToString());
            newAchv.ThirdIntCondition = int.Parse(achievementData[i]["AchievementCondition3"].ToString());

            Type rewardType = Type.GetType(achievementData[i]["RewardType"].ToString());
            Reward newRwd = newItem.AddComponent(rewardType) as Reward;
            newRwd.RewardName = achievementData[i]["RewardName"].ToString();
            newRwd.RewardQuantity = int.Parse(achievementData[i]["RewardQuantity"].ToString());
            newRwd.Set();
            newAchv.Reward = newRwd;

            mAchievements.Add(newItem.name, newItem);
        }
    }

    public void SaveDataLoadFromAchievement()
    {
        List<GameObject> achievementList = new List<GameObject>();
       foreach(string key in mAchievements.Keys)
        {
            achievementList.Add(mAchievements[key]);
        }
        for (int i = 0; i < achievementList.Count; i++)
        {
            achievementList[i].GetComponent<Achievement>().State =
                GameObject.Find("LobbyPlayer").GetComponent<LobbyPlayerData>()
                .AchieveData.Progress[achievementList[i].GetComponent<Achievement>().AchievementID];
        }
        UpdateAchievementUI();
    }

    public void UpdateAchievementUI()
    {
        List<GameObject> achievementList = new List<GameObject>();
        foreach (string key in mAchievements.Keys)
        {
            achievementList.Add(mAchievements[key]);
        }
        for(int i=0; i < achievementList.Count; i++)
        {
            Achievement newAchv = achievementList[i].GetComponent<Achievement>();
            GameObject newUI = Instantiate(mAchievementPrefab, mAchievementContainer.transform);

            newUI.GetComponent<AchieveInfoButton>().mDesc.transform.GetChild(0).GetComponent<Text>().text =
                newAchv.AchievementName + "\n" + newAchv.AchievementDesc;
            newUI.GetComponent<AchieveInfoButton>().mRewardIcon.transform.GetChild(0).GetComponent<Image>().sprite =
                newAchv.Reward.Icon;
            newUI.GetComponent<AchieveInfoButton>().mInactive.transform.GetChild(1).GetComponent<Text>().text =
                newAchv.FinalCondition().ToString();
            newUI.GetComponent<AchieveInfoButton>().mRewardButton.GetComponent<Button>()
                .onClick.AddListener(() => { ClickRewardButton(newAchv.AchievementID); });
            mAchievementButtons.Add(newAchv.name, newUI);
        }
        UpdateState();
    }

    public  void UpdateState()
    {
        foreach(string key in mAchievements.Keys)
        {
            mAchievements[key].GetComponent<Achievement>().CheckComplete();
            mAchievementButtons[key].GetComponent<AchieveInfoButton>().mInactive.transform.GetChild(0).GetComponent<Text>().text =
                mAchievements[key].GetComponent<Achievement>().CurrentValue.ToString();
            GameObject.Find("LobbyPlayer").GetComponent<LobbyPlayerData>()
                .AchieveData.Progress[mAchievements[key].GetComponent<Achievement>().AchievementID] = mAchievements[key].GetComponent<Achievement>().State;
            switch (mAchievements[key].GetComponent<Achievement>().State)
            {
                case Achievement.AState.Unactive:
                    mAchievementButtons[key].GetComponent<AchieveInfoButton>().Unactive();
                    break;
                case Achievement.AState.Inactive:
                    mAchievementButtons[key].GetComponent<AchieveInfoButton>().Inactive();
                    break;
                case Achievement.AState.WaitForComplete:
                    mAchievementButtons[key].GetComponent<AchieveInfoButton>().WaitForComplete();
                    break;
                case Achievement.AState.Complete:
                    mAchievementButtons[key].GetComponent<AchieveInfoButton>().Complete();
                    break;
            }
        }
    }

    public void ClickRewardButton(string _id)
    {
        mAchievements[_id].GetComponent<Achievement>().Completed();
        UpdateState();
        // 업적 데이터 세이브 하기
        SaveLoadManager.Instance.SaveAchievementData(
            GameObject.Find("LobbyPlayer").GetComponent<LobbyPlayerData>().AchieveData);
        // 유저 데이터 세이브 하기
        SaveLoadManager.Instance.SavePlayerInfoFile(
            GameObject.Find("LobbyPlayer").GetComponent<LobbyPlayerData>().Info);
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
