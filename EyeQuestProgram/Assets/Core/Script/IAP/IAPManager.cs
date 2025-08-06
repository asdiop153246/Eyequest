using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.UI;
using Unity.Services.Core;
using System;

public class IAPManager : MonoBehaviour, IStoreListener
{

    public bool _isFakeStore;
    private static IStoreController storeController;
    private static IExtensionProvider storeExtensionProvider;

    public static string PRODUCT_COINS_1 = "explorer_pack";
    public static string PRODUCT_COINS_2 = "trailblazer_pack";
    public static string PRODUCT_COINS_3 = "seeker_pack";
    public static string PRODUCT_COINS_4 = "adventurer_pack";
    public static string PRODUCT_COINS_5 = "conqueror_pack";
    public static string PRODUCT_COINS_6 = "legendary_voyage_pack";

    async void Start()
    {
        if (storeController == null)
        {
            try
            {
                await UnityServices.InitializeAsync();
                InitializePurchasing();
            }
            catch (Exception ex)
            {
                Debug.LogError("UGS Initialization Error: " + ex.Message);
            }
        }
    }

    public void InitializePurchasing()
    {
        var module = StandardPurchasingModule.Instance();

        if (_isFakeStore)
        {
            module.useFakeStoreUIMode = FakeStoreUIMode.StandardUser;
        }

        var builder = ConfigurationBuilder.Instance(module);

        builder.AddProduct(PRODUCT_COINS_1, ProductType.Consumable);
        builder.AddProduct(PRODUCT_COINS_2, ProductType.Consumable);
        builder.AddProduct(PRODUCT_COINS_3, ProductType.Consumable);
        builder.AddProduct(PRODUCT_COINS_4, ProductType.Consumable);
        builder.AddProduct(PRODUCT_COINS_5, ProductType.Consumable);
        builder.AddProduct(PRODUCT_COINS_6, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    public void BuyCoins(int _id)
    {
        string[] productIds = {
            PRODUCT_COINS_1, PRODUCT_COINS_2, PRODUCT_COINS_3,
            PRODUCT_COINS_4, PRODUCT_COINS_5, PRODUCT_COINS_6
        };

        if (_id >= 0 && _id < productIds.Length)
        {
            BuyProductID(productIds[_id]);
        }

    }

    void BuyProductID(string productId)
    {
        if (storeController != null && storeController.products != null)
        {
            Product product = storeController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                storeController.InitiatePurchase(product);
            }
        }
    }

    public GameObject[] _productTitleText;
    public GameObject[] _productDesText;
    public GameObject[] _productpriceText;

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
        storeExtensionProvider = extensions;
        Debug.Log("IAP Initialized");

        UpdatePriceUI();
    }

    void UpdatePriceUI()
    {
        string[] productIds = {
            PRODUCT_COINS_1, PRODUCT_COINS_2, PRODUCT_COINS_3,
            PRODUCT_COINS_4, PRODUCT_COINS_5, PRODUCT_COINS_6
        };

        for (int i = 0; i < productIds.Length; i++)
        {
            Product product = storeController.products.WithID(productIds[i]);
            if (product != null && !product.hasReceipt && _productpriceText.Length > i)
            {
                _productpriceText[i].GetComponent<TMPro.TextMeshProUGUI>().text = product.metadata.localizedPriceString;
            }
        }
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError("IAP Initialization Failed: " + error.ToString());
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.LogError($"IAP Initialization Failed: {error} - {message}");
    }

    public TMPro.TextMeshProUGUI _DiamondTxt;

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        string id = args.purchasedProduct.definition.id;

