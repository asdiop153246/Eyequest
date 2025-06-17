using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame_1_core : MonoBehaviour
{

    public GameObject _HowtoPlay_Panel;
    public List<GameObject> _Monster;

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
    public List<GameObject> _EyeUI_2;
    public void _GameStarter()
    {
        StartCoroutine(_ReadaySetGo());
    }

    public GameObject _ScorePanel;
    public GameObject _VideoPanel;
    public GameObject _TimmerIcon;

    public GameObject _CurrentMonster;

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
        _ScorePanel.SetActive(true);
        _VideoPanel.SetActive(true);
        _TimmerIcon.SetActive(true);
        _MainBanner.SetActive(false);
        _CreateMonster();
        _Timmer = 60;
        _isStart = true;
    }
    public void _CreateMonster()
    {
        foreach (GameObject obj in _Monster)
        {
            obj.SetActive(false);
        }

        // Pick a random object from the list
        if (_Monster.Count > 0)
        {
            int randomIndex = Random.Range(0, _Monster.Count);
            _Monster[randomIndex].SetActive(true);
            _CurrentMonster = _Monster[randomIndex];
        }

        foreach (GameObject obj in _EyeUI)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in _EyeUI_2)
        {
            obj.SetActive(false);
        }

        // Pick a random object from the list
        if (_Core._Core.Count > 0)
        {
            int randomIndex = Random.Range(0, _EyeUI.Count);
            _Core._Core[randomIndex].onAction.Invoke();
            _EyeUI[randomIndex].gameObject.SetActive(true);
            _EyeUI_2[randomIndex].gameObject.SetActive(true);
            _EyeUI[randomIndex].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = _Core._Core[randomIndex]._SkillName;
            _CurrentSkillName = _Core._Core[randomIndex]._SkillName;
            // Add Skill Processing Here
            // Show Camera
        }
    }

    public string _CurrentSkillName;

    public Minigame_SkillController _Core;

    public GameObject _Log;
    public GameObject _HitEffect;
    public GameObject _SlashEffect;
    public void _DoneVision()
    {
        StartCoroutine(_DoneProcessing());

        // Quest Here
        switch (_CurrentSkillName)
        {
            case "Infinity":
                Userdata.Instance.gameObject.GetComponent<QuestCore>()._DoneQuestById(4);
                break;

            case "Vertical":
                Userdata.Instance.gameObject.GetComponent<QuestCore>()._DoneQuestById(1);
                break;
            case "Horizontal":
                Userdata.Instance.gameObject.GetComponent<QuestCore>()._DoneQuestById(2);
                break;
            case "XStrike":
                Userdata.Instance.gameObject.GetComponent<QuestCore>()._DoneQuestById(3);
                break;
            case "Cyclone":
                Userdata.Instance.gameObject.GetComponent<QuestCore>()._DoneQuestById(1);
                break;
            case "Blinkshot":
                Userdata.Instance.gameObject.GetComponent<QuestCore>()._DoneQuestById(0);
                break;
            case "Shield":
                Userdata.Instance.gameObject.GetComponent<QuestCore>()._DoneQuestById(5);
                break;
        }

        foreach (GameObject obj in _EyeUI)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in _EyeUI_2)
        {
            obj.SetActive(false);
        }
    }

    IEnumerator _DoneProcessing()
    {
        _Log.GetComponent<TMPro.TextMeshProUGUI>().text = "Show Hit Effect";
        _HitEffect.SetActive(true);
        _SlashEffect.SetActive(true);
        _CurrentMonster.transform.GetChild(0).gameObject.GetComponent<Animator>().SetTrigger("_hit");

        switch (_CurrentMonster.name)
        {
            case "Amanita":
                Userdata.Instance.gameObject.GetComponent<QuestCore>()._DoneQuestById(6);
                break;
            case "Bumblebee":
                Userdata.Instance.gameObject.GetComponent<QuestCore>()._DoneQuestById(10);
                break;
            case "Bunnyshoot":
                Userdata.Instance.gameObject.GetComponent<QuestCore>()._DoneQuestById(14);
                break;
            case "DragonWitch":
                Userdata.Instance.gameObject.GetComponent<QuestCore>()._DoneQuestById(11);
                break;
            case "FisherBear":
                Userdata.Instance.gameObject.GetComponent<QuestCore>()._DoneQuestById(9);
                break;
            case "InsectBoy":
                Userdata.Instance.gameObject.GetComponent<QuestCore>()._DoneQuestById(8);
                break;
            case "Liana":
                Userdata.Instance.gameObject.GetComponent<QuestCore>()._DoneQuestById(7);
                break;
            case "Psycholofish":
                Userdata.Instance.gameObject.GetComponent<QuestCore>()._DoneQuestById(13);
                break;
            case "Squidbomber":
                Userdata.Instance.gameObject.GetComponent<QuestCore>()._DoneQuestById(12);
                break;
        }
        // Play Animation
        // Play Hit Effect
        yield return new WaitForSeconds(1f);
        _Log.GetComponent<TMPro.TextMeshProUGUI>().text = "Run Dead Animation";
        // Play Animation
        foreach (GameObject obj in _Monster)
        {
            obj.SetActive(false);
        }

        yield return new WaitForSeconds(0.5f);
        _HitEffect.SetActive(false);
        _SlashEffect.SetActive(false);
        _Log.GetComponent<TMPro.TextMeshProUGUI>().text = "Next Monster";

        _Core.GetComponent<Minigame_SkillController>()._HideAllObject();

        _CreateMonster();

        _ScoreCounter++;
  
    }

    public GameObject _EndGameBanner;
    public void FixedUpdate()
    {
        if (_isStart)
        {
            _Timmer -= Time.deltaTime;
            _TimmerUI.text = (int)_Timmer+"";

            _ScoreCounterUI.text = _ScoreCounter + "";

            if(_Timmer <= 0)
            {
                _isStart = false;
                _EnderPanel();
            }
        }
        else
        {
            _TimmerUI.text =  "";

            _ScoreCounterUI.text = "";
        }
    }

    public List<GameObject> _Reward;
    public TMPro.TextMeshProUGUI _Scorer;

    public void _EnderPanel()
    {

        _ScorePanel.SetActive(false);
        _VideoPanel.SetActive(false);
        _TimmerIcon.SetActive(false);

        _Scorer.text = _ScoreCounter + "";
        foreach (GameObject obj in _Reward)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in _EyeUI)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in _EyeUI_2)
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
                Application.LoadLevel(3);
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
