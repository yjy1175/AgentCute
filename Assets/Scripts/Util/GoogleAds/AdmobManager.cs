using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdmobManager : SingleToneMaker<AdmobManager>
{
    static bool isAdVideoLoaded = false;

    private RewardedAd videoAd;
    public static bool ShowAd = false;
    public enum AdType
    {
        Supply,
        Resurrection
    }
    public AdType currentType;
    string videoID;
    public void Start()
    {
        //Test ID : "ca-app-pub-3940256099942544/5224354917"
        //광고 ID
        videoID = "ca-app-pub-3940256099942544/5224354917";
        videoAd = new RewardedAd(videoID);
        Handle(videoAd);
        Load();
    }

    private void Handle(RewardedAd videoAd)
    {
        videoAd.OnAdLoaded += HandleOnAdLoaded;
        videoAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        videoAd.OnAdFailedToShow += HandleOnAdFailedToShow;
        videoAd.OnAdOpening += HandleOnAdOpening;
        videoAd.OnAdClosed += HandleOnAdClosed;
        videoAd.OnUserEarnedReward += HandleOnUserEarnedReward;
    }
    private void OnDestroy()
    {
        videoAd.OnAdLoaded -= HandleOnAdLoaded;
        videoAd.OnAdFailedToLoad -= HandleOnAdFailedToLoad;
        videoAd.OnAdFailedToShow -= HandleOnAdFailedToShow;
        videoAd.OnAdOpening -= HandleOnAdOpening;
        videoAd.OnAdClosed -= HandleOnAdClosed;
        videoAd.OnUserEarnedReward -= HandleOnUserEarnedReward;
    }
    private void Load()
    {
        AdRequest request = new AdRequest.Builder().Build();
        videoAd.LoadAd(request);
    }

    public RewardedAd ReloadAd()
    {
        RewardedAd videoAd = new RewardedAd(videoID);
        Handle(videoAd);
        AdRequest request = new AdRequest.Builder().Build();
        videoAd.LoadAd(request);
        return videoAd;
    }

    //오브젝트 참조해서 불러줄 함수
    public void Show(AdType _type)
    {
        currentType = _type;
        StartCoroutine("ShowRewardAd");
    }

    private IEnumerator ShowRewardAd()
    {
        while (!videoAd.IsLoaded())
        {
            yield return null;
        }
        videoAd.Show();
    }

    //광고가 로드되었을 때
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {

    }
    //광고 로드에 실패했을 때
    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {

    }
    //광고 보여주기를 실패했을 때
    public void HandleOnAdFailedToShow(object sender, AdErrorEventArgs args)
    {

    }
    //광고가 제대로 실행되었을 때
    public void HandleOnAdOpening(object sender, EventArgs args)
    {

    }
    //광고가 종료되었을 때
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        //새로운 광고 Load
        this.videoAd = ReloadAd();
    }
    //광고를 끝까지 시청하였을 때
    public void HandleOnUserEarnedReward(object sender, EventArgs args)
    {
        //보상이 들어갈 곳입니다.
        switch (currentType)
        {
            case AdType.Supply:
                DeongunStartManager.Instance.DrawBuff();
                LobbyUIManager.Instance.OpenDoengunPannel();
                break;
            case AdType.Resurrection:
                break;
        }
    }
}
