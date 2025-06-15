using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
    public GameObject _LoginCore;

    public void OnEnable()
    {
        StartCoroutine(_Loader());
    }
    public IEnumerator _Loader()
    {
        yield return new WaitForSeconds(0.5f);
        _LoginCore.SetActive(true);
    }
}