        if (id == PRODUCT_COINS_1) StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._AddCurreny(25, 1));
        else if (id == PRODUCT_COINS_2) StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._AddCurreny(75, 1));
        else if (id == PRODUCT_COINS_3) StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._AddCurreny(150, 1));
        else if (id == PRODUCT_COINS_4) StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._AddCurreny(320, 1));
        else if (id == PRODUCT_COINS_5) StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._AddCurreny(600, 1));
        else if (id == PRODUCT_COINS_6) StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._AddCurreny(1000, 1));

        Debug.Log("Coins Purchased! : " + id);

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.LogWarning($"Purchase failed: {product.definition.id}, Reason: {failureReason}");
    }

    public int _CurrentSelectGoldPack;
    public TMPro.TextMeshProUGUI _GoldPackDes;
    public GameObject _GoldPackPopUp;
    public GameObject _ConfirmBtm;
    public void _SelectGoldPack(int _id)
    {
        _CurrentSelectGoldPack = _id;

        switch (_id)
        {
            case 0:
                if (Userdata.Instance._isTh)
                {
                    _GoldPackDes.text = "25 เพชร = 250 ทอง";
                }
                else
                {
                    _GoldPackDes.text = "25 GEMS = 250 GOLDs";
                }
               
                if (Userdata.Instance._User.data.currency.gem < 25)
                {
                    _ConfirmBtm.GetComponent<Button>().interactable = false;
                }
                else
                {
                    _ConfirmBtm.GetComponent<Button>().interactable = true;
                }
                break;
            case 1:
                

                if (Userdata.Instance._isTh)
                {
                    _GoldPackDes.text = "75 เพชร = 800 ทอง";
                }
                else
                {
                    _GoldPackDes.text = "75 GEMS = 800 GOLDs";
                }

                if (Userdata.Instance._User.data.currency.gem < 75)
                {
                    _ConfirmBtm.GetComponent<Button>().interactable = false;
                }
                else
                {
                    _ConfirmBtm.GetComponent<Button>().interactable = true;
                }
                break;
            case 2:
                

                if (Userdata.Instance._isTh)
                {
                    _GoldPackDes.text = "150 เพชร = 1,800 ทอง";
                }
                else
                {
                    _GoldPackDes.text = "150 GEMS = 1,800 GOLDs";
                }
                if (Userdata.Instance._User.data.currency.gem < 150)
                {
                    _ConfirmBtm.GetComponent<Button>().interactable = false;
                }
                else
                {
                    _ConfirmBtm.GetComponent<Button>().interactable = true;
                }
                break;
            case 3:
                //_GoldPackDes.text = "320 GEMS = 4000 GOLD";

                if (Userdata.Instance._isTh)
                {
                    _GoldPackDes.text = "320 เพชร = 4,000 ทอง";
                }
                else
                {
                    _GoldPackDes.text = "320 GEMS = 4,000 GOLDs";
                }

                if (Userdata.Instance._User.data.currency.gem < 320)
                {
                    _ConfirmBtm.GetComponent<Button>().interactable = false;
                }
                else
                {
                    _ConfirmBtm.GetComponent<Button>().interactable = true;
                }
                break;
            case 4:

                if (Userdata.Instance._isTh)
                {
                    _GoldPackDes.text = "600 เพชร = 8,500 ทอง";
                }
                else
                {
                    _GoldPackDes.text = "600 GEMS = 8,500 GOLDs";
                }

                if (Userdata.Instance._User.data.currency.gem < 600)
                {
                    _ConfirmBtm.GetComponent<Button>().interactable = false;
                }
                else
                {
                    _ConfirmBtm.GetComponent<Button>().interactable = true;
                }
                break;
            case 5:
                //_GoldPackDes.text = "1000 GEMS = 15000 GOLD";

                if (Userdata.Instance._isTh)
                {
                    _GoldPackDes.text = "1,000 เพชร = 15,000 ทอง";
                }
                else
                {
                    _GoldPackDes.text = "1,000 GEMS = 15,000 GOLDs";
                }

                if (Userdata.Instance._User.data.currency.gem < 1000)
                {
                    _ConfirmBtm.GetComponent<Button>().interactable = false;
                }
                else
                {
                    _ConfirmBtm.GetComponent<Button>().interactable = true;
                }
                break;
        }

    }

    public void _BuyGold()
    {
        switch (_CurrentSelectGoldPack)
        {
            case 0:
                Userdata.Instance._User.data.currency.gem -= 25;
                StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._AddCurreny(250, 0));
                break;
            case 1:
                //_GoldPackDes.text = "79 GEMS = 59 GOLD";
                Userdata.Instance._User.data.currency.gem -= 75;
                StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._AddCurreny(800, 0));
                break;
            case 2:
                //_GoldPackDes.text = "99 GEMS = 90 GOLD";
                Userdata.Instance._User.data.currency.gem -= 150;
                StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._AddCurreny(1800, 0));
                break;
            case 3:
                //_GoldPackDes.text = "199 GEMS = 189 GOLD";
                Userdata.Instance._User.data.currency.gem -= 320;
                StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._AddCurreny(4000, 0));
                break;
            case 4:
                //_GoldPackDes.text = "599 GEMS = 499 GOLD";
                Userdata.Instance._User.data.currency.gem -= 600;
                StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._AddCurreny(8500, 0));
                break;
            case 5:
                //_GoldPackDes.text = "1090 GEMS = 1999 GOLD";
                Userdata.Instance._User.data.currency.gem -= 1000;
                StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._AddCurreny(15000, 0));
                break;
        }

        _GoldPackPopUp.SetActive(false);
    }
}
