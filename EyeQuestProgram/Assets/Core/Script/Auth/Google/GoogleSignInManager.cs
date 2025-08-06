using UnityEngine;
using Firebase;
using Firebase.Auth;
using Google;
using System.Threading.Tasks;
using Firebase.Extensions;

public class GoogleSignInManager : MonoBehaviour
{
    private FirebaseAuth auth;
    private GoogleSignInConfiguration configuration;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;

        configuration = new GoogleSignInConfiguration
        {
            WebClientId = "917852209560-au2u6vvsuti1vr1l1f15rv29dbi8i9g3.apps.googleusercontent.com",
            RequestEmail = true,
            RequestIdToken = true
        };
    }

    public GameObject _WaitingPanel;

    public void OnGoogleSignIn()
    {
        GoogleSignIn.Configuration = configuration;
     
        var signIn = GoogleSignIn.DefaultInstance.SignIn();
        signIn.ContinueWithOnMainThread(OnAuthenticationFinished);  // ✅

        _WaitingPanel.SetActive(true);
    }

    public TMPro.TextMeshProUGUI _LoginLog;
    private void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            Debug.LogError("Google Sign-In failed: " + task.Exception);
            _LoginLog.text = "Google Sign-In failed: " + task.Exception;
            return;
        }

        if (task.IsCanceled)
        {
            Debug.LogWarning("Google Sign-In was canceled.");
            _LoginLog.text = "Google Sign-In was canceled.";
            return;
        }

        GoogleSignInUser user = task.Result;
        _LoginLog.text = "Google Sign-In Success! Welcome " + user.DisplayName;

        Credential credential = GoogleAuthProvider.GetCredential(user.IdToken, null);

        auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(authTask =>  // ✅
        {
            if (authTask.IsFaulted || authTask.IsCanceled)
            {
                Debug.LogError("Firebase Sign-In failed: " + authTask.Exception);
                _LoginLog.text = "Firebase Sign-In failed: " + authTask.Exception;
                return;
            }

            FirebaseUser newUser = authTask.Result;
            _LoginLog.text = "Firebase Sign-In Success! Welcome " + newUser.DisplayName + " / " + newUser.Email;

            GetComponent<LoginManager>()._CallFireBaseLogin(newUser.Email, newUser.UserId, newUser.DisplayName, user.IdToken);

            PlayerPrefs.SetInt("isLogin_Type", 3);
            PlayerPrefs.SetString("Username", newUser.Email);
            PlayerPrefs.SetString("Password", newUser.UserId);
            PlayerPrefs.SetString("DisplayName", newUser.DisplayName);
            PlayerPrefs.SetString("accessToken", user.IdToken);
        });
    }
}
