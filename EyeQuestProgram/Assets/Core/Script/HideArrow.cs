using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideArrow : MonoBehaviour
{
    public GameObject _IMG;
    public Scrollbar _ScrollBar;

    public void _UpdateValue()
    {
        if(_ScrollBar.value > 0.9)
        {
            _IMG.SetActive(false);
        }
        else
        {
            _IMG.SetActive(true);
        }
        
    }
}
