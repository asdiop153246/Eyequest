using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame_3_core : MonoBehaviour
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
        _EyeUI[1].SetActive(true);
        _TimmerIcon.SetActive(true);
        _Timmer = 60;
        _isStart = true;
        SetNextSpawnTime();
    }

    public GameObject _BinkEffect;
    public Transform _BinkEffectPos;
    public GameObject _EndGameBanner;

    private float timer = 0f;
    private float nextSpawnTime;

    public void FixedUpdate()
    {
        if (_isStart)
        {
            _Timmer -= Time.deltaTime;
            _TimmerUI.text = (int)_Timmer + "";

            timer += Time.deltaTime;

            if (timer >= nextSpawnTime)
            {
                SpawnRandomObject();
                timer = 0f;
                SetNextSpawnTime();
            }


            if (_Timmer <= 0)
            {
                _isStart = false;

                StartCoroutine(_EndGame_ShowSomeThing());
            }
        }

    }

    void SetNextSpawnTime()
    {
        nextSpawnTime = Random.Range(1f, 3f); // Set new random time
    }

    public GameObject _PinPrefab;
    public Transform _PinCreateTranfrom;

    public List<GameObject> _PINStore;

    void SpawnRandomObject()
    {
        GameObject _tempPin = Instantiate(_PinPrefab, _PinCreateTranfrom.position,Quaternion.identity, _PinCreateTranfrom);
        _tempPin.GetComponent<Mover>()._Core = GetComponent<Minigame_3_core>();
        _PINStore.Add(_tempPin);
        Debug.Log("CREATE PIN");
    }

    public Transform _UITarget;
    public float perfectThreshold = 10f;
    public float goodThreshold = 50f;

    public TMPro.TextMeshProUGUI _resultTxt;
    public TMPro.TextMeshProUGUI _ScoreTxt;
    public void JudgeDistance()
    {
        if(_PINStore.Count == 0)
        {
            return;
        }

        Vector2 screenTarget = RectTransformUtility.WorldToScreenPoint(null, _UITarget.position);
        Vector2 screenMarker = RectTransformUtility.WorldToScreenPoint(null, _PINStore[0].transform.position);
        float distance = Vector2.Distance(screenTarget, screenMarker);

        string result;
        if (distance <= perfectThreshold)
        {
            result = "Perfect";
            _ScoreCounter += 3;
        }
        else if (distance <= goodThreshold)
        {
            result = "Good";
            _ScoreCounter += 2;
        }
        else
        {
            result = "Miss";
            _ScoreCounter += 0;
        }

        _resultTxt.text = result;
        _resultTxt.GetComponent<Animation>().Play("ScorePopper_MiniGame_3");
        
        _ScoreTxt.text = _ScoreCounter+"";
        _ScoreTxt.GetComponent<Animation>().Play("ScorePopper_MiniGame_3");

        Debug.Log($"Distance: {distance:F2} -> Result: {result}");

        StartCoroutine(_RemoveAddDestory(_PINStore[0]));
        
    }

    IEnumerator _RemoveAddDestory(GameObject _x)
    {
        _PINStore.Remove(_x);
        yield return new WaitForSeconds(0.2f);
        Destroy(_x.gameObject);
        _isBlinkEnable();

    }


    public GameObject _TimeUp;

    public List<GameObject> _Reward;

    public TMPro.TextMeshProUGUI _Scorer;

    public IEnumerator _EndGame_ShowSomeThing()
    {
        _VideoPanel.SetActive(false);
        _BinkBarPanel.SetActive(false);
        _MainBanner.SetActive(false);
        _EyeUI[0].SetActive(false);
        _EyeUI[1].SetActive(false);
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
                Application.LoadLevel(5);
                break;
            case 1:
                Application.LoadLevel(1);
                break;
        }

    }

    public bool _isBlink;

    public void _isBlinkEnable()
    {
        _isBlink = true;
    }
}
