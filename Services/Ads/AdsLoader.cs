using System;
using Unity.Services.LevelPlay;
using UnityEngine;

public class AdsLoader : Singleton<AdsLoader> 
{
    string AppID = "23745e8d5";
    string rewardedAdID = "kk4zqdqu8zwccwtv";
    public Action<AdReward> CurrencyAdFinished;
    public Action<AdReward>  ItemAdFinished;

    void Start() {
        LevelPlay.SetMetaData("is_test_suite", "enable");
        LevelPlay.Init(AppID);
        // LevelPlay.Init("editor");
        LevelPlay.LaunchTestSuite();
    }
    void OnEnable() {
        // CurrencyAdFinished += (reward) => GameEvents.CurrencyEvents.AddManual(new CurrencyData[1]{ new CurrencyData(reward._amount, reward.id) });
    }
    void OnDisable() {
        // CurrencyAdFinished -= (reward) => GameEvents.CurrencyEvents.AddManual(new CurrencyData[1]{ new CurrencyData(reward._amount, reward.id) });
    }

    public void ShowAdd(Action reward) {
        reward.Invoke();
        LevelPlayRewardedAd rewardedAd = new LevelPlayRewardedAd(rewardedAdID);
        rewardedAd.LoadAd(); 
        
        rewardedAd.OnAdLoaded += (info) => rewardedAd.ShowAd();
        rewardedAd.OnAdLoadFailed += (info) => Debug.LogError("failed to load add");
        rewardedAd.OnAdRewarded += (info, levelreward) => reward.Invoke();
    }
}