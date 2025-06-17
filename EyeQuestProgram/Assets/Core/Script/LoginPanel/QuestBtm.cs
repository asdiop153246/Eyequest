using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBtm : MonoBehaviour
{
    public bool _isDone;
    public int _QuestSlot;
    public GameObject _Core;
    
    public void _OnClick()
    {
        Debug.Log(_isDone);
        _Core.GetComponent<QuestCaller>()._DoneQuest(_isDone, _QuestSlot);
    }
}
