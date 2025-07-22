using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
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

            PlayerPrefs.SetFloat("Sound",1);
        }
        else
        {
            foreach(AudioSource x in _BGSound)
            {
                x.enabled = false;
            }

            foreach (AudioListener z in _Sound)
            {
                z.enabled = false;
            }

            PlayerPrefs.SetFloat("Sound",0);
        }
    }

    public void _GetToggleVibration()
    {
        Userdata.Instance._isVibrationOn = _VibrationToggle.isOn;

        if (Userdata.Instance._isVibrationOn)
        {
            PlayerPrefs.SetFloat("_isVibrationOn", 1);
        }
        else
        {
            PlayerPrefs.SetFloat("_isVibrationOn", 0);
        }

    }

    public Toggle _THToggle;
    public Toggle _ENGToggle;

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

        _SoundToggle.isOn = Userdata.Instance._isBGSoundOn;
        _VibrationToggle.isOn = Userdata.Instance._isVibrationOn;
        

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

        }

        if (Userdata.Instance._isTh)
        {
            _THToggle.isOn = true;
            _ENGToggle.isOn = false;

            SwitchTH[] allSwitchTHComponents = FindObjectsOfType<SwitchTH>();

            foreach (SwitchTH switchTH in allSwitchTHComponents)
            {
                switchTH.GetComponent<SwitchTH>()._OnDemendSwitch();
            }
        }
        else
        {
            _THToggle.isOn = false;
            _ENGToggle.isOn = true;

            SwitchTH[] allSwitchTHComponents = FindObjectsOfType<SwitchTH>();

            foreach (SwitchTH switchTH in allSwitchTHComponents)
            {
                switchTH.GetComponent<SwitchTH>()._OnDemendSwitch();
            }
        }



    }

    public void _CallSound(int _Values)
    {
        Userdata.Instance._isCallSound(_Values);
    }

}
