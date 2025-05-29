using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public GameObject _GamePlay;

    public void Start()
    {
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
                Debug.Log("Current word :" + _CurrentWorldId);
                break;
            case 1:
                _ImgWord[0].sprite = _Island[2];
                _ImgWord[1].sprite = _Island[1];
                _ImgWord[2].sprite = _Island[0];
                Debug.Log("Current word :" + _CurrentWorldId);
                break;
            case 2:
                _ImgWord[0].sprite = _Island[1];
                _ImgWord[1].sprite = _Island[1];
                _ImgWord[2].sprite = _Island[0];
                Debug.Log("Current word :" + _CurrentWorldId);
                break;
        }

        StartCoroutine(_ChangeMap(_CurrentWorldId));

        _MovableUI.GetComponent<Animation>().clip = _LeftMove;
        _MovableUI.GetComponent<Animation>().Play();
    }

    public TMPro.TextMeshProUGUI _WorldName;

    public GameObject[] _LevelMap;

    IEnumerator _ChangeMap(int _id)
    {
        yield return new WaitForSeconds(0.1f);
        _MapBg.sprite = _WorldMap[_id];
        _MapBg.SetNativeSize();

        switch (_CurrentWorldId)
        {
            case 0:
                _LevelMap[0].SetActive(true);
                _LevelMap[1].SetActive(false);
                _LevelMap[2].SetActive(false);
                _WorldName.text = "Word - 1";
                break;
            case 1:
                _LevelMap[0].SetActive(false);
                _LevelMap[1].SetActive(false);
                _LevelMap[2].SetActive(true);
                _WorldName.text = "Word - 3";
                break;
            case 2:
                _LevelMap[0].SetActive(false);
                _LevelMap[1].SetActive(true);
                _LevelMap[2].SetActive(false);
                _WorldName.text = "Word - 2";
                break;
        }

        


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
                Debug.Log("Current word :" + _CurrentWorldId);
                break;
            case 1:
                _ImgWord[0].sprite = _Island[0];
                _ImgWord[1].sprite = _Island[1];
                _ImgWord[2].sprite = _Island[2];
                Debug.Log("Current word :" + _CurrentWorldId);
                break;
            case 2:
                _ImgWord[0].sprite = _Island[2];
                _ImgWord[1].sprite = _Island[0];
                _ImgWord[2].sprite = _Island[1];
                Debug.Log("Current word :" + _CurrentWorldId);
                break;
        }

        StartCoroutine(_ChangeMap(_CurrentWorldId));

        _MovableUI.GetComponent<Animation>().clip = _RightMove;
        _MovableUI.GetComponent<Animation>().Play();
    }
}
