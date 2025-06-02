using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Skill
{
    public string name;
    public float additionalAttackPower;
    public float criticalMultiplier;
    public float chanceToCrit;

    public Skill(string name, float additionalAttackPower, float criticalMultiplier, float chanceToCrit)
    {
        this.name = name;
        this.additionalAttackPower = additionalAttackPower;
        this.criticalMultiplier = criticalMultiplier;
        this.chanceToCrit = chanceToCrit;
    }
}
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
    public bool isImmune = false;
    public bool isChoosingBlink = false;
    [Header("Game Manager")]
    private GameManager gameManager;

    [Header("Skill Settings")]
    public List<Skill> skills = new List<Skill>();
    public TextMeshProUGUI skillText; // Assign in Inspector

    [Header("Webcam Settings")]
    public GameObject _webcamObject; // Assign the webcam object in Inspector


    void Start()
    {

        skills.Add(new Skill("Infinity Attack", 50f, 1.5f, 15f));
        skills.Add(new Skill("Focus Beam", 30f, 2.0f, 10f));
        skills.Add(new Skill("Vertical Attack", 40f, 2.5f, 25f));
        skills.Add(new Skill("Horizontal Attack", 40, 1.5f, 15f));
        skills.Add(new Skill("X Strike", 100f, 2.0f, 10f));
        skills.Add(new Skill("Cyclone", 70f, 2.5f, 25f));
        skills.Add(new Skill("Blinkshot", 20f, 1.25f, 10f));
        skills.Add(new Skill("Shield", 0f, 1f, 1f));



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

    public void takeTurn()
    {
        if (gameManager == null)
        {
            Debug.LogWarning("GameManager is not assigned or found in the scene.");
            return;
        }

        
        if (gameManager.currentTurnIndex == 0)
        {
            Debug.Log($"{gameObject.name} is taking a turn.");
            isImmune = false;
        }
        else
        {
            Debug.Log($"{gameObject.name} cannot take a turn right now. It's not the player's turn.");
        }
    }

    public void ChooseBlink()
    {
        if (gameManager == null)
        {
            Debug.LogWarning("GameManager is not assigned or found in the scene.");
            return;
        }

        if (gameManager.currentTurnIndex == 0)
        {
            isChoosingBlink = true;
            Debug.Log($"{gameObject.name} is choosing to blink.");
            // Implement blink logic here, e.g., show UI for blink options
        }
        else
        {
            Debug.Log($"{gameObject.name} cannot choose to blink right now. It's not the player's turn.");
        }
    }
    // Call this method when it's the player's turn
    public void Attack(int skillIndex)
    {
        if (gameManager == null || gameManager.selectedTarget == null)
        {
            Debug.LogWarning("Cannot attack: GameManager or selected target is null.");
            return;
        }

        if (skillIndex < 0 || skillIndex >= skills.Count)
        {
            Debug.LogWarning("Invalid skill index.");
            return;
        }

        Skill selectedSkill = skills[skillIndex];
        float actualAttackPower = baseAttackPower + selectedSkill.additionalAttackPower;
        bool isCritical = false;

        // Critical chance logic
        if (Random.Range(0f, 100f) < selectedSkill.chanceToCrit)
        {
            actualAttackPower *= selectedSkill.criticalMultiplier;
            isCritical = true;
            Debug.Log($"{gameObject.name} used {selectedSkill.name} - CRITICAL HIT!");
        }
        else
        {
            Debug.Log($"{gameObject.name} used {selectedSkill.name}");
        }

        DealDamage(gameManager.selectedTarget, actualAttackPower, isCritical,skillIndex);
    }

    private void DealDamage(GameObject target, float damageAmount, bool wasCritical,int skillIndex)
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
        skillText.text = skills[skillIndex].name + " used! " + (wasCritical ? "Critical Hit!" : "");
        // Optionally check if it's your turn (gameManager can expose currentTurnPlayer)
        Invoke(nameof(EndAttack), 2f);
    }
    public void TakeDamage(float damage)
    {
        if (isImmune)
        {
            Debug.Log($"{gameObject.name} is immune to damage.");
            return;
        }
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
    public void EndAttack()
    {
        skillText.text = "";
        _webcamObject.SetActive(false); // Disable the webcam object after attack
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
