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

        float random = Random.Range(0f, 100f);

        if (random <= 70f)
        {
            NormalAttack(target);
        }
        else
        {
            SpecialAttack(target);
        }

        Invoke(nameof(EndTurn), 1.5f);
    }

    void NormalAttack(GameObject target)
    {
        attackPower = Random.Range(40, 80);
        Debug.Log($"{gameObject.name} uses NORMAL attack on {target.name} for {attackPower} damage.");
        ActionText.text = $"{gameObject.name} used Normal Attack!";
        target.GetComponent<Player>()?.TakeDamage(attackPower);
    }

    void SpecialAttack(GameObject target)
    {
        attackPower = Random.Range(80, 120);
        Debug.Log($"{gameObject.name} uses Special attack on {target.name} for {attackPower} damage.");
        ActionText.text = $"{gameObject.name} used Special Attack!";
        target.GetComponent<Player>()?.TakeDamage(attackPower);
    }
}
