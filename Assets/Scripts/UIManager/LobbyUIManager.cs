using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LobbyUIManager : SingleToneMaker<LobbyUIManager>
{
    // Start is called before the first frame update
    #region Pannel
    [Header("일시정지")]
    [SerializeField]
    private GameObject mBackGroundPannel;
    [SerializeField]
    private bool mIsPause = false;

    [Header("메뉴")]
    [SerializeField]
    private GameObject mMenuPannel;
    [SerializeField]
    private bool mIsOpenMenuPannel = false;

    [Header("스킬창")]
    [SerializeField]
    private GameObject mSkillPannel;
    [SerializeField]
    private bool mIsOpenSkillPannel = false;

    [Header("스킬정보창")]
    [SerializeField]
    private GameObject mSkillInfoPannel;

    [Header("업적창")]
    [SerializeField]
    private GameObject mAchivePannel;
    [SerializeField]
    private bool mIsOpenAchivePannel = false;

    [Header("일일퀘스트창")]
    [SerializeField]
    private GameObject mDailyQuestPannel;
    [SerializeField]
    private bool mIsOpenDailyQuestPannel = false;

    [Header("옵션창")]
    [SerializeField]
    private GameObject mOptionPannel;
    [SerializeField]
    private bool mIsOpenOptionPannel = false;

    [Header("게임종료창")]
    [SerializeField]
    private GameObject mGameExitPannel;
    [SerializeField]
    private bool mIsOpenGameExitPannel = false;

    [Header("창고패널")]
    [SerializeField]
    private GameObject mWareHousePannel;
    [SerializeField]
    private bool mIsOpenWareHousePannel = false;

    [Header("경고창")]
    [SerializeField]
    private GameObject mAlertEnterPannel;
    [SerializeField]
    private GameObject mAlertEnterExitPannel;

    [Header("던전 입장")]
    [SerializeField]
    private GameObject mSupportItemPannel;
    [SerializeField]
    private GameObject mDeongunStartPannel;

    [Header("플레이어 정보창")]
    [SerializeField]
    private GameObject mPlayerInfoPannel;
    [SerializeField]
    private bool mIsOpenPlayerInfoPannel = false;

    [Header("BM상점")]
    [SerializeField]
    private GameObject mBMPannel;
    [SerializeField]
    private bool mIsOpenBMPannel = false;
    #endregion
    void Start()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
    public void OpenAlertEnterPannel(string _desc)
    {
        mAlertEnterPannel.transform.GetChild(2).GetComponent<Text>().text = _desc;
        mAlertEnterPannel.SetActive(true);
    }
    public void CloseAlertEnterPannel()
    {
        mAlertEnterPannel.SetActive(false);
    }
    public void OpenAlertEnterExitPannel(string _desc)
    {
        mAlertEnterExitPannel.transform.GetChild(2).GetComponent<Text>().text = _desc;
        mAlertEnterExitPannel.SetActive(true);
    }
    public void EnterAlertEnterExitPannel()
    {

    }
    public void CloseAlertEnterExitPannel()
    {
        mAlertEnterExitPannel.SetActive(false);
    }
    private void GamePause()
    {
        Time.timeScale = Convert.ToInt32(mIsPause);
        mIsPause = !mIsPause;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        mBackGroundPannel.SetActive(mIsPause);
    }
    public void ClickMenuButton()
    {
        mIsOpenMenuPannel = !mIsOpenMenuPannel;
        mMenuPannel.SetActive(mIsOpenMenuPannel);
    }
    public void ClickSkillButton()
    {
        GamePause();
        mIsOpenSkillPannel = !mIsOpenSkillPannel;
        if (mIsOpenSkillPannel)
        {
            for (int i = 0; i < 4; i++)
            {
                string weaponType = mSkillPannel.transform.GetChild(i + 5).name;
                List<GameObject> generalSkill = SkillManager.Instance.FindGeneralSkill(weaponType);
                for (int j = 0; j < generalSkill.Count; j++)
                {
                    mSkillPannel.transform.GetChild(i + 5).GetChild(j + 1).GetChild(0).GetChild(0).GetComponent<Image>().sprite =
                        Resources.Load<Sprite>("UI/SkillIcon/" + generalSkill[j].name);
                    mSkillPannel.transform.GetChild(i + 5).GetChild(j + 1).GetChild(1)
                        .gameObject.SetActive(generalSkill[j].GetComponent<Skill>().Spec.IsLocked);
                }
                List<GameObject> ultimateSkill = SkillManager.Instance.FindUltimateSkill(weaponType);
                for (int j = 0; j < ultimateSkill.Count; j++)
                {
                    mSkillPannel.transform.GetChild(i + 5).GetChild(j + 4).GetChild(0).GetChild(0).GetComponent<Image>().sprite =
                        Resources.Load<Sprite>("UI/SkillIcon/" + ultimateSkill[j].name);
                    mSkillPannel.transform.GetChild(i + 5).GetChild(j + 4).GetChild(1)
                        .gameObject.SetActive(ultimateSkill[j].GetComponent<Skill>().Spec.IsLocked);
                }
            }
        }
        mSkillPannel.SetActive(mIsOpenSkillPannel);
    }
    public void ClickAchiveButton()
    {
        GamePause();
        mIsOpenAchivePannel = !mIsOpenAchivePannel;
        mAchivePannel.SetActive(mIsOpenAchivePannel);
    }
    public void ClickDailyQuestButton()
    {
        GamePause();
        mIsOpenDailyQuestPannel = !mIsOpenDailyQuestPannel;
        mDailyQuestPannel.SetActive(mIsOpenDailyQuestPannel);
    }
    public void ClickOptionButton()
    {
        GamePause();
        mIsOpenOptionPannel = !mIsOpenOptionPannel;
        mOptionPannel.SetActive(mIsOpenOptionPannel);
    }
    public void ClickGameExitButton()
    {
        GamePause();
        mIsOpenGameExitPannel = !mIsOpenGameExitPannel;
        mGameExitPannel.SetActive(mIsOpenGameExitPannel);
    }
    public void ClickInteractionButton()
    {
        LobbyPlayerMove player = GameObject.Find("LobbyPlayer").GetComponent<LobbyPlayerMove>();
        // 플레이어가 창고와 상호작용대기중이라면
        if (player.IsTriggerInWareHouse)
        {
            GamePause();
            mIsOpenWareHousePannel = !mIsOpenWareHousePannel;
            mWareHousePannel.SetActive(mIsOpenWareHousePannel);
        }
        // 플레이어가 던전 입구와 상호작용 대기중 이라면
        if (player.IsTriggerInStartZone)
        {
            GamePause();
            mSupportItemPannel.SetActive(true);
        }
    }
    public void ClickAdvButton(bool _ok)
    {
        // 광고 보기 한 경우
        if (_ok)
        {
            // TODO : 광고 틀기, 던전 버프 랜덤 적용, 광고 횟수 차감
        }
        // 광고 보지 않은 경우
        else
        {
            mSupportItemPannel.SetActive(false);
            mDeongunStartPannel.SetActive(true);
        }
    }
    public void CloseDeongunStartPannel()
    {
        GamePause();
        mDeongunStartPannel.SetActive(false);
        // TODO : 적용되고있던 던전 버프 삭제
    }
    public void ClickPlayerInfoPannel()
    {
        mIsOpenPlayerInfoPannel = !mIsOpenPlayerInfoPannel;
        // 플레이어의 정보 받아오기
        if (mIsOpenPlayerInfoPannel)
        {
            LobbyPlayerInfo info = GameObject.Find("LobbyPlayer").GetComponent<LobbyPlayerData>().Info;
            // TODO : 업적정보 불러오기
            mPlayerInfoPannel.transform.GetChild(7).GetChild(1).GetComponent<Text>().text 
                = info.BaseHp.ToString();
            mPlayerInfoPannel.transform.GetChild(7).GetChild(3).GetComponent<Text>().text 
                = ((info.BaseATK + info.TrainingATK) * info.TrainingAddDamage).ToString();
            mPlayerInfoPannel.transform.GetChild(7).GetChild(5).GetComponent<Text>().text 
                = (info.BaseSPD * info.MoveSpeedRate).ToString();

            if (info.CurrentWeaponName != "")
                mPlayerInfoPannel.transform.GetChild(8).GetChild(1).GetChild(0).GetComponent<Image>().sprite
                    = EquipmentManager.Instance.FindWeapon(info.CurrentWeaponName).GetComponent<SpriteRenderer>().sprite;
            if (info.CurrentCostumeName != "")
                mPlayerInfoPannel.transform.GetChild(9).GetChild(1).GetChild(0).GetComponent<Image>().sprite
                    = EquipmentManager.Instance.FindCostume(info.CurrentCostumeName).GetComponent<SpriteRenderer>().sprite;
            if (info.CurrentCostumeShapeName != "")
                mPlayerInfoPannel.transform.GetChild(10).GetChild(1).GetChild(0).GetComponent<Image>().sprite
                    = EquipmentManager.Instance.FindCostume(info.CurrentCostumeShapeName).GetComponent<SpriteRenderer>().sprite;
        }
        mPlayerInfoPannel.SetActive(mIsOpenPlayerInfoPannel);
    }
    public void ClickSkillButton(string _type)
    {
        string weaponType = _type.Substring(0, 2);
        string skillType = _type.Substring(2, 1);
        int skillNum = int.Parse(_type.Substring(3, 1));
        List<GameObject> skillList = null;
        if(skillType == "g")
        {
            skillList = SkillManager.Instance.FindGeneralSkill(weaponType);
            Skill skill = skillList[skillNum].GetComponent<Skill>();
            string coolTime = "";
            mSkillInfoPannel.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>().sprite =
                Resources.Load<Sprite>("UI/SkillIcon/" + skillList[skillNum].name);
            mSkillInfoPannel.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = skill.Spec.EquipName;
            for (int i = 0; i < skillList[skillNum].GetComponent<Skill>().Spec.getSkillCoolTime().Count; i++)
            {
                coolTime += "[" + skillList[skillNum].GetComponent<Skill>().Spec.getSkillCoolTime()[i] + "초]";
            }
            mSkillInfoPannel.transform.GetChild(5).GetChild(0).GetComponent<Text>().text 
                = skill.Spec.EquipDesc + "\n" + "쿨타임 : " + coolTime;
        }
        else
        {
            skillList = SkillManager.Instance.FindUltimateSkill(weaponType);
            Skill skill = skillList[skillNum].GetComponent<Skill>();
            string coolTime = "";
            mSkillInfoPannel.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>().sprite =
                Resources.Load<Sprite>("UI/SkillIcon/" + skillList[skillNum].name);
            mSkillInfoPannel.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = skill.Spec.EquipName;
            for (int i = 0; i < skillList[skillNum].GetComponent<Skill>().Spec.getSkillCoolTime().Count; i++)
            {
                coolTime += "[" + skillList[skillNum].GetComponent<Skill>().Spec.getSkillCoolTime()[i] + "초]";
            }
            mSkillInfoPannel.transform.GetChild(5).GetChild(0).GetComponent<Text>().text 
                = skill.Spec.EquipDesc + "\n" + "쿨타임 : " + coolTime;
        }
        mSkillInfoPannel.SetActive(true);
    }
    public void CloseSkillInfoPannel()
    {
        mSkillInfoPannel.SetActive(false);
    }
    public void ClickBMButton()
    {
        mIsOpenBMPannel = !mIsOpenBMPannel;
        if (mIsOpenBMPannel)
        {
            mBMPannel.transform.GetChild(1).GetChild(1).GetComponent<Text>().text =
                GameObject.Find("LobbyPlayer").GetComponent<LobbyPlayerData>().Info.Diamond.ToString();
        }
        mBMPannel.SetActive(mIsOpenBMPannel);
    }
    public void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
