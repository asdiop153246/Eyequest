using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCore : MonoBehaviour
{
    public List<Userdata.DailyReward> _CurrentQuestById;

    public List<QuestClass> _QuestList;


    public enum _RewardType
    {
        Gold,Gem,Vision
    }
    [System.Serializable]
    public class QuestClass
    {
        public int _id;
        public string _QuestName_ENG;
        public string _QuestCondition_ENG;
        public string _QuestDescription_ENG;

        public string _QuestName_TH;
        public string _QuestCondition_TH;
        public string _QuestDescription_TH;

        public _RewardType _Type;
        public int _Currency;
    }

    public int _CurrectRewardId;
    public void _DoneQuestById(int _id)
    {
        foreach (Userdata.DailyReward x in _CurrentQuestById)
        {
            if(_id == x.reward_no)
            {
                if (!x.is_claimed && !x.is_done)
                {
                    x.is_done = true;
                    _CurrectRewardId = x.id;
                }
            }
           
        }

        StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._DailyReward_isDone(_CurrectRewardId));
        // Update Quest API HERE
    }

    public void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            _DoneQuestById(5);
        }
    }
}
