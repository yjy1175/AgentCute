using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LobbyPlayerData : MonoBehaviour
{
    [SerializeField]
    private LobbyPlayerInfo mInfo;
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
}
