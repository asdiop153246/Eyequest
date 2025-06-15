using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Text.RegularExpressions;
public class LoginManager : MonoBehaviour
{
    public static LoginManager Instance;

    public string _URL;
    #region Login

    public GameObject _LoginPanel;

    public TMPro.TMP_InputField _EmailPanel;
    public TMPro.TMP_InputField _ForgotPassword_Email_Panel;

    public string _Temp_UserEmail;

    public delegate void GeneralDelegate();

    public GameObject _LoginOK;
    public GameObject _LoginFailed;
    public GameObject _WaitingPanel;

    public void OnEnable()
    {
        Userdata.Instance.gameObject.GetComponent<ApiCaller>().OnCall_GetWorldData_OK += () =>
        {
            _WaitingPanel.SetActive(false);
            _LoginOK.SetActive(true);
            Application.LoadLevel(1);
        };
    }

    public void OnDisable()
    {
        Userdata.Instance.gameObject.GetComponent<ApiCaller>().OnCall_GetWorldData_OK -= Userdata.Instance.gameObject.GetComponent<ApiCaller>().OnCall_GetWorldData_OK;
    }

    public void _GetEmail()
    {
        _Temp_UserEmail = _EmailPanel.text;
    }
    public class _CheckEmailClass
    {
        public string _Email;
    }

    public GameObject _EnterPasswordPanel;
    public TMPro.TextMeshProUGUI _HeaderEmail;

    public void _CheckEmail_Btm()
    {
        StartCoroutine(_Check_Email(_Temp_UserEmail));
    }

    public IEnumerator _Check_Email(string _UserEmail)
    {
        if (Userdata.Instance._isDemo)
        {
            _WaitingPanel.SetActive(true);
            yield return new WaitForSeconds(2f);
            _WaitingPanel.SetActive(false);

            if (_Temp_UserEmail != "")
            {
                _LoginOK.SetActive(true);
                yield return new WaitForSeconds(2f);
                _LoginPanel.SetActive(false);
                _LoginOK.SetActive(false);
                _EnterPasswordPanel.SetActive(true);
                Userdata.Instance._User.data.user.email = _UserEmail;
                _HeaderEmail.text = Userdata.Instance._User.data.user.email;
            }
            else
            {
                _LoginFailed.SetActive(true);
                yield return new WaitForSeconds(2f);
                _LoginFailed.SetActive(false);
            }
            
        }
        else
        {
            _WaitingPanel.SetActive(true);

            _CheckEmailClass data = new _CheckEmailClass();
            data._Email = _UserEmail;

            string json = JsonUtility.ToJson(data);

            var request = new UnityWebRequest(_URL + "/api/leaderboard/getEventLeaderboard", "POST");
            //request.SetRequestHeader("Authorization", Userdata.instance._Userdata.data.account.access_token);

            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Accept", "application/json");

            yield return request.SendWebRequest();
            Debug.Log("request responseText:" + request.downloadHandler.text);

            _WaitingPanel.SetActive(false);

            if (request.result != UnityWebRequest.Result.Success)
            {
                _LoginFailed.SetActive(true);
                yield return new WaitForSeconds(2f);
                _LoginFailed.SetActive(false);
                //ShowErrorWithCode(request.responseCode.ToString());
                //OnCallBack_Login_CheckEmail_Failed?.Invoke();
            }
            else
            {
                _LoginOK.SetActive(true);
                Userdata.Instance._User.data.user.email = _UserEmail;
                yield return new WaitForSeconds(2f);
                _LoginPanel.SetActive(false);
                _EnterPasswordPanel.SetActive(true);
            }
        }
       
    }

    public string _Temp_Password;
    public TMPro.TMP_InputField _Password_Field;

    public class _CheckPassword_Class
    {
        public string email;
        public string password;
    }
    public void _GetPassword()
    {
        _Temp_Password = _Password_Field.text;
    }

