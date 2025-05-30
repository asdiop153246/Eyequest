using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftTurnScript : MonoBehaviour
{
    public Transform topUIParent; // Assign TopUI here in Inspector

    public void ShiftTurnUI()
    {
        if (topUIParent.childCount == 0) return;

        Transform currentTurn = topUIParent.GetChild(0);
        currentTurn.SetSiblingIndex(topUIParent.childCount - 1);
    }
}
