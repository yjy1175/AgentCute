
# GoogleAds

## GoogleMobileAds plugin을 이용한 광고 제공
* 광고가 필요한 곳에 AdmobMananger를 통한 광고 재생


## TroubleShooting
* UnityEditor환경과 Mobile환경의 GoogleAdmob의 작동방식으로 인한 오류
* Mobile환경에서 광고 재생 시, Unity상의 API를 호출하면 앱 크래시 발생
* GooleAdmob이 재생될 때, 게임의 메인 쓰레드를 중지 후 실행한 다는 점 캐치
* flag형태의 변수를 두어, 보상을 주는 API를 게임의 쓰레드가 정상적으로 돌아왔을 때, 실행해주는 방법으로 해결
```sh
    IEnumerator WaitForReward()
    {
        yield return new WaitForEndOfFrame();
        if (curVideoCompleteReward)
        {
            ChangeEndReward(curVideoCompleteReward);
            curVideoCompleteReward = false;
        }
    }
```

## 참고소스
* [AdmobMananger.cs](../../Assets/Scripts/Util/GoogleAds/AdmobMananger.cs)