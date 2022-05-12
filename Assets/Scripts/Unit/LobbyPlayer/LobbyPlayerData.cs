using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class LobbyPlayerData : MonoBehaviour
{
    [SerializeField]
    private LobbyPlayerInfo mInfo = null;
    public LobbyPlayerInfo Info
    {
        get { return mInfo; }
        set 
        { 
            mInfo = value;
            GetComponent<LobbyPlayerEventHendler>().ChangeMoveSpeed(mInfo.BaseSPD * mInfo.MoveSpeedRate);
            GetComponent<LobbyPlayerEventHendler>().ChangeGoods(mInfo.Gold, mInfo.Diamond);
        }
    }

    [SerializeField]
    private LobbyPlayerAchievementData mAchieveData = null;
    public LobbyPlayerAchievementData AchieveData 
    {
        get => mAchieveData;
        set
        {
            mAchieveData = value;
        }
    }

    [SerializeField]
    private bool mIsChangedDate;
    private void Start()
    {
        Info = SaveLoadManager.Instance.LoadBaseData();
        AchieveData = SaveLoadManager.Instance.LoadAchieveData();
        AchievementManager.Instance.SaveDataLoadFromAchievement();
        DailyAdCountCheck();
        initEquip();
    }
    // 저장된 날짜를 확인하여 디바이스 시간과 비교해서 광고 횟수 충전
    public void DailyAdCountCheck()
    {
        StartCoroutine(WebChk());
    }
    // 웹에서 시간을 가져오는 코루틴 함수
    IEnumerator WebChk()
    {
        string url = "www.naver.com";
        UnityWebRequest request = new UnityWebRequest();
        using(request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                // 인터넷 연결 오류
                LobbyUIManager.Instance.OpenAlertEnterPannel("인터넷 연결이 필요합니다.\n강제로 진행 시 불이익이 생길 수 있습니다.");
            }
            else
            {
                string date = request.GetResponseHeader("date");
                DateTime dateTime = DateTime.Parse(date).ToUniversalTime();
                mIsChangedDate = dateTime.Day != Info.Date;
            }
        }
        if (mIsChangedDate)
        {
            // 날짜가 바뀌었다면 최대 시청횟수로 변경
            Info.DailyAddCount = 3;
        }
    }
    public void  initEquip()
    {
        if(mInfo.CurrentWeaponName != "")
        {
            EquipmentManager.Instance.ChangeWeaponLobbyPlayer(mInfo.CurrentWeaponName);
        }
        if(mInfo.CurrentCostumeName != "")
        {
            EquipmentManager.Instance.ChangeCostumeLobbyPlayer(mInfo.CurrentCostumeName);
        }
        if(mInfo.CurrentCostumeShapeName != "")
        {
            EquipmentManager.Instance.ChangeCostumeShapeLobbyPlayer(mInfo.CurrentCostumeShapeName);
        }

        // 무기 해금 정보 수정
        foreach(string key in mInfo.Weaponlock.Keys)
        {
            EquipmentManager.Instance.FindWeapon(key).GetComponent<Weapon>().IsLocked = mInfo.Weaponlock[key];
        }
        // 코스튬 해금 정보 수정
        foreach (string key in mInfo.Costumelock.Keys)
        {
            EquipmentManager.Instance.FindCostume(key).GetComponent<Costume>().IsLocked = mInfo.Costumelock[key];
        }
        // 스킬 해금 정보 수정
        foreach (string key in mInfo.Skilllock.Keys)
        {
            SkillManager.Instance.FindSkill(key).GetComponent<Skill>().Spec.IsLocked = mInfo.Skilllock[key];
        }
    }
}
