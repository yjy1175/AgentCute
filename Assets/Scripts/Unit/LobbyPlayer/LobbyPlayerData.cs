using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            GetComponent<LobbyPlayerEventHendler>().ChangeGoods(mInfo.Gold, mInfo.Diamond, mInfo.Stemina);
        }
    }
    private void Start()
    {
        Info = SaveLoadManager.Instance.LoadBaseData();
        initEquip();
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
    }
}
