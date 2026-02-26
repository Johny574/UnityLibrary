using System;
using System.Linq;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Economy;
using Unity.Services.Friends;

public class Authenticator : Singleton<Authenticator> {

public GoogleAuthenticator GoogleAuthenticator { get; private set; }
  public static Action FinishedLoading;
  public Action RegisterFailed, LoginFailed;
  protected override async void Awake() {
    base.Awake();
    await UnityServices.InitializeAsync();
  }

  async void Start()
  {
    GoogleAuthenticator = new();
    #if !UNITY_EDITOR
    if (AuthenticationService.Instance.SessionTokenExists)
    {
      await Loader.Instance.Play(async () => await AuthenticationService.Instance.SignInAnonymouslyAsync(), "Signing in", false);
    }
    #endif
    
  }
  void OnEnable() {
    WorldManager.WorldsLoaded += OnWorldsLoaded;
  }
  
  void OnDisable() {
    WorldManager.WorldsLoaded -= OnWorldsLoaded;
  }

  async void OnWorldsLoaded() {
    await FriendsService.Instance.InitializeAsync();
    await EconomyService.Instance.Configuration.SyncConfigurationAsync();
    FinishedLoading?.Invoke();
  }

  #if UNITY_EDITOR
  public async void SignInAnonymous() {
    if (AuthenticationService.Instance.IsSignedIn)
      return;

    await Loader.Instance.Play(async () => await AuthenticationService.Instance.SignInAnonymouslyAsync(), "Signing in", false);
  }
  #endif

  public async System.Threading.Tasks.Task SignInWithUsernamePassword(string username, string password) {
    if (AuthenticationService.Instance.IsSignedIn)
      return;

    if (!IsValid(password)) {
      UIEvents.ShowPrompt?.Invoke(new PromptData(LoginFailed, $"Credentials invalid."));
      Loader.Instance.Hide();
      return;
    }
    
    try {
      await Loader.Instance.Play(async () => await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password), "Signing in", false);
    }

    catch (AuthenticationException) {
      UIEvents.ShowPrompt?.Invoke(new PromptData(async () =>await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password), "No account found with these credentials, create a new account ?"));
      Loader.Instance.Hide();
    }

    catch(RequestFailedException e) {
      UIEvents.ShowPrompt?.Invoke(new PromptData(LoginFailed, e.Message));
      Loader.Instance.Hide();
    }
  }

  public async System.Threading.Tasks.Task SignUpWithUsernamePassword(string username, string password) {
    if (AuthenticationService.Instance.IsSignedIn)
      return;

    if (!IsValid(password)) {
      UIEvents.ShowPrompt?.Invoke(new PromptData(RegisterFailed, $"Credentials invalid."));
      Loader.Instance.Hide();
      return;
    }

    try
    {
      await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);    
    }
    catch (RequestFailedException e)
    {
      Loader.Instance.Hide();
      UIEvents.ShowPrompt?.Invoke(new PromptData(RegisterFailed, e.Message));
    }
  }

  public static bool IsValid(string password) {
        if (string.IsNullOrEmpty(password))
            return false;

        bool hasLetter = password.Any(char.IsLetter);
        bool hasUpper = password.Any(char.IsUpper);
        bool hasLower = password.Any(char.IsLower);
        bool hasDigit = password.Any(char.IsDigit);
        bool hasSymbol = password.Any(ch => !char.IsLetterOrDigit(ch));
        bool lengthValid = password.Length >= 8 && password.Length <= 30;

        return hasLetter && hasUpper && hasLower && hasDigit && hasSymbol && lengthValid;
    }
}