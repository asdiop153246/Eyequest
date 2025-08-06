using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kiwiwareable : MonoBehaviour
{
    public List<GameObject> _Hat;
    public List<GameObject> _Body;
    public List<GameObject> _Weapon;

    public int _HatId;
    public int _BodyId;
    public int _WeaponId;

    public void OnEnable()
    {
        _UpdateWable();

        _HatId = Userdata.Instance._User.data.current_ware.current_hat;
        _BodyId = Userdata.Instance._User.data.current_ware.current_body;
        _WeaponId = Userdata.Instance._User.data.current_ware.current_weapon;
    }

    public void _UpdateWable()
    {
        foreach(GameObject x in _Hat)
        {
            x.SetActive(false);
        }

        foreach (GameObject x in _Body)
        {
            x.SetActive(false);
        }

        foreach (GameObject x in _Weapon)
        {
            x.SetActive(false);
        }

        switch (Userdata.Instance._User.data.current_ware.current_hat)
        {
            case 1:
                _Hat[0].SetActive(true);
                break;
            case 4:
                _Hat[1].SetActive(true);
                break;
            case 7:
                _Hat[2].SetActive(true);
                break;
            case 10:
                _Hat[3].SetActive(true);
                break;
            case 13:
                _Hat[4].SetActive(true);
                break;
            case 16:
                _Hat[5].SetActive(true);
                break;
            case 19:
                _Hat[6].SetActive(true);
                break;
            case 22:
                _Hat[7].SetActive(true);
                break;
            case 25:
                _Hat[8].SetActive(true);
                break;
            default:
                _Hat[0].SetActive(true);
                break;
        }

        switch (Userdata.Instance._User.data.current_ware.current_body)
        {
            case 2:
                _Body[0].SetActive(true);
                break;
            case 5:
                _Body[1].SetActive(true);
                break;
            case 8:
                _Body[2].SetActive(true);
                break;
            case 11:
                _Body[3].SetActive(true);
                break;
            case 14:
                _Body[4].SetActive(true);
                break;
            case 17:
                _Body[5].SetActive(true);
                break;
            case 20:
                _Body[6].SetActive(true);
                break;
            case 23:
                _Body[7].SetActive(true);
                break;
            case 26:
                _Body[8].SetActive(true);
                break;
            default:
                _Body[0].SetActive(true);
                break;
        }

        switch (Userdata.Instance._User.data.current_ware.current_weapon)
        {
            case 3:
                _Weapon[0].SetActive(true);
                break;
            case 6:
                _Weapon[1].SetActive(true);
                break;
            case 9:
                _Weapon[2].SetActive(true);
                break;
            case 12:
                _Weapon[3].SetActive(true);
                break;
            case 15:
                _Weapon[4].SetActive(true);
                break;
            case 18:
                _Weapon[5].SetActive(true);
                break;
            case 21:
                _Weapon[6].SetActive(true);
                break;
            case 24:
                _Weapon[7].SetActive(true);
                break;
            case 27:
                _Weapon[8].SetActive(true);
                break;
            default:
                _Weapon[0].SetActive(true);
                break;
        }

        //_Body[Userdata.Instance._User.data.current_ware.current_body].SetActive(true);
       // _Weapon[Userdata.Instance._User.data.current_ware.current_weapon].SetActive(true);

    }
}
