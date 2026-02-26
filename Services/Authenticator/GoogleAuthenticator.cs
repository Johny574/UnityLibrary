public class GoogleAuthenticator {
    //  public void SignInWithGooglePlay()
    // {
    //     PlayGamesPlatform.Activate();
    //     LoginGooglePlayGames();
    // }

    // public void LoginGooglePlayGames()
    // {
    //     PlayGamesPlatform.Instance.Authenticate(status =>
    //     {
    //         if (status == SignInStatus.Success)
    //         {
    //             Debug.Log("Google Play Games sign-in successful!");
    //             RequestServerAccessAndSignIn();
    //         }
    //         else
    //         {
    //             Debug.LogWarning("Automatic sign-in failed: " + status);
    //             SignInManually();
    //         }
    //     });
    // }

    // private void SignInManually()
    // {
    //     PlayGamesPlatform.Instance.ManuallyAuthenticate(status =>
    //     {
    //         if (status == SignInStatus.Success)
    //         {
    //             Debug.Log("Manual sign-in successful!");
    //             RequestServerAccessAndSignIn();
    //         }
    //         else
    //         {
    //             Debug.LogError("Manual sign-in failed: " + status);
    //         }
    //     });
    // }

    // private void RequestServerAccessAndSignIn()
    // {
    //     PlayGamesPlatform.Instance.RequestServerSideAccess(true, async authCode =>
    //     {
    //         try
    //         {
    //             await AuthenticationService.Instance.SignInWithGooglePlayGamesAsync(authCode);
    //             Debug.Log("Unity Authentication sign-in successful! PlayerID: " + AuthenticationService.Instance.PlayerId);
    //         }
    //         catch (Exception e)
    //         {
    //             Debug.LogError("Unity Authentication sign-in failed: " + e);
    //         }
    //     });
    // }
}