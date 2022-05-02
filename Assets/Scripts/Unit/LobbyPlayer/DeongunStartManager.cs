using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeongunStartManager : MonoBehaviour
{
    [SerializeField]
    private WarInfo mWarInfo = new WarInfo();
    [SerializeField]
    private GameObject mSurvivalModeCheck;
    [SerializeField]
    private bool mIsClickedSurvivalModeCheck;
    [SerializeField]
    private GameObject mBossModeCheck;
    [SerializeField]
    private bool mIsClickedBossModeCheck;
    [SerializeField]
    private DeongunBuffType currentBuffType = DeongunBuffType.None;
    public enum DeongunBuffType
    {
        None,
    }
    public enum GameMode
    {
        None,
        SurvivalMode,
        BossMode
    }




    public void ClickSirvivalModeButton()
    {
        mIsClickedSurvivalModeCheck = !mIsClickedSurvivalModeCheck;
        mSurvivalModeCheck.transform.GetChild(0).gameObject.SetActive(mIsClickedSurvivalModeCheck);
        if (mIsClickedBossModeCheck)
        {
            mIsClickedBossModeCheck = false;
            mBossModeCheck.transform.GetChild(0).gameObject.SetActive(mIsClickedBossModeCheck);
        }
        mWarInfo.WarMode = mIsClickedSurvivalModeCheck ? GameMode.SurvivalMode : GameMode.None;
    }
    public void ClickBossModeButton()
    {
        mIsClickedBossModeCheck = !mIsClickedBossModeCheck;
        mBossModeCheck.transform.GetChild(0).gameObject.SetActive(mIsClickedBossModeCheck);
        if (mIsClickedSurvivalModeCheck)
        {
            mIsClickedSurvivalModeCheck = false;
            mSurvivalModeCheck.transform.GetChild(0).gameObject.SetActive(mIsClickedSurvivalModeCheck);
        }   
        mWarInfo.WarMode = mIsClickedBossModeCheck ? GameMode.BossMode : GameMode.None;
    }
    public void ClickGameStart()
    {
        if(mWarInfo.WarMode == GameMode.None)
        {
            LobbyUIManager.Instance.OpenAlertEnterPannel("모드 선택이 올바르지 않습니다.");
        }
        else if(mWarInfo.WarMode == GameMode.BossMode)
        {
            LobbyUIManager.Instance.OpenAlertEnterPannel("생존 모드에서 드래곤을 제외한 모든 보스 처치를 완료해야합니다.");
        }
        else
        {
            LobbyPlayerInfo playerData = GameObject.Find("LobbyPlayer").GetComponent<LobbyPlayerData>().Info;
            mWarInfo.WarHp = playerData.BaseHp + playerData.TrainingHp;
            mWarInfo.WarDamage = (int)((playerData.BaseATK + playerData.TrainingATK) * playerData.TrainingAddDamage);
            mWarInfo.WarMoveSpeed = playerData.BaseSPD * playerData.MoveSpeedRate;
            mWarInfo.WarDiamond = playerData.Diamond;
            mWarInfo.WarWeaponName = playerData.CurrentWeaponName;
            mWarInfo.WarCostumeName = playerData.CurrentCostumeName;
            mWarInfo.WarCostumeShapeName = playerData.CurrentCostumeShapeName;
            mWarInfo.WarBuff = currentBuffType;
            SaveLoadManager.Instance.SavePlayerWarData(mWarInfo);
            // 스테미너 감소
            playerData.Stemina--;
            // 전투 씬 로드
            SceneManager.LoadScene("SampleScene");
        }
    }
}
