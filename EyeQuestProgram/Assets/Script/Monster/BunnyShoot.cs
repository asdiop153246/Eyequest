using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyShoot : EnemyAI
{
protected override void PerformAction()
    {
        GameObject target = gameManager.GetRandomPlayer();
        if (target == null)
        {
            Debug.LogWarning("No target found.");
            EndTurn();
            return;
        }

        Invoke(nameof(EndTurn), 1.5f);
    }

}
