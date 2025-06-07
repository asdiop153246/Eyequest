using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private float attackPower;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Call this when it's the enemy's turn
    public void TakeTurn()
    {
        Debug.Log($"{gameObject.name} is thinking...");

        // Simulate thinking delay
        Invoke(nameof(PerformAction), 3.5f);
    }

    void PerformAction()
    {
        // Example: Find a random player to attack
        GameObject target = gameManager.GetRandomPlayer();

        if (target != null)
        {
            attackPower = Random.Range(50, 100); // Random attack power for demonstration
            Debug.Log($"{gameObject.name} attacks {target.name} for {attackPower} damage.");
            Player player = target.GetComponent<Player>();
            if (player != null)
                player.TakeDamage(attackPower);
        }

        // End turn after delay
        Invoke(nameof(EndTurn), 1.5f);
    }

    void EndTurn()
    {
        gameManager.NextTurn();
    }

    void OnMouseDown()
    {
        if (gameManager != null && gameManager.currentTurnIndex == 0) // Only allow on player turn
        {
            gameManager.SelectTarget(this.gameObject);
        }
    }
}
