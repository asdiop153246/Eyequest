using UnityEngine;
using Firebase.Auth;
using Facebook.Unity;
using System.Collections.Generic;
using Firebase.Extensions; // ✅ สำคัญ
using TMPro;

public class FacebookLoginManager : MonoBehaviour
{
    private FirebaseAuth auth;

    public TextMeshProUGUI _LoginLog;

    void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(() =>
            {
                if (FB.IsInitialized)
                    FB.ActivateApp();
            });
        }
        else
        {
            FB.ActivateApp();
        }

        auth = FirebaseAuth.DefaultInstance;
    }

    public GameObject _WaitingPanel;

    public void OnFacebookLogin()
    {
        var permissions = new List<string>() { "public_profile", "email" };
        FB.LogInWithReadPermissions(permissions, AuthCallback);

        _WaitingPanel.SetActive(true);
    }

    private void AuthCallback(ILoginResult result)
    {
        if (!FB.IsLoggedIn)
        {
            Debug.LogWarning("Facebook Login Canceled or Failed.");
            _LoginLog.text = "Facebook Login Canceled or Failed.";
            return;
        }

        string accessToken = AccessToken.CurrentAccessToken.TokenString;
        Credential credential = FacebookAuthProvider.GetCredential(accessToken);

        _LoginLog.text = "Signing in with Firebase...";

        auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Firebase Facebook Sign-In Failed: " + task.Exception);
                _LoginLog.text = "Firebase Facebook Sign-In Failed: " + task.Exception.Message;
                return;
            }

            FirebaseUser newUser = task.Result;

            _LoginLog.text = "Firebase Facebook Sign-In Success! Welcome " +
                             (string.IsNullOrEmpty(newUser.DisplayName) ? newUser.Email : newUser.DisplayName);

            GetComponent<LoginManager>()._CallFireBaseLogin(
                newUser.Email,
                newUser.UserId,
                newUser.DisplayName,
                accessToken
            );
        });
    }
}