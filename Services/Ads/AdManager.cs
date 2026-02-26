using System;
using UnityEngine;

public class AdManager : Singleton<AdManager> {
    float AddTime, AddTimer;
    [SerializeField] float AddTimerMin = 5f, AddTimerMax = 10f;
    bool _shown = false; 

    void Update() {
        if (AddTime < AddTimer && !_shown)
            AddTime += Time.deltaTime;
        else if (!_shown) {
            ShowAdd();
        }
    }

    void ShowAdd() {
        _shown = true;   
        TrayEvents.AdEvents.ShowAdButton?.Invoke();
    }


    public static AdReward GenerateCurrencyAd() {
        var currencies = Enum.GetValues(typeof(PlayerCurrencyAdapter.Currency));
        var id = currencies.GetValue(UnityEngine.Random.Range(0, currencies.Length)).ToString();
        return new AdReward(id, 20, 50);
    }

    public void Refresh() {
        AddTime = 0f;
        AddTimer = UnityEngine.Random.Range(AddTimerMin, AddTimerMax);
        _shown = false;
    }
}

