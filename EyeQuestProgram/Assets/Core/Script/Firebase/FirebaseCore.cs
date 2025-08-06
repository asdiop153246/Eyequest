using System.Collections;
using UnityEngine;
using UnityEngine.Android;
using Firebase;
using Firebase.Extensions;
using Firebase.Messaging;
using UnityEngine.Networking;

public class FirebaseCore : MonoBehaviour
{
    private bool firebaseReady = false;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        
    }

    public IEnumerator InitFirebaseSafe()
    {
        var checkTask = FirebaseApp.CheckAndFixDependenciesAsync();
        yield return new WaitUntil(() => checkTask.IsCompleted);

        if (checkTask.Result == DependencyStatus.Available)
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;

            // 🔁 หน่วงเวลาเล็กน้อยก่อนเรียก Messaging
            yield return new WaitForSeconds(0.1f);

            yield return StartCoroutine(InitMessagingSafely());
        }
        else
        {
            Debug.LogError($"🚫 Firebase dependencies error: {checkTask.Result}");
        }
    }

    private IEnumerator InitMessagingSafely()
    {
        yield return null; // หน่วงอีก 1 frame เพื่อให้แน่ใจว่า Messaging ready

        try
        {
            FirebaseMessaging.TokenReceived += OnTokenReceived;
            FirebaseMessaging.MessageReceived += OnMessageReceived;
            firebaseReady = true;
            Debug.Log("✅ Firebase Messaging Registered");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("🔥 Messaging Init Failed: " + ex.ToString());
        }
    }

    public void OnTokenReceived(object sender, TokenReceivedEventArgs token)
    {
        Debug.Log("Received Registration Token: " + token.Token);
        StartCoroutine(SendTokenToServer(token.Token));
    }

    public void OnMessageReceived(object sender, MessageReceivedEventArgs e)
    {
        Debug.Log("📩 Message received from: " + e.Message.From);
    }

    [System.Serializable]
    public class TokenPayload
    {
        public string token;
        public string device_type;
        public string device_id;
        public string app_version;
    }

    public void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(SendTokenToServer("do-F55ERTfG0JXU5zSUcdk:APA91bEvJxx18XbcvuSocI7eacdwvoANxWsE2tYDOuZGoDzdoyZi3tCUtVlK3P0lNuIiDvb6-gh-jbPhlejjRHnZjJYXv1F-z1lJOpmSuZX2wiagphOOmbM"));
        }
    }

    public IEnumerator SendTokenToServer(string token)
    {
        TokenPayload payload = new TokenPayload();
        payload.token = token;
        payload.device_type = "";
        payload.device_id = "";
        payload.app_version = "";

        string json = JsonUtility.ToJson(payload);

        Debug.Log(json);
        string url = Userdata.Instance.GetComponent<ApiCaller>()._Url + "/api/notify/register-token";

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Authorization", "Bearer " + Userdata.Instance._User.data.access_token);
        request.SetRequestHeader("Accept", "application/json");
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"❌ Token send failed: {request.error}\n{request.downloadHandler.text}");
        }
        else
        {
            Debug.Log($"✅ Token sent successfully: {request.downloadHandler.text}");

#if UNITY_ANDROID && !UNITY_EDITOR
        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            var sdkInt = new AndroidJavaClass("android.os.Build$VERSION").GetStatic<int>("SDK_INT");

            if (sdkInt >= 33)
            {
                var contextCompat = new AndroidJavaClass("androidx.core.content.ContextCompat");
                int result = contextCompat.CallStatic<int>("checkSelfPermission", activity, "android.permission.POST_NOTIFICATIONS");

                if (result != 0) // PERMISSION_GRANTED = 0
                {
                    var activityCompat = new AndroidJavaClass("androidx.core.app.ActivityCompat");
                    string[] permissions = new string[] { "android.permission.POST_NOTIFICATIONS" };
                    activityCompat.CallStatic("requestPermissions", activity, permissions, 1001);
                }
            }
        }
#endif

            if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
            {
                Permission.RequestUserPermission(Permission.Camera);
            }
        }
    }
}
