using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAction : MonoBehaviour
{
    public bool _isCaller;
    public void _CallFirstAction(string _EventName)
    {
        if (!_isCaller)
        {
            StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>().FirstAction(_EventName));
            _isCaller = true;
        }
    }
}
