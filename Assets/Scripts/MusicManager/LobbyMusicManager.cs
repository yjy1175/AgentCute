using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource mBackGroundMusic;
    [SerializeField]
    private bool mIsBackGroundMusicMute;
    [SerializeField]
    private GameObject mBackOn;
    [SerializeField]
    private GameObject mBackOff;
    private void Awake()
    {

    }
    public void MuteBackGroundMusic(bool _mute)
    {
        mIsBackGroundMusicMute = _mute;
        mBackGroundMusic.mute = mIsBackGroundMusicMute;
        mBackOn.transform.GetChild(0).gameObject.SetActive(!mIsBackGroundMusicMute);
        mBackOff.transform.GetChild(0).gameObject.SetActive(mIsBackGroundMusicMute);
    }
}