    public void _CheckPassword_Btm()
    {
        StartCoroutine(_CheckPassword(_Temp_UserEmail, _Temp_Password));
    }

    public IEnumerator _CheckPassword(string _UserEmail, string _UserPassword)
    {
        _WaitingPanel.SetActive(true);

        _CheckPassword_Class data = new _CheckPassword_Class();
        data.email = _UserEmail;
        data.password = _UserPassword;

        string json = JsonUtility.ToJson(data);
        Debug.Log(json);
        var request = new UnityWebRequest(_URL + "/api/login", "POST");
        //request.SetRequestHeader("Authorization", Userdata.instance._Userdata.data.account.access_token);

        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Accept", "application/json");
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();
        Debug.Log("request responseText:" + request.downloadHandler.text);

        _WaitingPanel.SetActive(false);

        if (request.result != UnityWebRequest.Result.Success)
        {

            _LoginFailed.SetActive(true);
            yield return new WaitForSeconds(2f);
            _LoginFailed.SetActive(false);
            //ShowErrorWithCode(request.responseCode.ToString());
            //OnCallBack_Login_CheckEmail_Failed?.Invoke();
        }
        else
        {
            Userdata.Instance._User = JsonUtility.FromJson<Userdata.LoginResponse>(request.downloadHandler.text);
            if (Userdata.Instance._User.data.access_token == null)
            {
                _LoginFailed.SetActive(true);
                yield return new WaitForSeconds(2f);
                _LoginFailed.SetActive(false);
            }
            else
            {

                _WaitingPanel.SetActive(true);
                StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._GetWorldData());
                
            }
        }
    }

    public void _NextSence()
    {

    }

    public void _FastLogin()
    {
        Userdata.Instance._User.data.user.email = "panatthakorn.isd@gmail.com";
        StartCoroutine(_CheckPassword(Userdata.Instance._User.data.user.email, "12345678mM!"));
    }




    #endregion

    #region Register

    public GameObject _RegisterPanel;

    public TMPro.TMP_InputField _Register_Username;
    public TMPro.TMP_InputField _Register_Email_Input;
    public TMPro.TMP_InputField _Register_Create_Password;
    public TMPro.TMP_InputField _Register_Confirm_Password;

    public Slider _Age;
    public TMPro.TextMeshProUGUI _AgeValue;
    public TMPro.TMP_Dropdown _Gender;
    public TMPro.TMP_Dropdown _EyeProblems;

    [System.Serializable]
    public class Register
    {
        public string name;
        public string email;
        public string password;
        public string password_confirmation;
        public string type;
    }

    public Register _RegisterData;

    public void _Inpute_UserName()
    {
        _RegisterData.name = _Register_Username.text;
    }

    public void _Input_Email()
    {
        _RegisterData.email = _Register_Email_Input.text;
    }

    public void _Input_Password()
    {
        _RegisterData.password = _Register_Create_Password.text;
    }

    public void _Input_password_confirmation()
    {
        _RegisterData.password_confirmation = _Register_Confirm_Password.text;
    }

    public void _Input_Age()
    {
        _AgeValue.text = _Age.value.ToString();
    }


    public void _Register_Btm()
    {
        StartCoroutine(_Register_Password());
    }

    public TMPro.TextMeshProUGUI _RedTxt;
    public IEnumerator _Register_Password()
    {
        _RedTxt.gameObject.SetActive(false);
        _WaitingPanel.SetActive(true);
        _RegisterData.type = "personal";
        if (!IsValidPassword(_RegisterData.password, out string errorMessage))
        {
            _WaitingPanel.SetActive(false);
            _RedTxt.gameObject.SetActive(true);
            _RedTxt.text = errorMessage;
        }
        else
        {
            string json = JsonUtility.ToJson(_RegisterData);
            Debug.Log(json);
            var request = new UnityWebRequest(_URL + "/api/register", "POST");
            //request.SetRequestHeader("Authorization", Userdata.instance._Userdata.data.account.access_token);

            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Accept", "application/json");
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();
            Debug.Log("request responseText:" + request.downloadHandler.text);

            _WaitingPanel.SetActive(false);

            if (request.result != UnityWebRequest.Result.Success)
            {
                _LoginFailed.SetActive(true);
                yield return new WaitForSeconds(2f);
                _LoginFailed.SetActive(false);
                //ShowErrorWithCode(request.responseCode.ToString());
                //OnCallBack_Login_CheckEmail_Failed?.Invoke();
            }
            else
            {
                Userdata.Instance._User = JsonUtility.FromJson<Userdata.LoginResponse>(request.downloadHandler.text);
                _LoginOK.SetActive(true);
                yield return new WaitForSeconds(2f);
                _LoginOK.SetActive(false);
                _RegisterPanel.SetActive(false);
                _EnterPasswordPanel.SetActive(true);
                _HeaderEmail.text = Userdata.Instance._User.data.user.email;
                _Temp_UserEmail = Userdata.Instance._User.data.user.email;
                //_LoginPanel.SetActive(false);
                //_EnterPasswordPanel.SetActive(true);
            }
        }
        

        
    }

    public static bool IsValidPassword(string password, out string errorMessage)
    {
        List<string> errors = new List<string>();

        if (password.Length < 8)
            errors.Add("• ต้องมีความยาวอย่างน้อย 8 ตัวอักษร");

        if (!Regex.IsMatch(password, "[A-Z]"))
            errors.Add("• ต้องมีตัวอักษรพิมพ์ใหญ่ (A–Z)");

        if (!Regex.IsMatch(password, "[a-z]"))
            errors.Add("• ต้องมีตัวอักษรพิมพ์เล็ก (a–z)");

        if (!Regex.IsMatch(password, "[0-9]"))
            errors.Add("• ต้องมีตัวเลข (0–9)");

        if (!Regex.IsMatch(password, "[^a-zA-Z0-9]"))
            errors.Add("• ต้องมีอักขระพิเศษ (เช่น !@#$%^&)");

        if (errors.Count == 0)
        {
            errorMessage = "";
            return true;
        }

        errorMessage = string.Join(" ", errors);
        return false;
    }

    void Start()
    {
        string password = "abc123";
        if (IsValidPassword(password, out string errorMessage))
        {
            Debug.Log("รหัสผ่านถูกต้อง");
        }
        else
        {
            Debug.Log(errorMessage);
        }
    }

    #endregion

    #region Forget Password

    public class _ForgotPassword
    {
        public string email;
    }

    public void _ForgetPassword_BTM()
    {
        StartCoroutine(_ForgetPassword());
    }

    public void _GetEmail_ForgetPassword()
    {
        _Temp_UserEmail = _ForgotPassword_Email_Panel.text;
    }
    public IEnumerator _ForgetPassword()
    {
        _WaitingPanel.SetActive(true);

        _ForgotPassword data = new _ForgotPassword();
        data.email = _Temp_UserEmail;

        string json = JsonUtility.ToJson(data);
        Debug.Log(json);
        var request = new UnityWebRequest(_URL + "/api/forgot-password", "POST");
        //request.SetRequestHeader("Authorization", Userdata.instance._Userdata.data.account.access_token);

        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Accept", "application/json");
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();
        Debug.Log("request responseText:" + request.downloadHandler.text);

        _WaitingPanel.SetActive(false);

        if (request.result != UnityWebRequest.Result.Success)
        {
            _LoginFailed.SetActive(true);
            yield return new WaitForSeconds(2f);
            _LoginFailed.SetActive(false);
        }
        else
        {
            _LoginOK.SetActive(true);
            yield return new WaitForSeconds(2f);
            _LoginOK.SetActive(false);
        }
    }
    #endregion


    public void ShowErrorWithCode(string _Code)
    {

        Debug.Log(_Code);
        if (_Code == "401" || _Code == "500")
        {
            //_LogoutPanel.SetActive(true);
        }
    }
}
