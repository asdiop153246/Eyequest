using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockWorldLevel : MonoBehaviour
{
    public void _UpdateLevel(int _Score,int _Star)
    {
        

        if(_Score > Userdata.Instance._WorldData.world[Userdata.Instance._CurrentWorld].level[Userdata.Instance._CurrentStage].stars)
        {
            StartCoroutine(Userdata.Instance.gameObject.GetComponent<ApiCaller>()._UpdateLeveldata(
            Userdata.Instance._WorldData.world[Userdata.Instance._CurrentWorld].level[Userdata.Instance._CurrentStage].score,
            Userdata.Instance._WorldData.world[Userdata.Instance._CurrentWorld].level[Userdata.Instance._CurrentStage].stars));

            Userdata.Instance._WorldData.world[Userdata.Instance._CurrentWorld].level[Userdata.Instance._CurrentStage].stars = _Star;
            Userdata.Instance._WorldData.world[Userdata.Instance._CurrentWorld].level[Userdata.Instance._CurrentStage].score = _Score;
            Userdata.Instance._WorldData.world[Userdata.Instance._CurrentWorld].level[Userdata.Instance._CurrentStage].isUnlock = true;
        }

    }
}
