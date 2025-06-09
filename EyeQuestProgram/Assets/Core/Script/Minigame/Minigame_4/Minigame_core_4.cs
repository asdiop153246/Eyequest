using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame_core_4 : MonoBehaviour
{
    public GameObject _HowtoPlay_Panel;

    public TMPro.TextMeshProUGUI _TimmerUI;
    public float _Timmer;
    public TMPro.TextMeshProUGUI _ScoreCounterUI;
    public int _ScoreCounter;


    public GameObject _MainBanner;
    public GameObject _Ready;
    public GameObject _Set;
    public GameObject _Go;

    public bool _isStart;

    public List<GameObject> _EyeUI;

    public void _GameStarter()
    {
        StartCoroutine(_ReadaySetGo());
    }

    public GameObject _BinkBarPanel;
    public GameObject _VideoPanel;
    public GameObject _TimmerIcon;

    IEnumerator _ReadaySetGo()
    {
        _MainBanner.SetActive(true);
        _Ready.SetActive(true);
        _Set.SetActive(false);
        _Go.SetActive(false);
        yield return new WaitForSeconds(1f);
        _Ready.SetActive(false);
        _Set.SetActive(true);
        _Go.SetActive(false);
        yield return new WaitForSeconds(1f);
        _Ready.SetActive(false);
        _Set.SetActive(false);
        _Go.SetActive(true);
        yield return new WaitForSeconds(1f);
        _VideoPanel.SetActive(true);
        _BinkBarPanel.SetActive(true);
        _MainBanner.SetActive(false);
        _EyeUI[0].SetActive(true);
        _TimmerIcon.SetActive(true);
        _Timmer = 60;
        _isStart = true;
       // SetNextSpawnTime();
    }

    public void FixedUpdate()
    {
        if (_isStart)
        {
            _Timmer -= Time.deltaTime;
            _TimmerUI.text = (int)_Timmer + "";

            if (_Timmer <= 0)
            {
                _isStart = false;

                StartCoroutine(_EndGame_ShowSomeThing());
            }
        }

    }

    public GameObject _BinkEffect;
    public Transform _BinkEffectPos;

    public void _DoneVision()
    {
        StartCoroutine(_DoneProcessing());
        GameObject A = Instantiate(_BinkEffect, _BinkEffectPos);
        A.transform.localScale = Vector3.one;
        A.transform.localPosition = Vector3.zero;
        Destroy(A, 2f);
    }

    IEnumerator _DoneProcessing()
    {
        //_Log.GetComponent<TMPro.TextMeshProUGUI>().text = "Show Hit Effect";
        _ScoreCounter++;
        _ScoreCounterUI.text =  _ScoreCounter + "";
        _ScoreCounterUI.gameObject.GetComponent<Animation>().Play("ScorePopper");
        yield return new WaitForSeconds(0);
    }

    public GameObject _TimeUp;

    public List<GameObject> _Reward;

    public TMPro.TextMeshProUGUI _Scorer;

    public GameObject _EndGameBanner;
    public IEnumerator _EndGame_ShowSomeThing()
    {
        _VideoPanel.SetActive(false);
        _BinkBarPanel.SetActive(false);
        _MainBanner.SetActive(false);
        _EyeUI[0].SetActive(false);
        _TimeUp.SetActive(true);
        yield return new WaitForSeconds(1);
        _TimeUp.SetActive(false);
        yield return new WaitForSeconds(2);

        _Scorer.text = _ScoreCounter + "";
        foreach (GameObject obj in _Reward)
        {
            obj.SetActive(false);
        }

        if (_Reward.Count > 0)
        {
            int randomIndex = Random.Range(0, _Reward.Count);
            _Reward[randomIndex].SetActive(true);
            _Reward[randomIndex].transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = Random.Range(0, 999) + "";
        }

        _EndGameBanner.SetActive(true);
    }

    public void _Loader(int _id)
    {
        switch (_id)
        {
            case 0:
                Application.LoadLevel(6);
                break;
            case 1:
                Application.LoadLevel(1);
                break;
        }

    }
}
