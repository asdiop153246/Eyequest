using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class QuestCaller : MonoBehaviour
{
    public List<QuestUIClass> _QuestCore;

    public Sprite _Gold;
    public Sprite _Gem;
    public Sprite _Vision;
    public Sprite _isDone;
    public Sprite _isClaim;
    public Sprite _Go;
    [System.Serializable]
    public class QuestUIClass
    {
        public TMPro.TextMeshProUGUI _QuestName;
        public TMPro.TextMeshProUGUI _QuestDes;
        public TMPro.TextMeshProUGUI _QuestCondition;
        public TMPro.TextMeshProUGUI _QuestReward;
        public Image _RewardImage;

        public Button _ClaimBtm;
    }

    public Button _ClaimAll;

    public void OnEnable()
    {
        _UpdateQuestTxT();
    }

    public void _UpdateQuestTxT()
    {
        _ClaimAll.interactable = false;

        QuestCore questCore = Userdata.Instance.GetComponent<QuestCore>();
        for (int i = 0; i < questCore._CurrentQuestById.Count; i++)
        {
            _QuestCore[i]._QuestName.text = Userdata.Instance.GetComponent<QuestCore>()._QuestList[Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[i]._id]._QuestName_ENG;
            _QuestCore[i]._QuestDes.text = Userdata.Instance.GetComponent<QuestCore>()._QuestList[Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[i]._id]._QuestDescription_ENG;
            _QuestCore[i]._QuestCondition.text = "- "+ Userdata.Instance.GetComponent<QuestCore>()._QuestList[Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[i]._id]._QuestCondition_ENG;
            _QuestCore[i]._QuestReward.text = Userdata.Instance.GetComponent<QuestCore>()._QuestList[Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[i]._id]._Currency + "";

            if (Userdata.Instance.GetComponent<QuestCore>()._QuestList[Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[i]._id]._Type == QuestCore._RewardType.Gold)
            {
                _QuestCore[i]._RewardImage.sprite = _Gold;
            }
            else if (Userdata.Instance.GetComponent<QuestCore>()._QuestList[Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[i]._id]._Type == QuestCore._RewardType.Vision)
            {
                _QuestCore[i]._RewardImage.sprite = _Vision;
            }
            else if (Userdata.Instance.GetComponent<QuestCore>()._QuestList[Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[i]._id]._Type == QuestCore._RewardType.Gem)
            {
                _QuestCore[i]._RewardImage.sprite = _Gem;
            }

            if (!Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[i]._isDone && !Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[i]._isClaim)
            {
                _QuestCore[i]._ClaimBtm.GetComponent<Image>().sprite = _Go;
                _QuestCore[i]._ClaimBtm.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "GO";
                _QuestCore[i]._ClaimBtm.GetComponent<Button>().interactable = true;
                _QuestCore[i]._ClaimBtm.GetComponent<QuestBtm>()._isDone = false;
            }
            else if (Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[i]._isDone && !Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[i]._isClaim)
            {
                _QuestCore[i]._ClaimBtm.GetComponent<Image>().sprite = _isDone;
                _QuestCore[i]._ClaimBtm.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Claim";
                _QuestCore[i]._ClaimBtm.GetComponent<Button>().interactable = true;
                _QuestCore[i]._ClaimBtm.GetComponent<QuestBtm>()._isDone = true;
                _ClaimAll.interactable = true;
            }
            else if (Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[i]._isDone && Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[i]._isClaim)
            {
                _QuestCore[i]._ClaimBtm.GetComponent<Image>().sprite = _isClaim;
                _QuestCore[i]._ClaimBtm.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Done";
                _QuestCore[i]._ClaimBtm.GetComponent<Button>().interactable = false;
            }

            Debug.Log("QUEST : " + i + " / " + Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[i]._isDone + " / " + Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[i]._isClaim);
        }
    }

    public UnityEvent OnGoAction;

    public GameObject _RewardPopUp;
    public GameObject _GoldReward;
    public GameObject _GemReward;
    public GameObject _VisionReward;

    public void _DoneQuest(bool _isDone, int _QuestSlot)
    {

        Debug.Log("_DoneQuest : "+ " / "+ _isDone + " / " + _QuestSlot);
        if (_isDone)
        {
            _RewardPopUp.SetActive(true);
            _GoldReward.SetActive(false);
            _GemReward.SetActive(false);
            _VisionReward.SetActive(false);
            // GET ITEM;
            if (Userdata.Instance.GetComponent<QuestCore>()._QuestList[Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[_QuestSlot]._id]._Type == QuestCore._RewardType.Gold)
            {
                _GoldReward.transform.GetChild(0).transform.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = Userdata.Instance.GetComponent<QuestCore>()._QuestList[Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[_QuestSlot]._id]._Currency + "";
                _GoldReward.SetActive(true);

                Userdata.Instance._User.data.currency.gold += Userdata.Instance.GetComponent<QuestCore>()._QuestList[Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[_QuestSlot]._id]._Currency;
                StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._AddCurreny(0, 0));
            }
            else if (Userdata.Instance.GetComponent<QuestCore>()._QuestList[Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[_QuestSlot]._id]._Type == QuestCore._RewardType.Vision)
            {
                _VisionReward.transform.GetChild(0).transform.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = Userdata.Instance.GetComponent<QuestCore>()._QuestList[Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[_QuestSlot]._id]._Currency + "";
                _VisionReward.SetActive(true);
                Userdata.Instance._User.data.currency.vision_point += Userdata.Instance.GetComponent<QuestCore>()._QuestList[Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[_QuestSlot]._id]._Currency;
                StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._AddCurreny(0, 2));
            }
            else if (Userdata.Instance.GetComponent<QuestCore>()._QuestList[Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[_QuestSlot]._id]._Type == QuestCore._RewardType.Gem)
            {
                _GemReward.transform.GetChild(0).transform.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = Userdata.Instance.GetComponent<QuestCore>()._QuestList[Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[_QuestSlot]._id]._Currency + "";
                _GemReward.SetActive(true);
                Userdata.Instance._User.data.currency.gem += Userdata.Instance.GetComponent<QuestCore>()._QuestList[Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[_QuestSlot]._id]._Currency;
                StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._AddCurreny(0, 1));
            }

            Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[_QuestSlot]._isDone = true;
            Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[_QuestSlot]._isClaim = true;

            _UpdateQuestTxT();

            // Update Quest API Here

        }
        else
        {
            OnGoAction.Invoke();
        }
    }

    public void _CliamAll()
    {
        int _Vision = 0;
        int _Gem = 0;
        int _Gold = 0;

        _RewardPopUp.SetActive(true);
        _GoldReward.SetActive(false);
        _GemReward.SetActive(false);
        _VisionReward.SetActive(false);

        QuestCore questCore = Userdata.Instance.GetComponent<QuestCore>();
        for (int i = 0; i < questCore._CurrentQuestById.Count; i++)
        {
            if (Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[i]._isDone && !Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[i]._isClaim)
            {
                if (Userdata.Instance.GetComponent<QuestCore>()._QuestList[Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[i]._id]._Type == QuestCore._RewardType.Gold)
                {
                    _Gold += Userdata.Instance.GetComponent<QuestCore>()._QuestList[Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[i]._id]._Currency;
                    _GoldReward.SetActive(true);
                }
                else if (Userdata.Instance.GetComponent<QuestCore>()._QuestList[Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[i]._id]._Type == QuestCore._RewardType.Vision)
                {
                    _Vision += Userdata.Instance.GetComponent<QuestCore>()._QuestList[Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[i]._id]._Currency;
                    _VisionReward.SetActive(true);
                }
                else if (Userdata.Instance.GetComponent<QuestCore>()._QuestList[Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[i]._id]._Type == QuestCore._RewardType.Gem)
                {
                    _Gem += Userdata.Instance.GetComponent<QuestCore>()._QuestList[Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[i]._id]._Currency;
                    _GemReward.SetActive(true);
                }

                Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[i]._isDone = true;
                Userdata.Instance.GetComponent<QuestCore>()._CurrentQuestById[i]._isClaim = true;
            }

            
        }

        _GoldReward.transform.GetChild(0).transform.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = _Gold + "";
        _VisionReward.transform.GetChild(0).transform.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = _Vision + "";
        _GemReward.transform.GetChild(0).transform.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = _Gem + "";

        _UpdateQuestTxT();

        // Update Quest API Here
    }
}
