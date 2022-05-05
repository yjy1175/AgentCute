using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : SingleToneMaker<MusicManager>
{
    private bool DEBUG = false;
    [SerializeField]
    private GameObject mBackgroundMusic;
    /*
     * 초원맵 - 전투 BGM(오락실버전2) 
     * 설원맵 - 전투 BGM(설원맵전용) 
     * 화산맵 - 전투 BGM(오락실버전1) 
     * 던전맵 - 타이틀 BGM(오락실버전2)
     */

    private AudioSource mAudioSource;

    //projectile명을 통해 SoundData검색
    private Dictionary<string, SoundData> mAudioClipData;

    public struct SoundData
    {
        public AudioClip soundName;
        public float volume;
    }
    
    private void Awake()
    {
        mBackgroundMusic = GameObject.Find("BackgroundMusic");
    }
    
    void Start()
    {
        mAudioClipData = new Dictionary<string, SoundData>();
        mAudioSource = GetComponent<AudioSource>();
        //sound 최적화
        mAudioSource.dopplerLevel = 0f;
        mAudioSource.reverbZoneMix = 0f;
        LoadSoundEffect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //mAudioClipData에 _name으로 되어있는 soundEffect 재생
    public void OneShotProjectileSound(string _name)
    {
        if (!mAudioClipData.ContainsKey(_name))
        {
            if(DEBUG)
                Debug.Log(_name + "으로된 clip은 설정되어있지 않습니다");
            return;
        }
        if(DEBUG)
            Debug.Log("projectile명 :" + _name + ",재생할 클립명 : " + mAudioClipData[_name].soundName);
        mAudioSource.volume = mAudioClipData[_name].volume;
        mAudioSource.PlayOneShot(mAudioClipData[_name].soundName);

    }

    //맵에 설정된 background음악을 play
    public void OnBackgroundMusic()
    {
        mBackgroundMusic.GetComponent<AudioSource>().clip
                    = Resources.Load("Sound\\BGM\\" + MapManager.Instance.CurrentMapType.ToString()) as AudioClip;
        if(DEBUG)   
            Debug.Log("음악 on: " +mBackgroundMusic.GetComponent<AudioSource>().clip.name);
        mBackgroundMusic.GetComponent<AudioSource>().Play();
        mBackgroundMusic.GetComponent<AudioSource>().volume = 0.3f;
        mBackgroundMusic.GetComponent<AudioSource>().loop = true;
    }

    //sound effect load
    private void LoadSoundEffect()
    {
        List<Dictionary<string, object>> soundData = CSVReader.Read("CSVFile\\SoundEffect");

        //AudioClip 받아와 딕셔너리화
        AudioClip[] soundEffectsList = Resources.LoadAll<AudioClip>("Sound\\Effect");
        Debug.Log("음악 개수 :"+ soundEffectsList.Length);

        Dictionary<string, AudioClip> soundDict = new Dictionary<string, AudioClip>();
        foreach (AudioClip clip in soundEffectsList)
        {
            soundDict[clip.name]=  clip;
        }


        for (int idx = 0; idx < soundData.Count; idx++)
        {
            SoundData data = new SoundData();
            if (soundData[idx]["SoundName"].ToString().Equals("None"))
                continue;
            Debug.Log(soundData[idx]["SoundName"].ToString());
            data.soundName = soundDict[soundData[idx]["SoundName"].ToString()];
            data.volume = float.Parse(soundData[idx]["Volume"].ToString());
            mAudioClipData[soundData[idx]["ProjectileName"].ToString()] = data;
        }
    }
}
