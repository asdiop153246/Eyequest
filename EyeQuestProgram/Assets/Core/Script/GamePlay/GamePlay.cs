using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void _BackToMenu()
    {
        Application.LoadLevel(1);
    }

    public void _Restart()
    {
        Application.LoadLevel(2);
    }
}
