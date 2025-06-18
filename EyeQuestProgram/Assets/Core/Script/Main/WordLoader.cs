using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordLoader : MonoBehaviour
{
    public Sprite[] _PinImage;

    public List<_LevelData> _World_1;
    public List<_LevelData> _World_2;
    public List<_LevelData> _World_3;

    [System.Serializable]
    public class _LevelData
    {
        public Image _LevelPin;
        public List<GameObject> _Star;
    }

    public void OnEnable()
    {
        Userdata.Instance.gameObject.GetComponent<ApiCaller>().OnCall_BuyBooster_OK += () =>
        {
            _BuyBoosterOK();
        };
    }

    public void OnDisable()
    {
        Userdata.Instance.gameObject.GetComponent<ApiCaller>().OnCall_BuyBooster_OK -= Userdata.Instance.gameObject.GetComponent<ApiCaller>().OnCall_BuyBooster_OK;
    }


    public int _CurrentWorld;

    public void _ForceUpdate()
    {
        UpdateLevel(0);
    }

    public void UpdateLevel(int _id)
    {
        // CURRENT LEVEL UNLOCK
        for (int i = 0; i < Userdata.Instance._WorldData.world[_id].level.Count; i++)
        {
            if (Userdata.Instance._WorldData.world[_id].level[i].isUnlock)
            {
                if (_id == 0)
                {
                    _CurrentWorld = 0;
                    Userdata.Instance._CurrentWorld = _CurrentWorld;
                    

                    _World_1[i]._LevelPin.GetComponent<Button>().interactable = true;

                    _World_1[i]._LevelPin.sprite = _PinImage[0];

                    if (Userdata.Instance._WorldData.world[_id].level[i].stars == 3)
                    {
                        _World_1[i]._Star[0].transform.GetChild(0).gameObject.SetActive(true);
                        _World_1[i]._Star[1].transform.GetChild(0).gameObject.SetActive(true);
                        _World_1[i]._Star[2].transform.GetChild(0).gameObject.SetActive(true);
                    }
                    else if (Userdata.Instance._WorldData.world[_id].level[i].stars == 2)
                    {
                        _World_1[i]._Star[0].transform.GetChild(0).gameObject.SetActive(true);
                        _World_1[i]._Star[1].transform.GetChild(0).gameObject.SetActive(true);
                        _World_1[i]._Star[2].transform.GetChild(0).gameObject.SetActive(false);
                    }
                    else if (Userdata.Instance._WorldData.world[_id].level[i].stars == 1)
                    {
                        _World_1[i]._Star[0].transform.GetChild(0).gameObject.SetActive(true);
                        _World_1[i]._Star[1].transform.GetChild(0).gameObject.SetActive(false);
                        _World_1[i]._Star[2].transform.GetChild(0).gameObject.SetActive(false);
                    }
                    else if (Userdata.Instance._WorldData.world[_id].level[i].stars == 0)
                    {
                        _World_1[i]._Star[0].transform.GetChild(0).gameObject.SetActive(false);
                        _World_1[i]._Star[1].transform.GetChild(0).gameObject.SetActive(false);
                        _World_1[i]._Star[2].transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
                else if(_id == 1)
                {
                    _CurrentWorld = 1;
                    Userdata.Instance._CurrentWorld = _CurrentWorld;

                    _World_2[i]._LevelPin.GetComponent<Button>().interactable = true;

                    _World_2[i]._LevelPin.sprite = _PinImage[0];

                    if (Userdata.Instance._WorldData.world[_id].level[i].stars == 3)
                    {
                        _World_2[i]._Star[0].transform.GetChild(0).gameObject.SetActive(true);
                        _World_2[i]._Star[1].transform.GetChild(0).gameObject.SetActive(true);
                        _World_2[i]._Star[2].transform.GetChild(0).gameObject.SetActive(true);
                    }
                    else if (Userdata.Instance._WorldData.world[_id].level[i].stars == 2)
                    {
                        _World_2[i]._Star[0].transform.GetChild(0).gameObject.SetActive(true);
                        _World_2[i]._Star[1].transform.GetChild(0).gameObject.SetActive(true);
                        _World_2[i]._Star[2].transform.GetChild(0).gameObject.SetActive(false);
                    }
                    else if (Userdata.Instance._WorldData.world[_id].level[i].stars == 1)
                    {
                        _World_2[i]._Star[0].transform.GetChild(0).gameObject.SetActive(true);
                        _World_2[i]._Star[1].transform.GetChild(0).gameObject.SetActive(false);
                        _World_2[i]._Star[2].transform.GetChild(0).gameObject.SetActive(false);
                    }
                    else if (Userdata.Instance._WorldData.world[_id].level[i].stars == 0)
                    {
                        _World_2[i]._Star[0].transform.GetChild(0).gameObject.SetActive(false);
                        _World_2[i]._Star[1].transform.GetChild(0).gameObject.SetActive(false);
                        _World_2[i]._Star[2].transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
                else if (_id == 2)
                {
                    _CurrentWorld = 2;
                    Userdata.Instance._CurrentWorld = _CurrentWorld;

                    _World_3[i]._LevelPin.GetComponent<Button>().interactable = true;

                    _World_3[i]._LevelPin.sprite = _PinImage[0];

                    if (Userdata.Instance._WorldData.world[_id].level[i].stars == 3)
                    {
                        _World_3[i]._Star[0].transform.GetChild(0).gameObject.SetActive(true);
                        _World_3[i]._Star[1].transform.GetChild(0).gameObject.SetActive(true);
                        _World_3[i]._Star[2].transform.GetChild(0).gameObject.SetActive(true);
                    }
                    else if (Userdata.Instance._WorldData.world[_id].level[i].stars == 2)
                    {
                        _World_3[i]._Star[0].transform.GetChild(0).gameObject.SetActive(true);
                        _World_3[i]._Star[1].transform.GetChild(0).gameObject.SetActive(true);
                        _World_3[i]._Star[2].transform.GetChild(0).gameObject.SetActive(false);
                    }
                    else if (Userdata.Instance._WorldData.world[_id].level[i].stars == 1)
                    {
                        _World_3[i]._Star[0].transform.GetChild(0).gameObject.SetActive(true);
                        _World_3[i]._Star[1].transform.GetChild(0).gameObject.SetActive(false);
                        _World_3[i]._Star[2].transform.GetChild(0).gameObject.SetActive(false);
                    }
                    else if (Userdata.Instance._WorldData.world[_id].level[i].stars == 0)
                    {
                        _World_3[i]._Star[0].transform.GetChild(0).gameObject.SetActive(false);
                        _World_3[i]._Star[1].transform.GetChild(0).gameObject.SetActive(false);
                        _World_3[i]._Star[2].transform.GetChild(0).gameObject.SetActive(false);
                    }
                }

            }
            else
            {
                if (_id == 0)
                {
                    _World_1[i]._LevelPin.GetComponent<Button>().interactable = false;

                    _World_1[i]._LevelPin.sprite = _PinImage[1];

                    _World_1[i]._Star[0].gameObject.SetActive(false);
                    _World_1[i]._Star[1].gameObject.SetActive(false);
                    _World_1[i]._Star[2].gameObject.SetActive(false);
                }
                else if (_id == 1)
                {
                    _World_2[i]._LevelPin.GetComponent<Button>().interactable = false;

                    _World_2[i]._LevelPin.sprite = _PinImage[1];

                    _World_2[i]._Star[0].gameObject.SetActive(false);
                    _World_2[i]._Star[1].gameObject.SetActive(false);
                    _World_2[i]._Star[2].gameObject.SetActive(false);
                }
                else if (_id == 2)
                {
                    _World_3[i]._LevelPin.GetComponent<Button>().interactable = false;

                    _World_3[i]._LevelPin.sprite = _PinImage[1];

                    _World_3[i]._Star[0].gameObject.SetActive(false);
                    _World_3[i]._Star[1].gameObject.SetActive(false);
                    _World_3[i]._Star[2].gameObject.SetActive(false);
                }
            }
        }

        // Unlock NEXT LEVEL

        Debug.Log("Check");
    }


public TMPro.TextMeshProUGUI _LevelName;
    public TMPro.TextMeshProUGUI _Score;
    public List<TMPro.TextMeshProUGUI> _LeaderboardName;
    public List<TMPro.TextMeshProUGUI> _SocreSlot;

    public List<GameObject> _BoosterSlot;
    public List<GameObject> _Booster_Toggle;
    public List<GameObject> _Star;

    public GameObject _LevelSelection;
    public int _CurrentLevel;
    public void _OpenLevelPopUp(int _id)
    {
        
        _CurrentLevel = _id;
        Userdata.Instance._CurrentStage = _CurrentLevel;
        _LevelSelection.SetActive(true);


        _LevelName.text = "Level : " + (_id+1);

        if (Userdata.Instance._User.data.booster.booster1 != 0)
        {
            _Booster_Toggle[0].SetActive(false);
            _BoosterSlot[0].SetActive(true);
            _BoosterSlot[0].transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = Userdata.Instance._User.data.booster.booster1.ToString();
        }
        else
        {
            _Booster_Toggle[0].SetActive(true);
            _BoosterSlot[0].SetActive(false);
        }

        if (Userdata.Instance._User.data.booster.booster2 != 0)
        {
            _Booster_Toggle[1].SetActive(false);
            _BoosterSlot[1].SetActive(true);
            _BoosterSlot[1].transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = Userdata.Instance._User.data.booster.booster2.ToString();
        }
        else
        {
            _Booster_Toggle[1].SetActive(true);
            _BoosterSlot[1].SetActive(false);
        }

        if (Userdata.Instance._User.data.booster.booster3 != 0)
        {
            _Booster_Toggle[2].SetActive(false);
            _BoosterSlot[2].SetActive(true);
            _BoosterSlot[2].transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = Userdata.Instance._User.data.booster.booster3.ToString();
        }
        else
        {
            _Booster_Toggle[2].SetActive(true);
            _BoosterSlot[2].SetActive(false);
        }

        if (Userdata.Instance._User.data.booster.booster4 != 0)
        {
            _Booster_Toggle[3].SetActive(false);
            _BoosterSlot[3].SetActive(true);
            _BoosterSlot[3].transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = Userdata.Instance._User.data.booster.booster4.ToString();
        }
        else
        {
            _Booster_Toggle[3].SetActive(true);
            _BoosterSlot[3].SetActive(false);
        }


        _Score.text = Userdata.Instance._WorldData.world[_CurrentWorld].level[_id].score+"";

        if(Userdata.Instance._WorldData.world[_CurrentWorld].level[_id].Leaderboard.Count== 0){
            _LeaderboardName[0].text = "-";
            _SocreSlot[0].text = "-";

            _LeaderboardName[1].text = "-";
            _SocreSlot[1].text = "-";

            _LeaderboardName[2].text = "-";
            _SocreSlot[2].text = "-";
        }
        else
        {
            for (int i = 0; i < Userdata.Instance._WorldData.world[_CurrentWorld].level[_id].Leaderboard.Count; i++)
            {
                if (Userdata.Instance._WorldData.world[_CurrentWorld].level[_id].Leaderboard[i].playerScore != 0)
                {
                    _LeaderboardName[i].text = Userdata.Instance._WorldData.world[_CurrentWorld].level[_id].Leaderboard[i].playerName.ToString();
                    _SocreSlot[i].text = Userdata.Instance._WorldData.world[_CurrentWorld].level[_id].Leaderboard[i].playerScore.ToString();
                }
                else
                {
                    _LeaderboardName[i].text = "-";
                    _SocreSlot[i].text = "-";
                }
            }
        }
        

        if (Userdata.Instance._WorldData.world[_CurrentWorld].level[_id].stars == 3)
        {
            _Star[0].gameObject.SetActive(true);
            _Star[1].gameObject.SetActive(true);
            _Star[2].gameObject.SetActive(true);
        }
        else if (Userdata.Instance._WorldData.world[_CurrentWorld].level[_id].stars == 2)
        {
            _Star[0].gameObject.SetActive(false);
            _Star[1].gameObject.SetActive(true);
            _Star[2].gameObject.SetActive(true);
        }
        else if (Userdata.Instance._WorldData.world[_CurrentWorld].level[_id].stars == 1)
        {
            _Star[0].gameObject.SetActive(false);
            _Star[1].gameObject.SetActive(false);
            _Star[2].gameObject.SetActive(true);
        }
        else if (Userdata.Instance._WorldData.world[_CurrentWorld].level[_id].stars == 0)
        {
            _Star[0].gameObject.SetActive(false);
            _Star[1].gameObject.SetActive(false);
            _Star[2].gameObject.SetActive(false);
        }

        Debug.Log("Check VV");
    }


    public GameObject _BoosterPanel;
    public Button _BuyBoosterBtm;
    public int _BoosterCurrent;
    public void _BuyBooster()
    {
        Userdata.Instance._User.data.currency.gold -= 100;
        StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._AddCurreny(0, 0));
        switch (_BoosterCurrent)
        {
            case 0:
                Userdata.Instance._User.data.booster.booster1 += 1;
                break;
            case 1:
                Userdata.Instance._User.data.booster.booster2 += 1;
                break;
            case 2:
                Userdata.Instance._User.data.booster.booster3 += 1;
                break;
            case 3:
                Userdata.Instance._User.data.booster.booster4 += 1;
                break;
        }

        StartCoroutine(Userdata.Instance.GetComponent<ApiCaller>()._BuyBooster(
            Userdata.Instance._User.data.booster.booster1,
            Userdata.Instance._User.data.booster.booster2,
            Userdata.Instance._User.data.booster.booster3,
            Userdata.Instance._User.data.booster.booster4));
    }

    public void _OpenBuyBooster(int _Id)
    {
        _BoosterCurrent = _Id;

        if (Userdata.Instance._User.data.currency.gold < 100)
        {
            _BuyBoosterBtm.interactable = false;
        }
        else
        {
            _BuyBoosterBtm.interactable = true;
        }
    }

    public void _BuyBoosterOK()
    {
        if (Userdata.Instance._User.data.booster.booster1 != 0)
        {
            _Booster_Toggle[0].SetActive(false);
            _BoosterSlot[0].SetActive(true);
            _BoosterSlot[0].transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = Userdata.Instance._User.data.booster.booster1.ToString();
        }
        else
        {
            _Booster_Toggle[0].SetActive(true);
            _BoosterSlot[0].SetActive(false);
        }

        if (Userdata.Instance._User.data.booster.booster2 != 0)
        {
            _Booster_Toggle[1].SetActive(false);
            _BoosterSlot[1].SetActive(true);
            _BoosterSlot[1].transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = Userdata.Instance._User.data.booster.booster2.ToString();
        }
        else
        {
            _Booster_Toggle[1].SetActive(true);
            _BoosterSlot[1].SetActive(false);
        }

        if (Userdata.Instance._User.data.booster.booster3 != 0)
        {
            _Booster_Toggle[2].SetActive(false);
            _BoosterSlot[2].SetActive(true);
            _BoosterSlot[2].transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = Userdata.Instance._User.data.booster.booster3.ToString();
        }
        else
        {
            _Booster_Toggle[2].SetActive(true);
            _BoosterSlot[2].SetActive(false);
        }

        if (Userdata.Instance._User.data.booster.booster4 != 0)
        {
            _Booster_Toggle[3].SetActive(false);
            _BoosterSlot[3].SetActive(true);
            _BoosterSlot[3].transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = Userdata.Instance._User.data.booster.booster4.ToString();
        }
        else
        {
            _Booster_Toggle[3].SetActive(true);
            _BoosterSlot[3].SetActive(false);
        }

        _BoosterPanel.SetActive(false);
    }

    public void _Loader(int _id)
    {
        Application.LoadLevel(_id);

    }
}
