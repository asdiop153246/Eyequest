using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Update_Currency : MonoBehaviour
{

    public TMPro.TextMeshProUGUI _GoldTxt;
    public TMPro.TextMeshProUGUI _GemTxt;
    public TMPro.TextMeshProUGUI _VisionTxt;
    public void OnEnable()
    {
        _UpdateTxt();

        Userdata.Instance.gameObject.GetComponent<ApiCaller>().OnCall_UpdateCurrency_OK += () =>
        {
            _UpdateTxt();
        };
    }

    public void OnDisable()
    {
        Userdata.Instance.gameObject.GetComponent<ApiCaller>().OnCall_UpdateCurrency_OK -= Userdata.Instance.gameObject.GetComponent<ApiCaller>().OnCall_UpdateCurrency_OK;
    }

    public void _UpdateTxt()
    {
        _GoldTxt.text = Userdata.Instance._User.data.currency.gold + "";
        _GemTxt.text = Userdata.Instance._User.data.currency.gem + "";
        _VisionTxt.text = Userdata.Instance._User.data.currency.vision_point + "";
    }
}
