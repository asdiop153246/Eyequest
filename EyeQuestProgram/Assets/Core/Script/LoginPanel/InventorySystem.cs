using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Android;

public class InventorySystem : MonoBehaviour
{
    public List<_ItemData> _ItemStore;

    
    public enum _itemType
    {
        Hat=1,Body=2,Weapon=3
    }

    [System.Serializable]
    public class _ItemData
    {
        public string _ItemName;
        public string _ItemDes;
        public Sprite _ItemIcon;
        public Sprite _StoreIcon;
        public _itemType _Type;
        public int _price;
    }
    public GameObject _MainPanel;
    public GameObject _InventoryPanel;

    //public GameObject _Loading;

    public void OnEnable()
    {
        Userdata.Instance.gameObject.GetComponent<ApiCaller>().OnCall_GetInventory_OK += () =>
        {
            _ItemPopUp.SetActive(false);
            _RemovePopUpPanel.SetActive(false);
            _UpdateCurrentWare();
            _UpdateItemList_Only();
        };

        /*Userdata.Instance.gameObject.GetComponent<ApiCaller>().OnCall_UpdateCurrentWare_OK += () =>
        {
            _UpdateCurrentWare();
        };*/
    }

    public void OnDisable()
    {
        Userdata.Instance.gameObject.GetComponent<ApiCaller>().OnCall_GetInventory_OK -= Userdata.Instance.gameObject.GetComponent<ApiCaller>().OnCall_GetInventory_OK;
        //Userdata.Instance.gameObject.GetComponent<ApiCaller>().OnCall_UpdateCurrentWare_OK -= Userdata.Instance.gameObject.GetComponent<ApiCaller>().OnCall_UpdateCurrentWare_OK;

    }

    public void _GetInventory()
    {
        Debug.Log("Call Get Inventory");
        StartCoroutine(Userdata.Instance.gameObject.GetComponent<ApiCaller>().GetRequest(0));
    }

    public void _GetByType(int _i)
    {
        _CurrentOnUI_Type = _i;
        StartCoroutine(Userdata.Instance.gameObject.GetComponent<ApiCaller>().GetRequest(_i));
    }

    public List<GameObject> _AllSlot;

