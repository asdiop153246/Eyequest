using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Starter : MonoBehaviour
{
    public GameObject _LoginCore;

    public void OnEnable()
    { 


        StartCoroutine(_DelayHideSound());
    }

    public IEnumerator _DelayHideSound()
    {
        foreach (AudioSource x in _BGSound)
        {
            x.enabled = false;
        }

        foreach (AudioListener z in _Sound)
        {
            z.enabled = false;
        }

        yield return new WaitForSeconds(0.1f);

        StartCoroutine(_Loader());

        if (PlayerPrefs.GetFloat("Sound", 1) == 1)
        {
            _SoundToggle.isOn = true;
            Userdata.Instance._isBGSoundOn = true;
        }
        else
        {
            _SoundToggle.isOn = false;
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

        if (PlayerPrefs.GetFloat("_isTH", 1) == 1)
        {
            Userdata.Instance._isTh = true;
            _THToggle.isOn = Userdata.Instance._isTh;

        }
        else
        {
            
            Userdata.Instance._isTh = false;
            _THToggle.isOn = Userdata.Instance._isTh;
        }

        if (Userdata.Instance._isTh)
        {
            SwitchTH[] allSwitchTHComponents = FindObjectsOfType<SwitchTH>();

            foreach (SwitchTH switchTH in allSwitchTHComponents)
            {
                switchTH.GetComponent<SwitchTH>()._OnDemendSwitch();
            }
        }
        else
        {
            SwitchTH[] allSwitchTHComponents = FindObjectsOfType<SwitchTH>();

            foreach (SwitchTH switchTH in allSwitchTHComponents)
            {
                switchTH.GetComponent<SwitchTH>()._OnDemendSwitch();
            }
        }

        yield return new WaitForSeconds(0.1f);

        GetComponent<AudioSource>().enabled = Userdata.Instance._isBGSoundOn;
        
    }

    public Toggle _SoundToggle;
    public Toggle _VibrationToggle;

    public AudioSource[] _BGSound;

    public AudioListener[] _Sound;

    public void _GetToogleSound()
    {
        Userdata.Instance._isBGSoundOn = _SoundToggle.isOn;

        if (Userdata.Instance._isBGSoundOn)
        {
            foreach (AudioSource x in _BGSound)
            {
                x.enabled = true;
            }

            foreach (AudioListener z in _Sound)
            {
                z.enabled = true;
            }

            PlayerPrefs.SetFloat("Sound", 1);
        }
        else
        {
            foreach (AudioSource x in _BGSound)
            {
                x.enabled = false;
            }

            foreach (AudioListener z in _Sound)
            {
                z.enabled = false;
            }

            PlayerPrefs.SetFloat("Sound", 0);
        }
    }

    public Toggle _THToggle;

    public void _GetToggleTH()
    {
        Userdata.Instance._isTh = _THToggle.isOn;

        if (Userdata.Instance._isTh)
        {
            PlayerPrefs.SetFloat("_isTH", 1);

            SwitchTH[] allSwitchTHComponents = FindObjectsOfType<SwitchTH>();

            foreach (SwitchTH switchTH in allSwitchTHComponents)
            {
                switchTH.GetComponent<SwitchTH>()._OnDemendSwitch();
            }
        }
        else
        {
            PlayerPrefs.SetFloat("_isTH", 0);

            SwitchTH[] allSwitchTHComponents = FindObjectsOfType<SwitchTH>();

            foreach (SwitchTH switchTH in allSwitchTHComponents)
            {
                switchTH.GetComponent<SwitchTH>()._OnDemendSwitch();
            }
        }

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
