using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> _Packaget;
    public List<GameObject> _MarketSlot_IMG;
    public List<GameObject> _PricesTxt;

    public TMPro.TextMeshProUGUI _HeaderTxt;

    public GameObject _MarketObj;
    public void _UpdateStoreByWord(int _id)
    {
        _MarketObj.SetActive(true);
        _HeaderTxt.text = "World " + Userdata.Instance._CurrentWorld + " - " + Userdata.Instance._CurrentStage;
        // 0-4 - SHOP World 1 
        // 1-5 // 1-13 - SHOP World 2 
        // 2-3 // 2-8 - SHOP World 3 

        foreach(GameObject x in _Packaget)
        {
            x.SetActive(false);
        }

        switch (Userdata.Instance._CurrentWorld)
        {
            case 0:
                _MarketSlot_IMG[0].GetComponent<Image>().sprite = GetComponent<InventorySystem>()._ItemStore[4]._StoreIcon;
                _MarketSlot_IMG[1].GetComponent<Image>().sprite = GetComponent<InventorySystem>()._ItemStore[5]._StoreIcon;
                _MarketSlot_IMG[2].GetComponent<Image>().sprite = GetComponent<InventorySystem>()._ItemStore[6]._StoreIcon;

                _MarketSlot_IMG[3].GetComponent<Image>().sprite = GetComponent<InventorySystem>()._ItemStore[7]._StoreIcon;
                _MarketSlot_IMG[4].GetComponent<Image>().sprite = GetComponent<InventorySystem>()._ItemStore[8]._StoreIcon;
                _MarketSlot_IMG[5].GetComponent<Image>().sprite = GetComponent<InventorySystem>()._ItemStore[9]._StoreIcon;

                _PricesTxt[0].GetComponent<TMPro.TextMeshProUGUI>().text = GetComponent<InventorySystem>()._ItemStore[4]._price+"";
                _PricesTxt[1].GetComponent<TMPro.TextMeshProUGUI>().text = GetComponent<InventorySystem>()._ItemStore[5]._price + "";
                _PricesTxt[2].GetComponent<TMPro.TextMeshProUGUI>().text = GetComponent<InventorySystem>()._ItemStore[6]._price + "";

                _PricesTxt[3].GetComponent<TMPro.TextMeshProUGUI>().text = GetComponent<InventorySystem>()._ItemStore[7]._price + "";
                _PricesTxt[4].GetComponent<TMPro.TextMeshProUGUI>().text = GetComponent<InventorySystem>()._ItemStore[8]._price + "";
                _PricesTxt[5].GetComponent<TMPro.TextMeshProUGUI>().text = GetComponent<InventorySystem>()._ItemStore[9]._price + "";

                _Packaget[0].GetComponent<MarketSlot>()._id = 4;
                _Packaget[1].GetComponent<MarketSlot>()._id = 5;
                _Packaget[2].GetComponent<MarketSlot>()._id = 6;
                _Packaget[3].GetComponent<MarketSlot>()._id = 7;
                _Packaget[4].GetComponent<MarketSlot>()._id = 8;
                _Packaget[5].GetComponent<MarketSlot>()._id = 9;

                _Packaget[0].SetActive(true);
                _Packaget[1].SetActive(true);
                _Packaget[2].SetActive(true);
                _Packaget[3].SetActive(true);
                _Packaget[4].SetActive(true);
                _Packaget[5].SetActive(true);
                break;

            case 1:
                if (_id == 5)
                {
                    _MarketSlot_IMG[0].GetComponent<Image>().sprite = GetComponent<InventorySystem>()._ItemStore[10]._StoreIcon;
                    _MarketSlot_IMG[1].GetComponent<Image>().sprite = GetComponent<InventorySystem>()._ItemStore[11]._StoreIcon;
                    _MarketSlot_IMG[2].GetComponent<Image>().sprite = GetComponent<InventorySystem>()._ItemStore[12]._StoreIcon;

                    _MarketSlot_IMG[3].GetComponent<Image>().sprite = GetComponent<InventorySystem>()._ItemStore[13]._StoreIcon;
                    _MarketSlot_IMG[4].GetComponent<Image>().sprite = GetComponent<InventorySystem>()._ItemStore[14]._StoreIcon;
                    _MarketSlot_IMG[5].GetComponent<Image>().sprite = GetComponent<InventorySystem>()._ItemStore[15]._StoreIcon;

                    _PricesTxt[0].GetComponent<TMPro.TextMeshProUGUI>().text = GetComponent<InventorySystem>()._ItemStore[10]._price + "";
                    _PricesTxt[1].GetComponent<TMPro.TextMeshProUGUI>().text = GetComponent<InventorySystem>()._ItemStore[11]._price + "";
                    _PricesTxt[2].GetComponent<TMPro.TextMeshProUGUI>().text = GetComponent<InventorySystem>()._ItemStore[12]._price + "";

                    _PricesTxt[3].GetComponent<TMPro.TextMeshProUGUI>().text = GetComponent<InventorySystem>()._ItemStore[13]._price + "";
                    _PricesTxt[4].GetComponent<TMPro.TextMeshProUGUI>().text = GetComponent<InventorySystem>()._ItemStore[14]._price + "";
                    _PricesTxt[5].GetComponent<TMPro.TextMeshProUGUI>().text = GetComponent<InventorySystem>()._ItemStore[15]._price + "";

                    _Packaget[0].GetComponent<MarketSlot>()._id = 10;
                    _Packaget[1].GetComponent<MarketSlot>()._id = 11;
                    _Packaget[2].GetComponent<MarketSlot>()._id = 12;
                    _Packaget[3].GetComponent<MarketSlot>()._id = 13;
                    _Packaget[4].GetComponent<MarketSlot>()._id = 14;
                    _Packaget[5].GetComponent<MarketSlot>()._id = 15;

                    _Packaget[0].SetActive(true);
                    _Packaget[1].SetActive(true);
                    _Packaget[2].SetActive(true);
                    _Packaget[3].SetActive(true);
                    _Packaget[4].SetActive(true);
                    _Packaget[5].SetActive(true);

                }

                if (_id == 13)
                {
                    _MarketSlot_IMG[0].GetComponent<Image>().sprite = GetComponent<InventorySystem>()._ItemStore[16]._StoreIcon;
                    _MarketSlot_IMG[1].GetComponent<Image>().sprite = GetComponent<InventorySystem>()._ItemStore[17]._StoreIcon;
                    _MarketSlot_IMG[2].GetComponent<Image>().sprite = GetComponent<InventorySystem>()._ItemStore[18]._StoreIcon;

                    _MarketSlot_IMG[3].GetComponent<Image>().sprite = GetComponent<InventorySystem>()._ItemStore[19]._StoreIcon;
                    _MarketSlot_IMG[4].GetComponent<Image>().sprite = GetComponent<InventorySystem>()._ItemStore[20]._StoreIcon;
                    _MarketSlot_IMG[5].GetComponent<Image>().sprite = GetComponent<InventorySystem>()._ItemStore[21]._StoreIcon;

                    _PricesTxt[0].GetComponent<TMPro.TextMeshProUGUI>().text = GetComponent<InventorySystem>()._ItemStore[16]._price + "";
                    _PricesTxt[1].GetComponent<TMPro.TextMeshProUGUI>().text = GetComponent<InventorySystem>()._ItemStore[17]._price + "";
                    _PricesTxt[2].GetComponent<TMPro.TextMeshProUGUI>().text = GetComponent<InventorySystem>()._ItemStore[18]._price + "";

                    _PricesTxt[3].GetComponent<TMPro.TextMeshProUGUI>().text = GetComponent<InventorySystem>()._ItemStore[19]._price + "";
                    _PricesTxt[4].GetComponent<TMPro.TextMeshProUGUI>().text = GetComponent<InventorySystem>()._ItemStore[20]._price + "";
                    _PricesTxt[5].GetComponent<TMPro.TextMeshProUGUI>().text = GetComponent<InventorySystem>()._ItemStore[21]._price + "";

                    _Packaget[0].GetComponent<MarketSlot>()._id = 16;
                    _Packaget[1].GetComponent<MarketSlot>()._id = 17;
                    _Packaget[2].GetComponent<MarketSlot>()._id = 18;
                    _Packaget[3].GetComponent<MarketSlot>()._id = 19;
                    _Packaget[4].GetComponent<MarketSlot>()._id = 20;
                    _Packaget[5].GetComponent<MarketSlot>()._id = 21;

                    _Packaget[0].SetActive(true);
                    _Packaget[1].SetActive(true);
                    _Packaget[2].SetActive(true);
                    _Packaget[3].SetActive(true);
                    _Packaget[4].SetActive(true);
                    _Packaget[5].SetActive(true);
                }
                break;

            case 2:
                if (_id == 3)
                {
                    _MarketSlot_IMG[0].GetComponent<Image>().sprite = GetComponent<InventorySystem>()._ItemStore[22]._StoreIcon;
                    _MarketSlot_IMG[1].GetComponent<Image>().sprite = GetComponent<InventorySystem>()._ItemStore[23]._StoreIcon;
                    _MarketSlot_IMG[2].GetComponent<Image>().sprite = GetComponent<InventorySystem>()._ItemStore[24]._StoreIcon;

                    _PricesTxt[0].GetComponent<TMPro.TextMeshProUGUI>().text = GetComponent<InventorySystem>()._ItemStore[22]._price + "";
                    _PricesTxt[1].GetComponent<TMPro.TextMeshProUGUI>().text = GetComponent<InventorySystem>()._ItemStore[23]._price + "";
                    _PricesTxt[2].GetComponent<TMPro.TextMeshProUGUI>().text = GetComponent<InventorySystem>()._ItemStore[24]._price + "";

                    _Packaget[0].GetComponent<MarketSlot>()._id = 22;
                    _Packaget[1].GetComponent<MarketSlot>()._id = 23;
                    _Packaget[2].GetComponent<MarketSlot>()._id = 24;

                    _Packaget[0].SetActive(true);
                    _Packaget[1].SetActive(true);
                    _Packaget[2].SetActive(true);
                }

                if (_id == 11)
                {
                    _MarketSlot_IMG[0].GetComponent<Image>().sprite = GetComponent<InventorySystem>()._ItemStore[25]._StoreIcon;
                    _MarketSlot_IMG[1].GetComponent<Image>().sprite = GetComponent<InventorySystem>()._ItemStore[26]._StoreIcon;
                    _MarketSlot_IMG[2].GetComponent<Image>().sprite = GetComponent<InventorySystem>()._ItemStore[27]._StoreIcon;

                    _PricesTxt[0].GetComponent<TMPro.TextMeshProUGUI>().text = GetComponent<InventorySystem>()._ItemStore[25]._price + "";
                    _PricesTxt[1].GetComponent<TMPro.TextMeshProUGUI>().text = GetComponent<InventorySystem>()._ItemStore[26]._price + "";
                    _PricesTxt[2].GetComponent<TMPro.TextMeshProUGUI>().text = GetComponent<InventorySystem>()._ItemStore[27]._price + "";

                    _Packaget[0].GetComponent<MarketSlot>()._id = 25;
                    _Packaget[1].GetComponent<MarketSlot>()._id = 26;
                    _Packaget[2].GetComponent<MarketSlot>()._id = 27;

                    _Packaget[0].SetActive(true);
                    _Packaget[1].SetActive(true);
                    _Packaget[2].SetActive(true);
                }
                break;
        }
    }

    public GameObject _BuyPopUP;
    public Image _ItemIcon;
    public TMPro.TextMeshProUGUI _ItemName;
    public TMPro.TextMeshProUGUI _ItemDes;

    int _ItemId;
    public void _OpenPopUP(int _CurrentItemId)
    {
        _ItemId = _CurrentItemId;
        _BuyPopUP.SetActive(true);
        _ItemIcon.sprite = GetComponent<InventorySystem>()._ItemStore[_CurrentItemId]._StoreIcon;
        _ItemName.text = GetComponent<InventorySystem>()._ItemStore[_CurrentItemId]._ItemName;
        _ItemDes.text = GetComponent<InventorySystem>()._ItemStore[_CurrentItemId]._ItemDes;
    }

    public void _BuyItem()
    {
        _BuyPopUP.SetActive(false);
        Userdata.Instance._User.data.currency.gold -= GetComponent<InventorySystem>()._ItemStore[_ItemId]._price;
        StartCoroutine(Userdata.Instance.gameObject.GetComponent<ApiCaller>()._AddItem(_ItemId,GetComponent<InventorySystem>()._CurrentOnUI_Type));
    }
}
