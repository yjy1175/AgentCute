using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    [SerializeField]
    private Text mProjectileCountText;
    [SerializeField]
    private Text mProjectileScaleText;
    [SerializeField]
    private Text mPassCountText;

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

    [Header("능력치선택창")]
    [SerializeField]
    private GameObject mStatusSelectPannel;
    [SerializeField]
    private Text mFirstSelectText;
    [SerializeField]
    private Text mSecondSelectText;
    [SerializeField]
    private Text mThirdSelectText;

    [Header("게임오버")]
    [SerializeField]
    private GameObject mGameOverPannel;
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
        mProjectileCountText.text = "+" + Projectile.AddProjectilesCount.ToString() + "개";
        mProjectileScaleText.text = "+" + (Projectile.AddScaleCoefficient - 1.0f).ToString() + "%";
        mPassCountText.text = "+" + Projectile.AddPassCount.ToString() + "마리";
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
    public void StatusSelectPannelOn()
    {
        // 일시 정지
        Time.timeScale = 0;
        mPauseBtn.SetActive(false);
        mBackGroundPannel.SetActive(true);
        // TO-DO : 각 선택 섹션 별로 능력치 저장 후 랜덤하게 등장하게 구현
        mFirstSelectText.text = "발사체 개수 증가 +1";
        mSecondSelectText.text = "발사체 범위 증가 +10%";
        mThirdSelectText.text = "몬스터 관통 수 증가 +1";
        mStatusSelectPannel.SetActive(true);
    }
    public void ClickedSelectStatus(int _num)
    {
        // 추후에 PlayerStatus를 통하여 수치 조정
        switch (_num)
        {
            case 0:
                ProjectileManager.Instance.AddProjectilesCount(1);
                break;
            case 1:
                ProjectileManager.Instance.AddProjectilesScale(0.1f);
                break;
            case 2:
                ProjectileManager.Instance.AddPassCount(1);
                break;
        }

        // 게임 재개
        Time.timeScale = 1;
        mPauseBtn.SetActive(true);
        mBackGroundPannel.SetActive(false);
        mStatusSelectPannel.SetActive(false);
    }

    public void GameOverPannelOn()
    {
        // 일시 정지
        Time.timeScale = 0;
        mPauseBtn.SetActive(false);
        mBackGroundPannel.SetActive(true);

        // 게임오버 패널 오픈
        mGameOverPannel.SetActive(true);
    }

    public void ClickGameReload()
    {
        // 씬 리로드
        SceneManager.LoadScene(0);
    }
    #endregion
}
