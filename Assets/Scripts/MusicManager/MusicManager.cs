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
    private void Awake()
    {
        mBackgroundMusic = GameObject.Find("BackgroundMusic");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMusic()
    {
        mBackgroundMusic.GetComponent<AudioSource>().clip
                    = Resources.Load("Sound\\BGM\\" + MapManager.Instance.CurrentMapType.ToString()) as AudioClip;
        if(DEBUG)   
            Debug.Log("음악 on: " +mBackgroundMusic.GetComponent<AudioSource>().clip.name);
        mBackgroundMusic.GetComponent<AudioSource>().Play();
        mBackgroundMusic.GetComponent<AudioSource>().loop = true;
    }
}
