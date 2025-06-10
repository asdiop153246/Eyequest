using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewStatProfile", menuName = "Enemy/Stat Profile")]
public class Statsprofile : ScriptableObject
{
    [Range(0, 1)] public float StrengthPercent;

    [Range(0, 1)] public float LuckPercent;
    [Range(0, 1)] public float TenacityPercent;

    public bool IsValid()
    {
        return Mathf.Approximately(StrengthPercent + LuckPercent + TenacityPercent, 1f);
    }
}
