using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    #endregion
    void Start()
    {
        
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