    public void _UpdateItemList_Only()
    {
        foreach (GameObject itemUI in _AllSlot)
        { 
            itemUI.GetComponent<Button>().interactable = false;
            itemUI.transform.GetChild(0).gameObject.SetActive(false);
        }

        for (int i = 0; i < Userdata.Instance._InventroyStore.Count; i++)
        {
            _AllSlot[i].transform.GetChild(0).gameObject.SetActive(true);

            if (_ItemStore[Userdata.Instance._InventroyStore[i]]._ItemIcon != null)
            {
                _AllSlot[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = _ItemStore[Userdata.Instance._InventroyStore[i]]._ItemIcon;
                Debug.Log(Userdata.Instance._InventroyStore[i]);
            }

            _AllSlot[i].GetComponent<Button>().interactable = true;


        }

        _MainPanel.SetActive(false);
        _InventoryPanel.SetActive(true);
    }

    public GameObject _ItemPopUp;
    public TMPro.TextMeshProUGUI _ItemPop_Name;
    public TMPro.TextMeshProUGUI _ItemPop_Des;
    public Image _ItemPopIcon;

    public int _CurrentSelectItemId;
    public int _CurrentOnUI_Type;

    public void _ShowPopUp(int _Slot)
    {

        _ItemPop_Name.text = _ItemStore[Userdata.Instance._InventroyStore[_Slot]]._ItemName;
        _ItemPop_Des.text = _ItemStore[Userdata.Instance._InventroyStore[_Slot]]._ItemDes;
        _ItemPopIcon.sprite = _ItemStore[Userdata.Instance._InventroyStore[_Slot]]._ItemIcon;
        _CurrentSelectItemId = Userdata.Instance._InventroyStore[_Slot];
        _CurrentSelectitemType = (int)_ItemStore[Userdata.Instance._InventroyStore[_Slot]]._Type;
        _ItemPopUp.SetActive(true);
    }

    public void _DiscardItem()
    {
        StartCoroutine(Userdata.Instance.gameObject.GetComponent<ApiCaller>()._DiscardItem(_CurrentSelectItemId, _CurrentOnUI_Type));
    }

    public int _CurrentSelectitemType;

    public GameObject _CurrentHat;
    public GameObject _CurrentBody;
    public GameObject _CurrentWeapon;

    public void _WareItem()
    {
        switch (_CurrentSelectitemType)
        {
            case 3:
                Userdata.Instance._User.data.current_ware.current_weapon = _CurrentSelectItemId;
                StartCoroutine(Userdata.Instance.gameObject.GetComponent<ApiCaller>()._WareItem(_CurrentSelectItemId, _CurrentOnUI_Type));
                break;
            case 2:
                Userdata.Instance._User.data.current_ware.current_body = _CurrentSelectItemId;
                StartCoroutine(Userdata.Instance.gameObject.GetComponent<ApiCaller>()._WareItem(_CurrentSelectItemId, _CurrentOnUI_Type));
                break;
            case 1:
                Userdata.Instance._User.data.current_ware.current_hat = _CurrentSelectItemId;
                StartCoroutine(Userdata.Instance.gameObject.GetComponent<ApiCaller>()._WareItem(_CurrentSelectItemId, _CurrentOnUI_Type));
                break;

        }
    }
    public void _UpdateCurrentWare()
    {

        Debug.Log("GET CURRENT WARE : " + Userdata.Instance._User.data.current_ware.current_hat + "," + Userdata.Instance._User.data.current_ware.current_body + "," + Userdata.Instance._User.data.current_ware.current_weapon);
        if (Userdata.Instance._User.data.current_ware.current_hat != 0)
        {
            _CurrentHat.transform.GetChild(0).GetComponent<Image>().sprite = _ItemStore[Userdata.Instance._User.data.current_ware.current_hat]._ItemIcon;
            _CurrentHat.transform.GetChild(0).gameObject.SetActive(true);
            _CurrentHat.GetComponent<Button>().interactable = true;
        }
        else
        {
            _CurrentHat.transform.GetChild(0).gameObject.SetActive(false);
            _CurrentHat.GetComponent<Button>().interactable = false;

        }

        if (Userdata.Instance._User.data.current_ware.current_body != 0)
        {
            _CurrentBody.transform.GetChild(0).GetComponent<Image>().sprite = _ItemStore[Userdata.Instance._User.data.current_ware.current_body]._ItemIcon;
            _CurrentBody.transform.GetChild(0).gameObject.SetActive(true);
            _CurrentBody.GetComponent<Button>().interactable = true;

        }
        else
        {
            _CurrentBody.transform.GetChild(0).gameObject.SetActive(false);
            _CurrentBody.GetComponent<Button>().interactable = false;
        }

        if (Userdata.Instance._User.data.current_ware.current_weapon != 0)
        {
            _CurrentWeapon.transform.GetChild(0).GetComponent<Image>().sprite = _ItemStore[Userdata.Instance._User.data.current_ware.current_weapon]._ItemIcon;
            _CurrentWeapon.transform.GetChild(0).gameObject.SetActive(true);
            _CurrentWeapon.GetComponent<Button>().interactable = true;
        }
        else
        {
            _CurrentWeapon.transform.GetChild(0).gameObject.SetActive(false);
            _CurrentWeapon.GetComponent<Button>().interactable = false;
        }

        _ItemPopUp.SetActive(false);
    }

    public GameObject _RemovePopUpPanel;
    public TMPro.TextMeshProUGUI _RemovePopUp_Name;
    public TMPro.TextMeshProUGUI _RemovePopUp_Des;
    public Image _RemovePopUpIcon;

    public int _CurrentRemoveItemId;
    public int _typeId;

    public void _ShowRemovePopUp(int _id)
    {
        _typeId = _id;
        switch (_id)
        {
            case 1: // Hat
                _RemovePopUp_Name.text = _ItemStore[Userdata.Instance._User.data.current_ware.current_hat]._ItemName;
                _RemovePopUp_Des.text = _ItemStore[Userdata.Instance._User.data.current_ware.current_hat]._ItemDes;
                _RemovePopUpIcon.sprite = _ItemStore[Userdata.Instance._User.data.current_ware.current_hat]._ItemIcon;
                _CurrentRemoveItemId = Userdata.Instance._User.data.current_ware.current_hat;
                //_CurrentSelectitemType = (int)_ItemStore[Userdata.Instance._InventroyStore[_Slot]]._Type;
                break;
            case 2: // Body
                _RemovePopUp_Name.text = _ItemStore[Userdata.Instance._User.data.current_ware.current_body]._ItemName;
                _RemovePopUp_Des.text = _ItemStore[Userdata.Instance._User.data.current_ware.current_body]._ItemDes;
                _RemovePopUpIcon.sprite = _ItemStore[Userdata.Instance._User.data.current_ware.current_body]._ItemIcon;
                _CurrentRemoveItemId = Userdata.Instance._User.data.current_ware.current_body;
                break;
            case 3: // Weapon
                _RemovePopUp_Name.text = _ItemStore[Userdata.Instance._User.data.current_ware.current_weapon]._ItemName;
                _RemovePopUp_Des.text = _ItemStore[Userdata.Instance._User.data.current_ware.current_weapon]._ItemDes;
                _RemovePopUpIcon.sprite = _ItemStore[Userdata.Instance._User.data.current_ware.current_weapon]._ItemIcon;
                _CurrentRemoveItemId = Userdata.Instance._User.data.current_ware.current_weapon;
                break;
        }


        _RemovePopUpPanel.SetActive(true);
    }

    public void _Removeware()
    {
        StartCoroutine(Userdata.Instance.gameObject.GetComponent<ApiCaller>()._RemoveItem(_CurrentRemoveItemId, _typeId, _CurrentOnUI_Type));
    }

    public void _UsePotion(int _id)
    {
        switch (_id)
        {
            case 0:
                Userdata.Instance._isUsePotion_A = true;
                break;
            case 1:
                Userdata.Instance._isUsePotion_B = true;
                break;
            case 2:
                Userdata.Instance._isUsePotion_C = true;
                break;
            case 3:
                Userdata.Instance._isUsePotion_D = true;
                break;
        }
    }
}
