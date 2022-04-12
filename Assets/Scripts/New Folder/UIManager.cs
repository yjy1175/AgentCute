using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingleToneMaker<UIManager>
{
    #region variables
    [Header("일시정지")]
    [SerializeField]
    private bool mIsPause;
    [SerializeField]
    private GameObject mPauseBtn;
    [SerializeField]
    private GameObject mPausePannel;
    [SerializeField]
    private GameObject mStatusPannel;
    [SerializeField]
    private GameObject mBackGroundPannel;

    [Header("옵션")]
    [SerializeField]
    private bool mIsOption;
    [SerializeField]
    private GameObject mOptionPannel;
    [SerializeField]
    private GameObject mExitBtn;

    [Header("전투포기")]
    [SerializeField]
    private bool mIsGiveup;
    [SerializeField]
    private GameObject mGiveupPannel;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        mIsPause = false;
        mIsOption = false;
        mIsGiveup = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region method
    /*
     *  Todo : 만들다보니 비슷한 기능들이여서 추후에 리팩토링필요
     */
    public void ClickedGamePause()
    {
        // 일시정지 상태에서 클릭
        if (mIsPause)
        {
            // 진행
            Time.timeScale = 1;
            mPausePannel.SetActive(false);
            mStatusPannel.SetActive(false);
            mPauseBtn.SetActive(true);
            mBackGroundPannel.SetActive(false);
        }
        // 진행중인 상태에서 클릭
        else
        {
            // 일시정지
            Time.timeScale = 0;
            mPausePannel.SetActive(true);
            mStatusPannel.SetActive(true);
            mPauseBtn.SetActive(false);
            mBackGroundPannel.SetActive(true);
        }
        mIsPause = !mIsPause;
    }
    public void ClickedOption()
    {
        // 옵션창이 켜진 상태
        if (mIsOption)
        {
            mOptionPannel.SetActive(false);
            mExitBtn.SetActive(false);
        }
        // 옵션창이 꺼진 상태
        else
        {
            mOptionPannel.SetActive(true);
            mExitBtn.SetActive(true);
        }
        mIsOption = !mIsOption;
    }
    public void ClickedGiveup()
    {
        // 옵션창이 켜진 상태
        if (mIsGiveup)
        {
            mGiveupPannel.SetActive(false);
        }
        // 옵션창이 꺼진 상태
        else
        {
            mGiveupPannel.SetActive(true);
        }
        mIsGiveup = !mIsGiveup;
    }
    #endregion
}
