



using System.Collections.Generic;
using Newtonsoft.Json;
using Unity.Services.Authentication;
using Unity.Services.RemoteConfig;

public class RemoteConfig : Singleton<RemoteConfig> {
      struct userAttributes { }
    struct appAttributes { }
    public Dictionary<string, Event> CurrentEvents;
    public Dictionary<string, LoginReward> Calender;

    void Start() => AuthenticationService.Instance.SignedIn += OnSignedIn;
    
    void OnDisable()
    {
        AuthenticationService.Instance.SignedIn -= OnSignedIn;
        RemoteConfigService.Instance.FetchCompleted -= LoadConfig;
    }

    async void OnSignedIn() {
        RemoteConfigService.Instance.FetchCompleted += LoadConfig;
        RemoteConfigService.Instance.FetchConfigs(new userAttributes(), new appAttributes());
    }

    void LoadConfig(ConfigResponse response) {
        CurrentEvents = LoadFromConfig<Dictionary<string, Event>>("Events");
        Calender = LoadFromConfig<Dictionary<string, LoginReward>>("Calender");
    }

    T LoadFromConfig<T>(string id) {
        string configJson = RemoteConfigService.Instance.appConfig.GetJson(id);
        return JsonConvert.DeserializeObject<T>(configJson);
    }
}