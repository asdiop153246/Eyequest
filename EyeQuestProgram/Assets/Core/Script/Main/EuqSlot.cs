using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EuqSlot : MonoBehaviour
{
    public int _SlotId;
    public int _CurrentItemId;
    public InventorySystem _Core;

    public void _ShowPopUpMethod()
    {
        _Core._ShowPopUp(_SlotId);
    }
}
