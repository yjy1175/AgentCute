using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMusicManager : SingleToneMaker<LobbyMusicManager>
{
    private bool DEBUG = true;
    [SerializeField]
    private AudioSource mBackGroundMusic;
    [SerializeField]
    private bool mIsBackGroundMusicMute;
    [SerializeField]
    private GameObject mBackOn;
    [SerializeField]
    private GameObject mBackOff;
    [SerializeField]
    private GameObject mEFMOn;
    [SerializeField]
    private GameObject mEFMOff;

    [SerializeField]
    private AudioSource mAudioSource;
    [SerializeField]
    private UIAudoiData mAudioData;

    public enum AudioType
    {
        Cancel,
        Choice,
        Equip,
        Purchase,
        LevelUp,
        Error,
    }
    private void Awake()
    {
        mAudioSource = GetComponent<AudioSource>();
    }
    public void MuteBackGroundMusic(bool _mute)
    {
        mIsBackGroundMusicMute = _mute;
        mBackGroundMusic.mute = mIsBackGroundMusicMute;
        mBackOn.transform.GetChild(0).gameObject.SetActive(!mIsBackGroundMusicMute);
        mBackOff.transform.GetChild(0).gameObject.SetActive(mIsBackGroundMusicMute);
    }

    public void MuteEFM(bool _mute)
    {
        mAudioSource.mute = _mute;
        mEFMOn.transform.GetChild(0).gameObject.SetActive(!_mute);
        mEFMOff.transform.GetChild(0).gameObject.SetActive(_mute);
    }

    public void ClickUISound(AudioType _type)
    {
        if (!mAudioData.ContainsKey(_type))
            return;
        mAudioSource.PlayOneShot(mAudioData[_type]);
    }
}
