using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    public Image healthBar; // Assign in Inspector
    public TextMeshProUGUI healthText; // Assign in Inspector
    [Header("Combat Settings")]
    public float health = 1000f;
    public float maxHealth = 1000f;
    public float baseAttackPower = 0f;
    public float baseCriticalChance = 15f;
    public float baseCriticalPower = 1.5f;
    public float evasionChance = 10f;
    [Header("Game Manager")]
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (healthBar != null)
        {
            healthBar.fillAmount = 1f; // Full health at start
        }
        if (healthText != null)
        {
            healthText.text = $"{health}/{maxHealth}"; // Display initial health
            health = maxHealth; // Initialize health
        }
    }

    // Call this method when it's the player's turn
    public void Attack(float attack)
    {
        if (gameManager == null || gameManager.selectedTarget == null)
        {
            Debug.LogWarning("Cannot attack: GameManager or selected target is null.");
            return;
        }

        float actualAttackPower = baseAttackPower + attack; // Add any additional attack power
        bool isCritical = false;

        // Calculate critical hit chance
        if (Random.Range(0f, 100f) < baseCriticalChance)
        {
            actualAttackPower *= baseCriticalPower;
            isCritical = true;
            Debug.Log($"{gameObject.name} landed a CRITICAL HIT!");
        }

        // Deal damage to the selected target
        DealDamage(gameManager.selectedTarget, actualAttackPower, isCritical);
    }
    private void DealDamage(GameObject target, float damageAmount, bool wasCritical)
    {
        if (target == null)
        {
            Debug.LogWarning("Target is null. Cannot deal damage.");
            return;
        }
        MonsterHealth monster = target.GetComponentInChildren<MonsterHealth>();
        if (monster != null)
        {
            monster.TakeDamage(damageAmount);
            Debug.Log($"{gameObject.name} attacks {target.name} for {damageAmount} damage.");
        }
        // Optionally check if it's your turn (gameManager can expose currentTurnPlayer)
        Invoke(nameof(EndAttack), 1.5f);
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"{gameObject.name} takes {damage} damage. Remaining health: {health}");
        if (health <= 0)
        {
            Die();
        }
        UpdateHealthBar();
    }
    void Die()
    {
        Debug.Log($"{gameObject.name} has died.");
        // Handle player death logic here, e.g., disable controls, play animation, etc.
        gameObject.SetActive(false); // Disable the player GameObject
    }
    void EndAttack()
    {
        gameManager.NextTurn();
    }
    void UpdateHealthBar()
    {
        if (health < 0) health = 0; // Ensure health doesn't go below 0
        if (health > maxHealth) health = maxHealth; // Ensure health doesn't exceed maxHealth
        if (healthBar != null)
        {
            healthBar.fillAmount = health / maxHealth; 
            healthText.text = $"{health}/{maxHealth}"; 
        }

            
        
    }
}
