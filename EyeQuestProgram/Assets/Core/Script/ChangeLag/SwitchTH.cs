using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchTH : MonoBehaviour
{
    public bool _isChangeImage;
   //public bool _isChange
    
    public void OnEnable()
    {
        StartCoroutine(_DelaySwitch());
    }

    public void Awake()
    {
        if (GetComponent<Image>())
            GetComponent<Image>().enabled = false;

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

        if (GetComponent<Image>())
            GetComponent<Image>().enabled = true;

        yield return new WaitForSeconds(0);
    }
}
