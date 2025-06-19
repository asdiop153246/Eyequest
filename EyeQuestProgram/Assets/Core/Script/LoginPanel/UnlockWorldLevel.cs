using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockWorldLevel : MonoBehaviour
{
    public void _UpdateLevel(int _Score,int _Star)
    {
        Userdata.Instance._WorldData.world[Userdata.Instance._CurrentWorld].level[Userdata.Instance._CurrentStage].stars = _Star;
        Userdata.Instance._WorldData.world[Userdata.Instance._CurrentWorld].level[Userdata.Instance._CurrentStage].score = _Score;
        Userdata.Instance._WorldData.world[Userdata.Instance._CurrentWorld].level[Userdata.Instance._CurrentStage].isUnlock = true;

        switch (Userdata.Instance._CurrentWorld)
        {
            case 0:
                if(Userdata.Instance._CurrentStage == 4)
                {
                    Userdata.Instance._WorldData.world[Userdata.Instance._CurrentWorld].level[5].isUnlock = true;
                    Userdata.Instance._CurrentStage = 5;
                }

                if (Userdata.Instance._CurrentStage == 9)
                {
                    Userdata.Instance._WorldData.world[1].level[0].isUnlock = true;
                    Userdata.Instance._CurrentStage = 9;
                }
                break;

            case 1:
                if (Userdata.Instance._CurrentStage == 5)
                {
                    Userdata.Instance._WorldData.world[Userdata.Instance._CurrentWorld].level[6].isUnlock = true;
                    Userdata.Instance._CurrentStage = 6;
                }

                if (Userdata.Instance._CurrentStage == 13)
                {
                    Userdata.Instance._WorldData.world[Userdata.Instance._CurrentWorld].level[14].isUnlock = true;
                    Userdata.Instance._CurrentStage = 14;
                }

                if (Userdata.Instance._CurrentStage == 19)
                {
                    Userdata.Instance._WorldData.world[2].level[0].isUnlock = true;
                    Userdata.Instance._CurrentStage = 19;
                }
                break;

            case 2:
                if (Userdata.Instance._CurrentStage == 3)
                {
                    Userdata.Instance._WorldData.world[Userdata.Instance._CurrentWorld].level[4].isUnlock = true;
                    Userdata.Instance._CurrentStage = 4;
                }

                if (Userdata.Instance._CurrentStage == 8)
                {
                    Userdata.Instance._WorldData.world[Userdata.Instance._CurrentWorld].level[9].isUnlock = true;
                    Userdata.Instance._CurrentStage = 9;
                }
                break;
        }
    }
}
