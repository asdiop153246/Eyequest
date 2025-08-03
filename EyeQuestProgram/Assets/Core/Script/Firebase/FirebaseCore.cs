using System.Collections;
using UnityEngine;
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
        StartCoroutine(InitFirebaseSafe());
    }

    private IEnumerator InitFirebaseSafe()
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

    public IEnumerator SendTokenToServer(string token)
    {
        TokenPayload payload = new TokenPayload
        {
            token = token,
            device_type = SystemInfo.operatingSystem,
            device_id = SystemInfo.deviceUniqueIdentifier,
            app_version = Application.version
        };

        string json = JsonUtility.ToJson(payload);
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
        }
    }
}
