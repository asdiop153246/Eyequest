using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RewardBtm : MonoBehaviour
{
    public enum _RewardBtm
    {
        RewardBrand,
        RewardProduct,
        RewardHistory
    }

    public _RewardBtm _Type;

    public GameObject _CoreReward;
    public string _BandName;
    public int _id;
    public TMPro.TextMeshProUGUI _Value;
    // Start is called before the first frame update
    public string imageUrl = "https://storage-demo.wellexp.co/uploads/customer/other/2025/00/-58d3020e-ae9a-4be4-b9e7-0ce2b96d48d1b";
    public Image targetImage;

    void Start()
    {
        targetImage.enabled = false;
        StartCoroutine(DownloadImage(imageUrl));
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
            targetImage.sprite = sprite;
            targetImage.SetNativeSize();
            targetImage.enabled = true;
        }
    }

    public void _OpenPopUp()
    {
        if (_Type == _RewardBtm.RewardBrand)
        {
            _CoreReward.GetComponent<WellExpRewardSystem>()._LoadProductInBrand(_id);
        }
        else if (_Type == _RewardBtm.RewardProduct)
        {
            _CoreReward.GetComponent<WellExpRewardSystem>()._OpenPopUp(_id);
        }
        else if(_Type == _RewardBtm.RewardHistory)
        {
           // RewardHistory
        }       
        
    }
}
