using UnityEngine;
using Firebase.Auth;
using Facebook.Unity;
using System.Collections.Generic;
using System.Threading.Tasks;

public class FacebookLoginManager : MonoBehaviour
{
    private FirebaseAuth auth;

    void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(() => {
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

    public void OnFacebookLogin()
    {
        var permissions = new List<string>() { "public_profile", "email" };
        FB.LogInWithReadPermissions(permissions, AuthCallback);
    }

    public TMPro.TextMeshProUGUI _LoginLog;

    private void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            string accessToken = AccessToken.CurrentAccessToken.TokenString;
            Credential credential = FacebookAuthProvider.GetCredential(accessToken);

            auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.LogError("Firebase Facebook Sign-In Failed: " + task.Exception);
                    _LoginLog.text = "Firebase Facebook Sign-In Failed: " + task.Exception;
                    return;
                }

                FirebaseUser newUser = task.Result;
                Debug.Log(newUser.Email);
                Debug.Log(newUser.UserId);
                Debug.Log(newUser.TokenAsync(true).ToString());
                _LoginLog.text = "Firebase Facebook Sign-In Success! Welcome " + newUser.DisplayName + " / " + newUser.Email + " / " + newUser.UserId + " / "+ newUser.TokenAsync(true).ToString();
                StartCoroutine(GetComponent<LoginManager>()._FirebaseAuth(newUser.Email, newUser.UserId, newUser.DisplayName,"123"));
                Debug.Log("Firebase Facebook Sign-In Success! Welcome " + newUser.DisplayName);
            });
        }
        else
        {
            _LoginLog.text = "Facebook Login Canceled or Failed.";
            Debug.LogWarning("Facebook Login Canceled or Failed.");
        }
    }
}
