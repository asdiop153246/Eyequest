using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Minigame_SkillController : MonoBehaviour
{

    public List<_SkillSet> _Core;


    [System.Serializable]
    public class _SkillSet
    {
        public string _SkillName;
        public UnityEvent onAction;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
