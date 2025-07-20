using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
    public GameObject _LoginCore;

    public void OnEnable()
    { 
        StartCoroutine(_DelayHideSound());
    }

    public IEnumerator _DelayHideSound()
    {
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(_Loader());

        if (PlayerPrefs.GetFloat("Sound", 1) == 1)
        {
            Userdata.Instance._isBGSoundOn = true;
        }
        else
        {
            Userdata.Instance._isBGSoundOn = false;
        }

        if (PlayerPrefs.GetFloat("_isVibrationOn", 1) == 1)
        {
            Userdata.Instance._isVibrationOn = true;
        }
        else
        {
            Userdata.Instance._isVibrationOn = false;
        }

        yield return new WaitForSeconds(0.1f);

        GetComponent<AudioSource>().enabled = Userdata.Instance._isBGSoundOn;
        
    }

    public IEnumerator _Loader()
    {
        yield return new WaitForSeconds(0.5f);
        _LoginCore.SetActive(true);
    }

    public void _CallSound(int _Values)
    {
        Userdata.Instance._isCallSound(_Values);
    }
}
