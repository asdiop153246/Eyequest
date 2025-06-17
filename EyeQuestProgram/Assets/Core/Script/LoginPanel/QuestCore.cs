using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCore : MonoBehaviour
{
    public List<QuestData> _CurrentQuestById;

    [System.Serializable]
    public class QuestData
    {
        public int _id;
        public bool _isClaim;
        public bool _isDone;
    }

    public void OnEnable()
    {
        foreach(QuestData x in _CurrentQuestById)
        {
            x._id = Random.Range(0, _QuestList.Count);
        }
    }

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

    public void _DoneQuestById(int _id)
    {
        foreach (QuestData x in _CurrentQuestById)
        {
            if(_id == x._id)
            {
                if (!x._isClaim && !x._isDone)
                {
                    x._isDone = true;
                }
            }
           
        }

        // Update Quest API HERE
    }
}
