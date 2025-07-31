using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Messaging;
using UnityEngine.Networking;

public class FirebaseCore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                Firebase.FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });

        Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
        Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
    }

    public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        UnityEngine.Debug.Log("Received Registration Token: " + token.Token);
        StartCoroutine(_SentAPI(token.Token));
    }

    public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        UnityEngine.Debug.Log("Received a new message from: " + e.Message.From);
    }

    public class _Token
    {
        public string token;
        public string device_type;
        public string device_id;
        public string app_version;
    }


    public IEnumerator _SentAPI(string _Token)
    {
        _Token _temp = new _Token();
        _temp.token = _Token;
        _temp.device_type = SystemInfo.operatingSystem.ToString();
        _temp.device_id = SystemInfo.deviceUniqueIdentifier.ToString();

        string json = JsonUtility.ToJson(_temp);
        Debug.Log(json);
        var request = new UnityWebRequest(Userdata.Instance.gameObject.GetComponent<ApiCaller>()._Url + "/api/notify/register-token", "POST");
        request.SetRequestHeader("Authorization", "Bearer " + Userdata.Instance._User.data.access_token);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Accept", "application/json");
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();
        Debug.Log("request responseText:" + request.downloadHandler.text);

        //_WaitingPanel.SetActive(false);

        if (request.result != UnityWebRequest.Result.Success)
        {

            //OnCall_GetInventory_Failed?.Invoke();
        }
        else
        {
            //Userdata.Instance._User = JsonUtility.FromJson<Userdata.LoginResponse>(request.downloadHandler.text);
            //StartCoroutine(GetRequest(_Type));
        }
    }
}
