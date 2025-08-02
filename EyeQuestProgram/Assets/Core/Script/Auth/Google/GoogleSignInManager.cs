using UnityEngine;
using Firebase;
using Firebase.Auth;
using Google;
using System.Threading.Tasks;

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

    public void OnGoogleSignIn()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        GoogleSignIn.Configuration.RequestEmail = true;

        var signIn = GoogleSignIn.DefaultInstance.SignIn();
        signIn.ContinueWith(OnAuthenticationFinished);
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

        // ได้ token จาก Google
        GoogleSignInUser user = task.Result;
        Debug.Log("Google Sign-In Success! " + user.Email);

        Credential credential = GoogleAuthProvider.GetCredential(user.IdToken, null);
        auth.SignInWithCredentialAsync(credential).ContinueWith(authTask =>
        {
            if (authTask.IsFaulted || authTask.IsCanceled)
            {
                Debug.LogError("Firebase Sign-In failed: " + authTask.Exception);
                _LoginLog.text = "Firebase Sign-In failed: " + authTask.Exception;
                return;
            }

            FirebaseUser newUser = authTask.Result;
            Debug.Log("Firebase Sign-In Success! Welcome " + newUser.DisplayName);
            Debug.Log(newUser.Email);
            Debug.Log(newUser.UserId);
            Debug.Log(newUser.TokenAsync(true));
            StartCoroutine(GetComponent<LoginManager>()._FirebaseAuth(newUser.Email, newUser.UserId, newUser.DisplayName, "123"));
            _LoginLog.text = "Firebase Sign-In Success! Welcome " + newUser.DisplayName + " / " + newUser.Email + " / " + newUser.UserId + " / " + newUser.TokenAsync(true).ToString();

        });
    }
}
