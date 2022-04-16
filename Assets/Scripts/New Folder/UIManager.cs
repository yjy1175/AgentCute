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


    [Header("테스트 로비")]
    [SerializeField]
    private GameObject mTestLobbyPannel;
    [SerializeField]
    private GameObject mBaseSkill;
    [SerializeField]
    private List<GameObject> mGeneralSkillBtn = new List<GameObject>();
    [SerializeField]
    private List<GameObject> mGeneralSkill = new List<GameObject>();
    [SerializeField]
    private List<GameObject> mUltimateSkillBtn = new List<GameObject>();
    [SerializeField]
    private List<GameObject> mUltimateSkill = new List<GameObject>();
    [SerializeField]
    private GameObject mSkillInfoPannel;
    [SerializeField]
    private Text mSkillInfoNameText;
    [SerializeField]
    private Text mSkillInfoTypeText;
    [SerializeField]
    private Text mSkillInfoCoolTimeText;
    [SerializeField]
    private Text mSkillInfoDescText;
    private bool mIsGSkillSelect = false;
    private bool mIsUSkillSelect = false;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        GamePause();
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
            GameRestart();
            mPausePannel.SetActive(false);
            mStatusPannel.SetActive(false);
        }
        // 진행중인 상태에서 클릭
        else
        {
            // 일시정지
            GamePause();
            mPausePannel.SetActive(true);
            mStatusPannel.SetActive(true);
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
        GamePause();
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
        GameRestart();
        mStatusSelectPannel.SetActive(false);
    }
    public void GameOverPannelOn()
    {
        // 일시 정지
        GamePause();

        // 게임오버 패널 오픈
        mGameOverPannel.SetActive(true);
    }
    public void ClickGameReload()
    {
        // 씬 리로드
        // 현재 오류로 리로드 불가...
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // 우선 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
    private void GamePause()
    {
        Time.timeScale = 0;
        mPauseBtn.SetActive(false);
        mBackGroundPannel.SetActive(true);
    }
    private void GameRestart()
    {
        Time.timeScale = 1;
        mPauseBtn.SetActive(true);
        mBackGroundPannel.SetActive(false);
    }
    public void ClickWeaponSelect(string _type)
    {
        int ran = Random.Range(0, 5);
        List<GameObject> newWeaponList = EquipmentManager.Instance.FindWepaonList(_type);
        EquipmentManager.Instance.ChangeWeapon(newWeaponList[ran].GetComponent<Weapon>().Spec.Type);
        mBaseSkill = SkillManager.Instance.FindBaseSkill(_type);
        mGeneralSkill = SkillManager.Instance.FindGeneralSkill(_type);
        mUltimateSkill = SkillManager.Instance.FindUltimateSkill(_type);
        for (int i = 0; i < mGeneralSkillBtn.Count; i++)
        {
            mGeneralSkillBtn[i].transform.GetChild(0).GetComponent<Image>().sprite =
                ProjectileManager.
                Instance.allProjectiles[mGeneralSkill[i].GetComponent<Skill>().Spec.getProjectiles()[0]].
                GetComponent<SpriteRenderer>().sprite;
        }
        for (int i = 0; i < mUltimateSkillBtn.Count; i++)
        {
            mUltimateSkillBtn[i].transform.GetChild(0).GetComponent<Image>().sprite =
                ProjectileManager.
                Instance.allProjectiles[mUltimateSkill[i].GetComponent<Skill>().Spec.getProjectiles()[0]].
                GetComponent<SpriteRenderer>().sprite;
        }
    }
    public void OverSkillSelectBtn(string _type)
    {
        string type = _type.Substring(0, 1);
        int num = int.Parse(_type.Substring(1, 1));
        switch (type)
        {
            case "G":
                SettingInfoPannel(mGeneralSkill, num);
                break;
            case "U":
                SettingInfoPannel(mUltimateSkill, num);
                break;
        }
    }
    public void OutSKillSelectBtn()
    {
        mSkillInfoPannel.SetActive(false);
    }
    private void SettingInfoPannel(List<GameObject> _skillList, int _num)
    {
        if(_skillList.Count > 0)
        {
            mSkillInfoNameText.text = _skillList[_num].GetComponent<Skill>().Spec.Name;
            mSkillInfoTypeText.text = _skillList[_num].GetComponent<Skill>().Spec.TypeName;
            mSkillInfoCoolTimeText.text = "";
            for (int i = 0; i < _skillList[_num].GetComponent<Skill>().Spec.getSkillCoolTime().Count; i++)
            {
                mSkillInfoCoolTimeText.text += "[" + _skillList[_num].GetComponent<Skill>().Spec.getSkillCoolTime()[i] + "초]";
            }
            mSkillInfoDescText.text = _skillList[_num].GetComponent<Skill>().Spec.Desc;

            mSkillInfoPannel.SetActive(true);
        }
    }
    public void ClickSkillSelectBtn(string _type)
    {
        string type = _type.Substring(0, 1);
        int num = int.Parse(_type.Substring(1, 1));
        switch (type)
        {
            case "G":
                GameObject.Find("PlayerObject").GetComponent<PlayerAttack>().CurrentGeneralSkill = mGeneralSkill[num].GetComponent<Skill>();
                for(int i = 0; i < mGeneralSkillBtn.Count; i++)
                {
                    if (i == num)
                        mGeneralSkillBtn[i].transform.GetChild(1).gameObject.SetActive(true);
                    else
                        mGeneralSkillBtn[i].transform.GetChild(1).gameObject.SetActive(false);
                }
                mIsGSkillSelect = true;
                break;
            case "U":
                GameObject.Find("PlayerObject").GetComponent<PlayerAttack>().CurrentUltimateSkill = mUltimateSkill[num].GetComponent<Skill>();
                for (int i = 0; i < mUltimateSkill.Count; i++)
                {
                    if (i == num)
                        mUltimateSkillBtn[i].transform.GetChild(1).gameObject.SetActive(true);
                    else
                        mUltimateSkillBtn[i].transform.GetChild(1).gameObject.SetActive(false);
                }
                mIsUSkillSelect = true;
                break;
        }
    }
    public void ClickGameStart()
    {
        if(mIsGSkillSelect && mIsUSkillSelect)
        {
            GameObject.Find("PlayerObject").GetComponent<PlayerAttack>().CurrentBaseSkill = mBaseSkill.GetComponent<Skill>();
            // 플레이매니저에서 스타트 API호출
            PlayerManager.Instance.SettingGameStart();
            GameRestart();
            mTestLobbyPannel.SetActive(false);
        }
    }
    #endregion
}
