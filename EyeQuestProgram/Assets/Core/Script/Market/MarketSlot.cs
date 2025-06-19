using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketSlot : MonoBehaviour
{
    public GameObject _Core;
    public int _id;

    public void _OpenSlot()
    {
        _Core.GetComponent<MarketManager>()._OpenPopUP(_id);
    }
}
