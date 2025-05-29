using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.UI;

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

    void Start()
    {
        if (storeController == null)
        {
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        if (_isFakeStore)
        {
            var module = StandardPurchasingModule.Instance();

            module.useFakeStoreUIMode = FakeStoreUIMode.StandardUser; // ให้ popup เลือก ซื้อ / ยกเลิก

            var builder = ConfigurationBuilder.Instance(module);

            builder.AddProduct(PRODUCT_COINS_1, ProductType.Consumable);
            builder.AddProduct(PRODUCT_COINS_2, ProductType.Consumable);
            builder.AddProduct(PRODUCT_COINS_3, ProductType.Consumable);
            builder.AddProduct(PRODUCT_COINS_4, ProductType.Consumable);
            builder.AddProduct(PRODUCT_COINS_5, ProductType.Consumable);
            builder.AddProduct(PRODUCT_COINS_6, ProductType.Consumable);

            UnityPurchasing.Initialize(this, builder);
        }
        else
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            builder.AddProduct(PRODUCT_COINS_1, ProductType.Consumable);
            builder.AddProduct(PRODUCT_COINS_2, ProductType.Consumable);
            builder.AddProduct(PRODUCT_COINS_3, ProductType.Consumable);
            builder.AddProduct(PRODUCT_COINS_4, ProductType.Consumable);
            builder.AddProduct(PRODUCT_COINS_5, ProductType.Consumable);
            builder.AddProduct(PRODUCT_COINS_6, ProductType.Consumable);

            UnityPurchasing.Initialize(this, builder);
        }
        
    }

    public void BuyCoins(int _id)
    {
        switch (_id)
        {
            case 0:
                BuyProductID(PRODUCT_COINS_1);
                break;
            case 1:
                BuyProductID(PRODUCT_COINS_2);
                break;
            case 2:
                BuyProductID(PRODUCT_COINS_3);
                break;
            case 3:
                BuyProductID(PRODUCT_COINS_4);
                break;
            case 4:
                BuyProductID(PRODUCT_COINS_5);
                break;
            case 5:
                BuyProductID(PRODUCT_COINS_6);
                break;
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

        Product product = storeController.products.WithID(PRODUCT_COINS_1);
        if (product != null && product.hasReceipt == false)
        {
            //_productTitleText[0].GetComponent<TMPro.TextMeshProUGUI>().text = product.metadata.localizedTitle;
            //_productDesText[0].GetComponent<TMPro.TextMeshProUGUI>().text = product.metadata.localizedDescription;
            _productpriceText[0].GetComponent<TMPro.TextMeshProUGUI>().text = product.metadata.localizedPriceString;
        }

        Product product_1 = storeController.products.WithID(PRODUCT_COINS_2);
        if (product_1 != null && product_1.hasReceipt == false)
        {
            //_productTitleText[1].GetComponent<TMPro.TextMeshProUGUI>().text = product_1.metadata.localizedTitle;
            //_productDesText[1].GetComponent<TMPro.TextMeshProUGUI>().text = product_1.metadata.localizedDescription;
            _productpriceText[1].GetComponent<TMPro.TextMeshProUGUI>().text = product_1.metadata.localizedPriceString;
        }

        Product product_2 = storeController.products.WithID(PRODUCT_COINS_3);
        if (product_2 != null && product_2.hasReceipt == false)
        {
            //_productTitleText[2].GetComponent<TMPro.TextMeshProUGUI>().text = product_2.metadata.localizedTitle;
           // _productDesText[2].GetComponent<TMPro.TextMeshProUGUI>().text = product_2.metadata.localizedDescription;
            _productpriceText[2].GetComponent<TMPro.TextMeshProUGUI>().text = product_2.metadata.localizedPriceString;
        }

        Product product_3 = storeController.products.WithID(PRODUCT_COINS_4);
        if (product_3 != null && product_3.hasReceipt == false)
        {
            //_productTitleText[3].GetComponent<TMPro.TextMeshProUGUI>().text = product_3.metadata.localizedTitle;
            //_productDesText[3].GetComponent<TMPro.TextMeshProUGUI>().text = product_3.metadata.localizedDescription;
            _productpriceText[3].GetComponent<TMPro.TextMeshProUGUI>().text = product_3.metadata.localizedPriceString;
        }

        Product product_4 = storeController.products.WithID(PRODUCT_COINS_5);
        if (product_4 != null && product_4.hasReceipt == false)
        {
           // _productTitleText[4].GetComponent<TMPro.TextMeshProUGUI>().text = product_4.metadata.localizedTitle;
           // _productDesText[4].GetComponent<TMPro.TextMeshProUGUI>().text = product_4.metadata.localizedDescription;
            _productpriceText[4].GetComponent<TMPro.TextMeshProUGUI>().text = product_4.metadata.localizedPriceString;
        }

        Product product_5 = storeController.products.WithID(PRODUCT_COINS_6);
        if (product_5 != null && product_5.hasReceipt == false)
        {
            //_productTitleText[5].GetComponent<TMPro.TextMeshProUGUI>().text = product_5.metadata.localizedTitle;
            //_productDesText[5].GetComponent<TMPro.TextMeshProUGUI>().text = product_5.metadata.localizedDescription;
            _productpriceText[5].GetComponent<TMPro.TextMeshProUGUI>().text = product_5.metadata.localizedPriceString;
        }
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError("IAP Initialization Failed: " + error);
    }

    public TMPro.TextMeshProUGUI _DiamondTxt;

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (args.purchasedProduct.definition.id == PRODUCT_COINS_1)
        {
            Debug.Log("Coins Purchased! : "+ PRODUCT_COINS_1);

            StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._AddCurreny(20, 1));
            //Userdata.Instance._User.data.currency.gem += 100;
            // เพิ่มเหรียญให้ผู้เล่น
        }

        if (args.purchasedProduct.definition.id == PRODUCT_COINS_2)
        {
            Debug.Log("Coins Purchased! : " + PRODUCT_COINS_2);
            StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._AddCurreny(59, 1));
            // เพิ่มเหรียญให้ผู้เล่น
        }

        if (args.purchasedProduct.definition.id == PRODUCT_COINS_3)
        {
            Debug.Log("Coins Purchased! : " + PRODUCT_COINS_3);
            StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._AddCurreny(90, 1));
            // เพิ่มเหรียญให้ผู้เล่น
        }

        if (args.purchasedProduct.definition.id == PRODUCT_COINS_4)
        {
            Debug.Log("Coins Purchased! : " + PRODUCT_COINS_4);
            StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._AddCurreny(189, 1));
            // เพิ่มเหรียญให้ผู้เล่น
        }

        if (args.purchasedProduct.definition.id == PRODUCT_COINS_5)
        {
            Debug.Log("Coins Purchased! : " + PRODUCT_COINS_5);
            StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._AddCurreny(499, 1));
            // เพิ่มเหรียญให้ผู้เล่น
        }

        if (args.purchasedProduct.definition.id == PRODUCT_COINS_6)
        {
            Debug.Log("Coins Purchased! : " + PRODUCT_COINS_6);
            StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._AddCurreny(1999, 1));
            // เพิ่มเหรียญให้ผู้เล่น
        }

        //_DiamondTxt.text = Userdata.Instance._User.data.currency.gem + "";

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.LogWarning($"Purchase failed: {product.definition.id}, Reason: {failureReason}");
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new System.NotImplementedException();
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
                _GoldPackDes.text = "35 GEMS = 20 GOLD";
                if (Userdata.Instance._User.data.currency.gem < 35)
                {
                    _ConfirmBtm.GetComponent<Button>().interactable = false;
                }
                else
                {
                    _ConfirmBtm.GetComponent<Button>().interactable = true;
                }
                break;
            case 1:
                _GoldPackDes.text = "79 GEMS = 59 GOLD";
                if (Userdata.Instance._User.data.currency.gem < 79)
                {
                    _ConfirmBtm.GetComponent<Button>().interactable = false;
                }
                else
                {
                    _ConfirmBtm.GetComponent<Button>().interactable = true;
                }
                break;
            case 2:
                _GoldPackDes.text = "99 GEMS = 90 GOLD";
                if (Userdata.Instance._User.data.currency.gem < 99)
                {
                    _ConfirmBtm.GetComponent<Button>().interactable = false;
                }
                else
                {
                    _ConfirmBtm.GetComponent<Button>().interactable = true;
                }
                break;
            case 3:
                _GoldPackDes.text = "199 GEMS = 189 GOLD";
                if (Userdata.Instance._User.data.currency.gem < 199)
                {
                    _ConfirmBtm.GetComponent<Button>().interactable = false;
                }
                else
                {
                    _ConfirmBtm.GetComponent<Button>().interactable = true;
                }
                break;
            case 4:
                _GoldPackDes.text = "599 GEMS = 499 GOLD";
                if (Userdata.Instance._User.data.currency.gem < 599)
                {
                    _ConfirmBtm.GetComponent<Button>().interactable = false;
                }
                else
                {
                    _ConfirmBtm.GetComponent<Button>().interactable = true;
                }
                break;
            case 5:
                _GoldPackDes.text = "1090 GEMS = 1999 GOLD";
                if (Userdata.Instance._User.data.currency.gem < 1090)
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
                Userdata.Instance._User.data.currency.gem -= 35;
                StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._AddCurreny(20, 0));
                break;
            case 1:
                _GoldPackDes.text = "79 GEMS = 59 GOLD";
                Userdata.Instance._User.data.currency.gem -= 79;
                StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._AddCurreny(59, 0));
                break;
            case 2:
                _GoldPackDes.text = "99 GEMS = 90 GOLD";
                Userdata.Instance._User.data.currency.gem -= 99;
                StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._AddCurreny(90, 0));
                break;
            case 3:
                _GoldPackDes.text = "199 GEMS = 189 GOLD";
                Userdata.Instance._User.data.currency.gem -= 199;
                StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._AddCurreny(189, 0));
                break;
            case 4:
                _GoldPackDes.text = "599 GEMS = 499 GOLD";
                Userdata.Instance._User.data.currency.gem -= 599;
                StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._AddCurreny(499, 0));
                break;
            case 5:
                _GoldPackDes.text = "1090 GEMS = 1999 GOLD";
                Userdata.Instance._User.data.currency.gem -= 1090;
                StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._AddCurreny(1999, 0));
                break;
        }

        _GoldPackPopUp.SetActive(false);
    }
}
