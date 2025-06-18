using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class ApiCaller : MonoBehaviour
{
    public string _Url = "http://3.0.20.44:8080";

    public delegate void GeneralDelegate();

    public GeneralDelegate OnCall_GetInventory_OK;
    public GeneralDelegate OnCall_GetInventory_Failed;

    [System.Serializable]
    public class EquipmentResponse
    {
        public string status;
        public List<int> equipments;
    }
    
    public IEnumerator GetRequest(int _Type)
    {
        switch (_Type)
        {
            case 0:
                using (UnityWebRequest www = UnityWebRequest.Get(_Url + "/api/equipment/?type=all"))
                {
                    Debug.Log("Call GetRequest");
                    www.SetRequestHeader("Authorization", "Bearer " + Userdata.Instance._User.data.access_token);
                    www.SetRequestHeader("Accept", "application/json");
                    www.SetRequestHeader("Content-Type", "application/json");
                    yield return www.SendWebRequest();

                    if (www.result == UnityWebRequest.Result.ProtocolError || www.result == UnityWebRequest.Result.ConnectionError)
                    {
                        Debug.LogError($"Error: {www.error}");
                        OnCall_GetInventory_Failed?.Invoke();
                    }
                    else
                    {
                        Debug.Log("GET Inventory : " + www.downloadHandler.text);
                        EquipmentResponse data = new EquipmentResponse();
                        data = JsonUtility.FromJson<EquipmentResponse>(www.downloadHandler.text);
                        Userdata.Instance._InventroyStore = data.equipments;

                        OnCall_GetInventory_OK?.Invoke();
                    }
                }
                break;
            case 1:
                using (UnityWebRequest www = UnityWebRequest.Get(_Url + "/api/equipment/?type=weapon"))
                {
                    Debug.Log("Call GetRequest");
                    www.SetRequestHeader("Authorization", "Bearer " + Userdata.Instance._User.data.access_token);
                    www.SetRequestHeader("Accept", "application/json");
                    www.SetRequestHeader("Content-Type", "application/json");
                    yield return www.SendWebRequest();

                    if (www.result == UnityWebRequest.Result.ProtocolError || www.result == UnityWebRequest.Result.ConnectionError)
                    {
                        Debug.LogError($"Error: {www.error}");
                        OnCall_GetInventory_Failed?.Invoke();
                    }
                    else
                    {
                        Debug.Log("GET Inventory : " + www.downloadHandler.text);
                        EquipmentResponse data = new EquipmentResponse();
                        data = JsonUtility.FromJson<EquipmentResponse>(www.downloadHandler.text);
                        Userdata.Instance._InventroyStore = data.equipments;

                        OnCall_GetInventory_OK?.Invoke();
                    }
                }
                break;
            case 2:
                using (UnityWebRequest www = UnityWebRequest.Get(_Url + "/api/equipment/?type=body"))
                {
                    Debug.Log("Call GetRequest");
                    www.SetRequestHeader("Authorization", "Bearer " + Userdata.Instance._User.data.access_token);
                    www.SetRequestHeader("Accept", "application/json");
                    www.SetRequestHeader("Content-Type", "application/json");
                    yield return www.SendWebRequest();

                    if (www.result == UnityWebRequest.Result.ProtocolError || www.result == UnityWebRequest.Result.ConnectionError)
                    {
                        Debug.LogError($"Error: {www.error}");
                        OnCall_GetInventory_Failed?.Invoke();
                    }
                    else
                    {
                        Debug.Log("GET Inventory : " + www.downloadHandler.text);
                        EquipmentResponse data = new EquipmentResponse();
                        data = JsonUtility.FromJson<EquipmentResponse>(www.downloadHandler.text);
                        Userdata.Instance._InventroyStore = data.equipments;

                        OnCall_GetInventory_OK?.Invoke();
                    }
                }
                break;
            case 3:
                using (UnityWebRequest www = UnityWebRequest.Get(_Url + "/api/equipment/?type=helmet"))
                {
                    Debug.Log("Call GetRequest");
                    www.SetRequestHeader("Authorization", "Bearer " + Userdata.Instance._User.data.access_token);
                    www.SetRequestHeader("Accept", "application/json");
                    www.SetRequestHeader("Content-Type", "application/json");
                    yield return www.SendWebRequest();

                    if (www.result == UnityWebRequest.Result.ProtocolError || www.result == UnityWebRequest.Result.ConnectionError)
                    {
                        Debug.LogError($"Error: {www.error}");
                        OnCall_GetInventory_Failed?.Invoke();
                    }
                    else
                    {
                        Debug.Log("GET Inventory : " + www.downloadHandler.text);
                        EquipmentResponse data = new EquipmentResponse();
                        data = JsonUtility.FromJson<EquipmentResponse>(www.downloadHandler.text);
                        Userdata.Instance._InventroyStore = data.equipments;

                        OnCall_GetInventory_OK?.Invoke();
                    }
                }
                break;

        }
        
    }

    public GeneralDelegate OnCall_AddItem_OK;
    public GeneralDelegate OnCall_AddItem_Failed;

    [System.Serializable]
    public class ValueContainer
    {
        public List<int> value = new List<int>();
    }


    public IEnumerator _AddItem(int _ItemId,int _Type)
    {

        DiscardItem.value.Clear();
        DiscardItem.value.Add(_ItemId);

        string json = JsonUtility.ToJson(DiscardItem);
        Debug.Log(json);
        var request = new UnityWebRequest(_Url + "/api/equipment", "POST");
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

            OnCall_GetInventory_Failed?.Invoke();
        }
        else
        {
            //Userdata.Instance._User = JsonUtility.FromJson<Userdata.LoginResponse>(request.downloadHandler.text);
            StartCoroutine(GetRequest(_Type));
        }
    }

    public ValueContainer DiscardItem;
    public IEnumerator _DiscardItem(int _ItemId,int _Type)
    {

        Debug.Log("Current Discard Item Id : " + _ItemId);
        DiscardItem.value.Clear();
        DiscardItem.value.Add(_ItemId);

        string json = JsonUtility.ToJson(DiscardItem);
        Debug.Log(json);
        var request = new UnityWebRequest(_Url + "/api/equipment", "DELETE");
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

            OnCall_GetInventory_Failed?.Invoke();
        }
        else
        {
            //Userdata.Instance._User = JsonUtility.FromJson<Userdata.LoginResponse>(request.downloadHandler.text);
            StartCoroutine(GetRequest(_Type));
           // OnCall_GetInventory_OK?.Invoke();
            //Debug.Log("GET Inventory : " + request.downloadHandler.text);
        }
    }
    public GeneralDelegate OnCall_UpdateCurrentWare_OK;
    public GeneralDelegate OnCall_UpdateCurrentWare_Failed;

    public IEnumerator _UpdateCurrentWare(int _ItemId, int _CurrentUI)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(_Url + "/api/currentware"))
        {
            Debug.Log("Call GetRequest");
            www.SetRequestHeader("Authorization", "Bearer " + Userdata.Instance._User.data.access_token);
            www.SetRequestHeader("Accept", "application/json");
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ProtocolError || www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError($"Error: {www.error}");
                OnCall_UpdateCurrentWare_Failed?.Invoke();
            }
            else
            {

                Debug.Log("GET Inventory : " + www.downloadHandler.text);
                CurrentWareResponse data = new CurrentWareResponse();
                data = JsonUtility.FromJson<CurrentWareResponse>(www.downloadHandler.text);
                Userdata.Instance._User.data.current_ware.current_hat = data.current_ware.current_hat;
                Userdata.Instance._User.data.current_ware.current_body = data.current_ware.current_body;
                Userdata.Instance._User.data.current_ware.current_weapon = data.current_ware.current_weapon;

                StartCoroutine(_AddItem(_ItemId, _CurrentUI));
            }
        }
    }

    [System.Serializable]
    public class CurrentWareResponse
    {
        public string status;
        public CurrentWare current_ware;
    }

    [System.Serializable]
    public class CurrentWare
    {
        public int current_hat;
        public int current_body;
        public int current_weapon;
    }


    [System.Serializable]
    public class Ware
    {
        public int hat;
        public int body;
        public int weapon;
    }

    public IEnumerator _WareItem(int _CurrentItemId,int _Type)
    {

            Debug.Log("Call _WareItem");
            Ware data = new Ware();
            data.hat = Userdata.Instance._User.data.current_ware.current_hat;
            data.body = Userdata.Instance._User.data.current_ware.current_body;
            data.weapon = Userdata.Instance._User.data.current_ware.current_weapon;

            string json = JsonUtility.ToJson(data);
            Debug.Log(json);
            var request = new UnityWebRequest(_Url + "/api/currentware/all", "PATCH");
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

                OnCall_GetInventory_Failed?.Invoke();
            }
            else
            {
                StartCoroutine(_DiscardItem(_CurrentItemId, _Type));
            }
    }

    public IEnumerator _RemoveItem(int _ItemId,int _Type,int _CurrentUI)
    {
            Ware data = new Ware();
            switch (_Type)
            {
                case 1:
                    data.hat = 0;
                    data.body = Userdata.Instance._User.data.current_ware.current_body;
                    data.weapon = Userdata.Instance._User.data.current_ware.current_weapon;
                break;
                case 2:
                    data.hat = Userdata.Instance._User.data.current_ware.current_hat;
                    data.body = 0;
                    data.weapon = Userdata.Instance._User.data.current_ware.current_weapon;
                break;
                case 3:
                    data.hat = Userdata.Instance._User.data.current_ware.current_hat;
                    data.body = Userdata.Instance._User.data.current_ware.current_body;
                    data.weapon = 0;
                break;
            }

            string json = JsonUtility.ToJson(data);
            Debug.Log(json);
            var request = new UnityWebRequest(_Url + "/api/currentware/all", "PATCH");
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

                OnCall_GetInventory_Failed?.Invoke();
            }
            else
            {
                StartCoroutine(_UpdateCurrentWare(_ItemId, _CurrentUI));
            }
    }

    [System.Serializable]
    public class CurrencyResponse
    {
        public string status;
        public Currency currency;
    }

    [System.Serializable]
    public class Currency
    {
        public int gold;
        public int gem;
        public int vision_point;
    }


    public GeneralDelegate OnCall_UpdateCurrency_OK;
    public IEnumerator _UpdateCurrencyValue()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(_Url + "/api/currency"))
        {
            Debug.Log("Call GetRequest");
            www.SetRequestHeader("Authorization", "Bearer " + Userdata.Instance._User.data.access_token);
            www.SetRequestHeader("Accept", "application/json");
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ProtocolError || www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError($"Error: {www.error}");
                //OnCall_UpdateCurrentWare_Failed?.Invoke();
            }
            else
            {

                Debug.Log("GET Inventory : " + www.downloadHandler.text);
                CurrencyResponse data = new CurrencyResponse();
                data = JsonUtility.FromJson<CurrencyResponse>(www.downloadHandler.text);
                Userdata.Instance._User.data.currency.gold = data.currency.gold;
                Userdata.Instance._User.data.currency.gem = data.currency.gem;
                Userdata.Instance._User.data.currency.vision_point = data.currency.vision_point;

                OnCall_UpdateCurrency_OK?.Invoke();
            }
        }
    }

    public class CurrencyUpdate
    {
        public int gold;
        public int gem;
        public int vision;
    }

    public IEnumerator _AddCurreny(int _CurrenyValue, int _Type)
    {
        CurrencyUpdate data = new CurrencyUpdate();
        switch (_Type)
        {
            case 0:
                data.gold = Userdata.Instance._User.data.currency.gold+_CurrenyValue;
                data.gem = Userdata.Instance._User.data.currency.gem;
                data.vision = Userdata.Instance._User.data.currency.vision_point;
                break;
            case 1:
                data.gold = Userdata.Instance._User.data.currency.gold;
                data.gem = Userdata.Instance._User.data.currency.gem + _CurrenyValue;
                data.vision = Userdata.Instance._User.data.currency.vision_point;
                break;
            case 2:
                data.gold = Userdata.Instance._User.data.currency.gold;
                data.gem = Userdata.Instance._User.data.currency.gem;
                data.vision = Userdata.Instance._User.data.currency.vision_point + _CurrenyValue;
                break;
        }
        

        string json = JsonUtility.ToJson(data);
        Debug.Log(json);
        var request = new UnityWebRequest(_Url + "/api/currency/all", "PATCH");
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
            StartCoroutine(_UpdateCurrencyValue());
        }
    }

    public GeneralDelegate OnCall_GetRewardList_OK;

    public class RequestData_GetRewardList
    {
        public int brand_id;
        public int? category_id;
        public int? product_name;
        public int offset;
        public int limit;
    }

    public IEnumerator _GetRewardList(int _BandId)
    {

        RequestData_GetRewardList _temp = new RequestData_GetRewardList();
        _temp.brand_id = _BandId;
        _temp.offset = 0;
        _temp.limit = 1000;

        json = JsonUtility.ToJson(_temp);

        Debug.Log(json);
        var request = new UnityWebRequest(_Url + "/api/reward-list", "POST");
        request.SetRequestHeader("Authorization", "Bearer " + Userdata.Instance._User.data.access_token);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Accept", "application/json");
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();
        Debug.Log("request responseText:" + request.downloadHandler.text);

        if (request.result != UnityWebRequest.Result.Success)
        {

            //OnCall_GetInventory_Failed?.Invoke();
        }
        else
        {
            Debug.Log("GET Reward : " + request.downloadHandler.text);
            Userdata.RewardResponse data = new Userdata.RewardResponse();
            data = JsonUtility.FromJson<Userdata.RewardResponse>(request.downloadHandler.text);
            Userdata.Instance._RewardData = data;

            OnCall_GetRewardList_OK?.Invoke();
        }

    }

    public GeneralDelegate OnCall_GetBand_OK;

    public class RequestData
    {
        public int category_id;
        public int offset;
        public int limit;
    }

    public class RequestData_null
    {
        public int? category_id;
        public int offset;
        public int limit;
    }

    string json;
    public IEnumerator _GetRewardList_Band(int CategoryId)
    {
        if (CategoryId == 0)
        {
            RequestData_null data = new RequestData_null();
            data.offset = 0;
            data.limit = 1000;

            json = JsonUtility.ToJson(data);
        }
        else
        {
            RequestData data = new RequestData();
            data.category_id = CategoryId;
            data.offset = 0;
            data.limit = 1000;

            json = JsonUtility.ToJson(data);
        }

        Debug.Log(json);
        var request = new UnityWebRequest(_Url + "/api/brand-list", "POST");
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
            Debug.Log("GET Reward : " + request.downloadHandler.text);
            Userdata.ApiResponse _temp = new Userdata.ApiResponse();
            _temp = JsonUtility.FromJson<Userdata.ApiResponse>(request.downloadHandler.text);
            Userdata.Instance._RewardGetBrand = _temp;

            OnCall_GetBand_OK?.Invoke();
        }
    }


    public GeneralDelegate OnCall_GetRewardList_Category_OK;

    public IEnumerator _GetRewardList_Category()
    {

        string json = "";
        Debug.Log(json);
        var request = new UnityWebRequest(_Url + "/api/reward-category", "POST");
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
            Debug.Log("GET Reward : " + request.downloadHandler.text);
            Userdata.CategoryResponse data = new Userdata.CategoryResponse();
            data = JsonUtility.FromJson<Userdata.CategoryResponse>(request.downloadHandler.text);
            Userdata.Instance._RewardCategoryData = data;

            OnCall_GetRewardList_Category_OK?.Invoke();
        }
    }

    public class Redeem_history
    {
        public int? brand_id;
        public int? category_id;
        public string? start_date;
        public string? end_date;
        public string? product_name;
        public int offset;
        public int limit;
    }

    public GeneralDelegate OnCall_GetRewardHistory_OK;

    public IEnumerator _GetRewardHistory()
    {

        Redeem_history _temp = new Redeem_history();
        _temp.offset = 0;
        _temp.limit = 1000;

        json = JsonUtility.ToJson(_temp);

        Debug.Log(json);
        var request = new UnityWebRequest(_Url + "/api/redeem-history", "POST");
        request.SetRequestHeader("Authorization", "Bearer " + Userdata.Instance._User.data.access_token);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Accept", "application/json");
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();
        Debug.Log("request responseText:" + request.downloadHandler.text);

        if (request.result != UnityWebRequest.Result.Success)
        {

            //OnCall_GetInventory_Failed?.Invoke();
        }
        else
        {
            Debug.Log("GET Reward : " + request.downloadHandler.text);
            Userdata.RedeemResponse data = new Userdata.RedeemResponse();
            data = JsonUtility.FromJson<Userdata.RedeemResponse>(request.downloadHandler.text);
            Userdata.Instance._RedeemResponse = data;

            OnCall_GetRewardHistory_OK?.Invoke();
        }

    }

    public GeneralDelegate OnCall_RedeemReward_OK;
    public class _RedeemReward
    {
        public string product_id;
    }



    public IEnumerator _Redeem_Reward(int _ProductReward)
    {

        _RedeemReward _temp = new _RedeemReward();
        _temp.product_id = _ProductReward.ToString();

        json = JsonUtility.ToJson(_temp);

        Debug.Log(json);
        var request = new UnityWebRequest(_Url + "/api/redeem-reward", "POST");
        request.SetRequestHeader("Authorization", "Bearer " + Userdata.Instance._User.data.access_token);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Accept", "application/json");
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();
        Debug.Log("request responseText:" + request.downloadHandler.text);

        if (request.result != UnityWebRequest.Result.Success)
        {

            //OnCall_GetInventory_Failed?.Invoke();
        }
        else
        {
            Debug.Log("GET Reward : " + request.downloadHandler.text);
             Userdata.RewardResponse_OK data = new Userdata.RewardResponse_OK();
            data = JsonUtility.FromJson<Userdata.RewardResponse_OK>(request.downloadHandler.text);
            Userdata.Instance._RewardResponse_OK = data;

            //OnCall_GetRewardHistory_OK?.Invoke();
            OnCall_RedeemReward_OK?.Invoke();
        }

    }

    public GeneralDelegate OnCall_GetWorldData_OK;

    public IEnumerator _GetWorldData()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(_Url + "/api/game/world"))
        {
            Debug.Log(_Url + "/api/game/world");
            www.SetRequestHeader("Authorization", "Bearer " + Userdata.Instance._User.data.access_token);
            www.SetRequestHeader("Accept", "application/json");
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ProtocolError || www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError($"Error: {www.error}");
                
            }
            else
            {

                Debug.Log("_GetWorldData : " + www.downloadHandler.text);
                Userdata.GameWorldData data = new Userdata.GameWorldData();
                data = JsonUtility.FromJson<Userdata.GameWorldData>(www.downloadHandler.text);
                Userdata.Instance._WorldData = data;
                OnCall_GetWorldData_OK?.Invoke();
                //StartCoroutine(_AddItem(_ItemId, _CurrentUI));
            }
        }

    }

    public GeneralDelegate OnCall_BuyBooster_OK;

    public class _BoosterClass
    {
        public int booster1;
        public int booster2;
        public int booster3;
        public int booster4;
    }

    public IEnumerator _BuyBooster(int _Booster_1,int _Booster_2,int _Booster_3,int _Booster_4)
    {
        Debug.Log("Call _WareItem");
        _BoosterClass data = new _BoosterClass();
        data.booster1 = _Booster_1;
        data.booster2 = _Booster_2;
        data.booster3 = _Booster_3;
        data.booster4 = _Booster_4;

        string json = JsonUtility.ToJson(data);
        Debug.Log(json);
        var request = new UnityWebRequest(_Url + "/api/booster/all", "PATCH");
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
            OnCall_BuyBooster_OK?.Invoke();
            //StartCoroutine(_DiscardItem(_CurrentItemId, _Type));
        }
    }
}
