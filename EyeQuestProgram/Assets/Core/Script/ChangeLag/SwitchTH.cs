using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchTH : MonoBehaviour
{
    public bool _isChangeImage;
    public void OnEnable()
    {
        StartCoroutine(_DelaySwitch());
    }

    public string _ENG;
    public string _TH;

    public Sprite[] _Img;

    public void _OnDemendSwitch()
    {
        StartCoroutine(_DelaySwitch());
    }

    public IEnumerator _DelaySwitch()
    {
        yield return new WaitForSeconds(0.001f);

        if (Userdata.Instance._isTh)
        {
            if (GetComponent<TMPro.TextMeshProUGUI>())
            {
                GetComponent<TMPro.TextMeshProUGUI>().text = _TH;
            }
            
            if (_isChangeImage)
            {
                GetComponent<Image>().sprite = _Img[0];
            }
        }
        else
        {
            if (GetComponent<TMPro.TextMeshProUGUI>())
            {
                GetComponent<TMPro.TextMeshProUGUI>().text = _ENG;
            }

            if (_isChangeImage)
            {
                GetComponent<Image>().sprite = _Img[1];
            }
        }
    }
}
