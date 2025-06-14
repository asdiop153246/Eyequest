using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;

public class WellExpRewardSystem : MonoBehaviour
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public int _CurrentCadId=0;

    public void OnEnable()
    {

        _Loaddata(_CurrentCadId);

        Userdata.Instance.gameObject.GetComponent<ApiCaller>().OnCall_GetRewardList_OK += () =>
        {
           _UpdateTxt();
        };

        Userdata.Instance.gameObject.GetComponent<ApiCaller>().OnCall_GetRewardHistory_OK += () =>
        {
            _UpdateHistory();
        };

        Userdata.Instance.gameObject.GetComponent<ApiCaller>().OnCall_GetBand_OK += () =>
        {
            _UpdateCategory();
        };

        Userdata.Instance.gameObject.GetComponent<ApiCaller>().OnCall_RedeemReward_OK += () =>
        {
            StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._UpdateCurrencyValue());
            _RedeemThisRewardOK();
        };


    }

    public void OnDisable()
    {
        Userdata.Instance.gameObject.GetComponent<ApiCaller>().OnCall_GetRewardList_OK -= Userdata.Instance.gameObject.GetComponent<ApiCaller>().OnCall_GetRewardList_OK;
        Userdata.Instance.gameObject.GetComponent<ApiCaller>().OnCall_GetRewardList_Category_OK -= Userdata.Instance.gameObject.GetComponent<ApiCaller>().OnCall_GetRewardList_Category_OK;
        Userdata.Instance.gameObject.GetComponent<ApiCaller>().OnCall_GetBand_OK -= Userdata.Instance.gameObject.GetComponent<ApiCaller>().OnCall_GetBand_OK;
        Userdata.Instance.gameObject.GetComponent<ApiCaller>().OnCall_GetRewardHistory_OK -= Userdata.Instance.gameObject.GetComponent<ApiCaller>().OnCall_GetRewardHistory_OK;
        Userdata.Instance.gameObject.GetComponent<ApiCaller>().OnCall_RedeemReward_OK -= Userdata.Instance.gameObject.GetComponent<ApiCaller>().OnCall_RedeemReward_OK;
    }

    public void _Loaddata(int _CadId)
    {
        _CurrentCadId = _CadId;
        StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._GetRewardList_Band(_CadId));
    }

    public Transform _ChildBox;
    public GameObject _PrefabIcon;
    public void _UpdateCategory()
    {
        foreach (Transform child in _ChildBox.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Userdata.CustomerData x in Userdata.Instance._RewardGetBrand.data)
        {
            GameObject _RewardPack = Instantiate(_PrefabIcon, _ChildBox);
            _RewardPack.GetComponent<RewardBtm>()._CoreReward = this.gameObject;
            _RewardPack.GetComponent<RewardBtm>().imageUrl = x.default_image;
            _RewardPack.GetComponent<RewardBtm>()._BandName = x.name;
            _RewardPack.GetComponent<RewardBtm>()._id = x.id;
            _RewardPack.GetComponent<RewardBtm>()._Type = RewardBtm._RewardBtm.RewardBrand;
        }
    }

    public Transform _ChildBox_InBrand;
    public GameObject _ProductIcon;
    int _RewardCounter;

    public GameObject _ProductInBrand_Panel;
    public void _UpdateTxt()
    {

        _ProductInBrand_Panel.SetActive(true);

        foreach (Transform child in _ChildBox_InBrand.transform)
        {
            Destroy(child.gameObject);
        }

        _RewardCounter = 0;

        foreach (Userdata.ProductData x in Userdata.Instance._RewardData.data)
        {
            GameObject _RewardPack = Instantiate(_ProductIcon, _ChildBox_InBrand);
            _RewardPack.GetComponent<RewardBtm>()._CoreReward = this.gameObject;
            _RewardPack.GetComponent<RewardBtm>().imageUrl = x.product_image;
            _RewardPack.GetComponent<RewardBtm>()._BandName = x.brands_name;
            _RewardPack.GetComponent<RewardBtm>()._id = _RewardCounter;
            _RewardPack.GetComponent<RewardBtm>()._Value.text = x.point.ToString();
            _RewardPack.GetComponent<RewardBtm>()._Type = RewardBtm._RewardBtm.RewardProduct;
            _RewardCounter++;


        }
    }

    public void _LoadProductInBrand(int _BrandId)
    {
        StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._GetRewardList(_BrandId));
    }

    public GameObject _RewardPopUpObj;
    public Image _Product_Image;
    public TMPro.TextMeshProUGUI _RewardName;
    public TMPro.TextMeshProUGUI _Des;
    public TMPro.TextMeshProUGUI _Point;

    //public TMPro.TextMeshProUGUI _Confirm_Point;
    //public TMPro.TextMeshProUGUI _Confirm_Conditon;
    //public Image _Confirm_Product_Image;

    public int _ProductId;
    public void _OpenPopUp(int _CurrentId)
    {
        _RewardPopUpObj.SetActive(true);
        _ProductId = _CurrentId;
        _RewardName.text = Userdata.Instance._RewardData.data[_CurrentId].name;
        _Des.text = Userdata.Instance._RewardData.data[_CurrentId].description;
        _Point.text = Userdata.Instance._RewardData.data[_CurrentId].point.ToString();

        //_Confirm_Point.text = Userdata.Instance._RewardData.data[_CurrentId].point.ToString();
        //_Confirm_Conditon.text = Userdata.Instance._RewardData.data[_CurrentId].condition;

        StartCoroutine(DownloadImage_Confirm(Userdata.Instance._RewardData.data[_CurrentId].product_image));
        StartCoroutine(DownloadImage(Userdata.Instance._RewardData.data[_CurrentId].product_image));
    }

    IEnumerator DownloadImage(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to load image: " + request.error);
        }
        else
        {
            Texture2D downloadedTexture = DownloadHandlerTexture.GetContent(request);
            Sprite sprite = Sprite.Create(downloadedTexture, new Rect(0, 0, downloadedTexture.width, downloadedTexture.height), new Vector2(0.5f, 0.5f));
            _Product_Image.sprite = sprite;
        }
    }

    IEnumerator DownloadImage_Confirm(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to load image: " + request.error);
        }
        else
        {
            Texture2D downloadedTexture = DownloadHandlerTexture.GetContent(request);
            Sprite sprite = Sprite.Create(downloadedTexture, new Rect(0, 0, downloadedTexture.width, downloadedTexture.height), new Vector2(0.5f, 0.5f));
            //_Confirm_Product_Image.sprite = sprite;
        }
    }

    public Transform _ChildBox_History;
    public GameObject _Product_History;
    public GameObject _Product_Panel;

    public void _LoadProductHistory()
    {
        StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._GetRewardHistory());
    }

    public void _UpdateHistory()
    {
        _Product_Panel.SetActive(true);

        foreach (Transform child in _ChildBox_History.transform)
        {
            Destroy(child.gameObject);
        }

        _RewardCounter = 0;

        Debug.Log("UPDATE HISTORY");

        foreach (Userdata.RedeemItem x in Userdata.Instance._RedeemResponse.data)
        {
            GameObject _RewardPack = Instantiate(_ProductIcon, _ChildBox_History);
            _RewardPack.GetComponent<RewardBtm>()._CoreReward = this.gameObject;
            _RewardPack.GetComponent<RewardBtm>().imageUrl = x.product_image;
            _RewardPack.GetComponent<RewardBtm>()._BandName = "";
            _RewardPack.GetComponent<RewardBtm>()._id = _RewardCounter;
            _RewardPack.GetComponent<RewardBtm>()._Value.text = x.point.ToString();
            _RewardPack.GetComponent<RewardBtm>()._Type = RewardBtm._RewardBtm.RewardHistory;
            _RewardCounter++;


        }
    }

    public GameObject _RedeemPanel;

    public TMPro.TextMeshProUGUI _RedeemName;
    public TMPro.TextMeshProUGUI _Code;
    public TMPro.TextMeshProUGUI _EndData;
    public TMPro.TextMeshProUGUI _Condiotion;

    public QRCodeEncodeController _Encoder;

    public void OpenRedeemPopUP(int _ProductId)
    {
        _RedeemPanel.SetActive(true);
        _RedeemName.text = Userdata.Instance._RedeemResponse.data[_ProductId].name;
        _Code.text = Userdata.Instance._RedeemResponse.data[_ProductId].product_barcode;
        _Encoder.GetComponent<QRCodeEncodeController>().Encode(Userdata.Instance._RedeemResponse.data[_ProductId].product_qrcode,QRCodeEncodeController.CodeMode.QR_CODE);
        _Encoder.GetComponent<QRCodeEncodeController>()._EncodeBarCode(Userdata.Instance._RedeemResponse.data[_ProductId].product_barcode, QRCodeEncodeController.CodeMode.CODE_128);
        _EndData.text = Userdata.Instance._RedeemResponse.data[_ProductId].end_date;
        _Condiotion.text = Userdata.Instance._RedeemResponse.data[_ProductId].condition;
    }

    public void _RedeemThisReward()
    {
        if(Userdata.Instance._User.data.currency.vision_point <= Userdata.Instance._RewardData.data[_ProductId].point)
        {
            Debug.Log("Not");
        }
        else
        {
            StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._Redeem_Reward(Userdata.Instance._RewardData.data[_ProductId].id));
        }
        
    }

    public GameObject _ConfirmRewardPopUp;
    public GameObject _RewardPopUp;

    public void _RedeemThisRewardOK()
    {
        _LoadProductHistory();
        _ConfirmRewardPopUp.SetActive(false);
        _RewardPopUp.SetActive(false);
    }

}
