using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public GameObject _GamePlay;

    public void Start()
    {
        if (Userdata.Instance._isTh)
        {
            _WorldName.text = "ป่าพิศวง";
        }
        else
        {
            _WorldName.text = "The Forest";
        }

        _GamePlay.SetActive(true);
    }
    public void _Play()
    {
        Application.LoadLevel(2);
    }

    public GameObject _MovableUI;
    public AnimationClip _LeftMove;
    public AnimationClip _RightMove;

    public int _CurrentWorldId;

    public Sprite[] _Island;
    public Sprite[] _WorldMap;
    public Image _MapBg;
    public Image[] _ImgWord;

    public GameObject[] _WorldIcon;

    public int _CurrentWorld_Id;

    public void _MoveLeft()
    {
        _CurrentWorldId++;
        if (_CurrentWorldId >= 3)
        {
            _CurrentWorldId = 0;
        }

        switch (_CurrentWorldId)
        {
            case 0:
                _ImgWord[0].sprite = _Island[0];
                _ImgWord[1].sprite = _Island[1];
                _ImgWord[2].sprite = _Island[2];
                

                _CurrentWorld_Id = 0;
                _WorldIcon[0].SetActive(false);
                Debug.Log("Current word :" + _CurrentWorld_Id);
                break;
            case 1:
                _ImgWord[0].sprite = _Island[2];
                _ImgWord[1].sprite = _Island[1];
                _ImgWord[2].sprite = _Island[0];
                _CurrentWorld_Id = 2;
                Debug.Log("Current word :" + _CurrentWorld_Id + Userdata.Instance._WorldData.world[_CurrentWorld_Id].isUnlock);

                if (Userdata.Instance._WorldData.world[_CurrentWorld_Id].isUnlock)
                {
                    _WorldIcon[0].SetActive(false);
                }
                else
                {
                    _WorldIcon[0].SetActive(true);
                }

                

                break;
            case 2:
                _ImgWord[0].sprite = _Island[1];
                _ImgWord[1].sprite = _Island[1];
                _ImgWord[2].sprite = _Island[0];

                _CurrentWorld_Id = 1;
                Debug.Log("Current word :" + _CurrentWorld_Id + Userdata.Instance._WorldData.world[_CurrentWorld_Id].isUnlock);

                if (Userdata.Instance._WorldData.world[_CurrentWorld_Id].isUnlock)
                {
                    _WorldIcon[0].SetActive(false);
                }
                else
                {
                    _WorldIcon[0].SetActive(true);
                }

                

                break;
        }

        StartCoroutine(_ChangeMap(_CurrentWorld_Id));

        _MovableUI.GetComponent<Animation>().clip = _LeftMove;
        _MovableUI.GetComponent<Animation>().Play();
    }

    public TMPro.TextMeshProUGUI _WorldName;

    public GameObject[] _LevelMap;

    public AudioClip[] _BgSound;
    IEnumerator _ChangeMap(int _id)
    {
        _MapBg.gameObject.SetActive(true);

        _MapBg.sprite = _WorldMap[_id];
        
        _MapBg.SetNativeSize();

        switch (_CurrentWorldId)
        {
            case 0:
                _LevelMap[0].SetActive(true);
                _LevelMap[1].SetActive(false);
                _LevelMap[2].SetActive(false);
                if (Userdata.Instance._isTh)
                {
                    _WorldName.text = "ป่าพิศวง";
                }
                else
                {
                    _WorldName.text = "The Forest";
                }
                
                Userdata.Instance._CurrentWorld = 0;
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().PlayOneShot(_BgSound[0]);

                break;
            case 1:
                _LevelMap[0].SetActive(false);
                _LevelMap[1].SetActive(false);
                _LevelMap[2].SetActive(true);

                if (Userdata.Instance._isTh)
                {
                    _WorldName.text = "เมืองล่มสลาย";
                }
                else
                {
                    _WorldName.text = "The Fallen City";
                }

                Userdata.Instance._CurrentWorld = 2;
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().PlayOneShot(_BgSound[1]);
                break;
            case 2:
                _LevelMap[0].SetActive(false);
                _LevelMap[1].SetActive(true);
                _LevelMap[2].SetActive(false);

                if (Userdata.Instance._isTh)
                {
                    _WorldName.text = "ห้องทดลอง";
                }
                else
                {
                    _WorldName.text = "The Lab";
                }

                
                Userdata.Instance._CurrentWorld = 1;
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().PlayOneShot(_BgSound[2]);
                break;
        }


        if (_id == 0)
        {
            GetComponent<WordLoader>()._CurrentWorld = 0;
            Userdata.Instance._CurrentWorld = 0;
            GetComponent<WordLoader>()._CurrentLevelCounter = Userdata.Instance._WorldData.world[GetComponent<WordLoader>()._CurrentWorld].level.Count;
        }
        else if (_id == 2)
        {
            GetComponent<WordLoader>()._CurrentWorld = 2;
            Userdata.Instance._CurrentWorld =2;
            GetComponent<WordLoader>()._CurrentLevelCounter = Userdata.Instance._WorldData.world[GetComponent<WordLoader>()._CurrentWorld].level.Count;
        }
        else if (_id == 1)
        {
            GetComponent<WordLoader>()._CurrentWorld = 1;
            Userdata.Instance._CurrentWorld = 1;
            GetComponent<WordLoader>()._CurrentLevelCounter = Userdata.Instance._WorldData.world[GetComponent<WordLoader>()._CurrentWorld].level.Count;
        }

        yield return new WaitForSeconds(0);
    }

    public void _MoveRight()
    {
        _CurrentWorldId--;
        if (_CurrentWorldId <= -1)
        {
            _CurrentWorldId = 2;
        }

        switch (_CurrentWorldId)
        {
            case 0:
                _ImgWord[0].sprite = _Island[2];
                _ImgWord[1].sprite = _Island[1];
                _ImgWord[2].sprite = _Island[0];
                _CurrentWorld_Id = 0;
                Debug.Log("Current word :" + _CurrentWorld_Id);
                _WorldIcon[2].SetActive(false);
                break;
            case 1:
                _ImgWord[0].sprite = _Island[0];
                _ImgWord[1].sprite = _Island[1];
                _ImgWord[2].sprite = _Island[2];
                _CurrentWorld_Id = 2;
                //Debug.Log("Current word :" + _CurrentWorld_Id);

                Debug.Log("Current word :" + _CurrentWorld_Id + Userdata.Instance._WorldData.world[_CurrentWorld_Id].isUnlock);

                if (Userdata.Instance._WorldData.world[_CurrentWorld_Id].isUnlock)
                {
                    _WorldIcon[2].SetActive(false);
                }
                else
                {
                    _WorldIcon[2].SetActive(true);
                }
                break;
            case 2:
                _ImgWord[0].sprite = _Island[2];
                _ImgWord[1].sprite = _Island[0];
                _ImgWord[2].sprite = _Island[1];

                _CurrentWorld_Id = 1;
                //Debug.Log("Current word :" + _CurrentWorld_Id);

                //Debug.Log("Current word :" + _CurrentWorld_Id + Userdata.Instance._WorldData.world[_CurrentWorld_Id].isUnlock);

                if (Userdata.Instance._WorldData.world[_CurrentWorld_Id].isUnlock)
                {
                    _WorldIcon[2].SetActive(false);
                }
                else
                {
                    _WorldIcon[2].SetActive(true);
                }

                break;
        }

        StartCoroutine(_ChangeMap(_CurrentWorld_Id));

        _MovableUI.GetComponent<Animation>().clip = _RightMove;
        _MovableUI.GetComponent<Animation>().Play();
    }
}
