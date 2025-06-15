using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Minigame_SkillController : MonoBehaviour
{

    public List<_SkillSet> _Core;

    public List<GameObject> _HideAll;

    //public GameObject _Camera;

    [System.Serializable]
    public class _SkillSet
    {
        public string _SkillName;
        public UnityEvent onAction;
    }
    
    public void _HideAllObject()
    {
        foreach(GameObject x in _HideAll)
        {
            x.SetActive(false);
        }
    }
}
