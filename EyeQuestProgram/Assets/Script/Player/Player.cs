using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]
public class Skill
{
    public string name;
    public float additionalAttackPower;

    public Skill(string name, float additionalAttackPower)
    {
        this.name = name;
        this.additionalAttackPower = additionalAttackPower;
    }
}
[System.Serializable]
public class PlayerStats
{
    public int level = 1;
    [Header("Health Settings")]
    public float maxHealth;
    public float currentHealth;

    [Header("Attack & Critical")]
    public float baseAttackPower = 0f;
    public float baseCriticalChance = 0f;

    [Header("Attributes")]
    public float strength;
    public float luck;
    public float tenacity;   

    [Header("Immune Settings")]
    public bool isImmune = false;
}
public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    public Image healthBar; // Assign in Inspector
    public TextMeshProUGUI healthText; // Assign in Inspector
    public PlayerStats stats = new PlayerStats();
    [Header("Game Manager")]
    private GameManager gameManager;

    [Header("Skill Settings")]
    public List<Skill> skills = new List<Skill>();
    public TextMeshProUGUI skillText; // Assign in Inspector
    public bool isChoosingBlink = false;
    public bool isChoosingShield = false;

    [Header("Webcam Settings")]
    public GameObject _webcamObject; // Assign the webcam object in Inspector
    public GameObject _ShieldGuideObject; // Assign the blink guide object in Inspector
    public GameObject _blinkGuideObject; // Assign the blink guide object in Inspector


    void Start()
    {

        skills.Add(new Skill("Infinity Attack", 0));
        skills.Add(new Skill("Focus Beam", 0));
        skills.Add(new Skill("Vertical Attack", 0));
        skills.Add(new Skill("Horizontal Attack", 0));
        skills.Add(new Skill("X Strike", 0));
        skills.Add(new Skill("Cyclone", 0));
        skills.Add(new Skill("Blinkshot", 0));
        skills.Add(new Skill("Shield", 0f));



        gameManager = FindObjectOfType<GameManager>();
        if (_webcamObject != null)
        {
            _webcamObject.SetActive(false); // Disable the webcam object at start
        }
        ApplyStats();
        if (healthBar != null)
        {
            healthBar.fillAmount = 1f; // Full health at start
        }
        if (healthText != null)
        {
            healthText.text = $"{(int)stats.currentHealth}/{(int)stats.maxHealth}"; // Display initial health
            stats.currentHealth = stats.maxHealth; // Initialize health
        }
    }
    void ApplyStats()
    {
        if (gameManager == null)
        {
            Debug.LogWarning("GameManager is not assigned or found in the scene.");
            return;
        }
        stats.strength = stats.level + 2 + (gameManager.StrengthItemModifier);
        stats.luck = stats.level + 2 + (gameManager.LuckItemModifier);
        stats.tenacity = stats.level + 2 + (gameManager.TenacityItemModifier);

        stats.maxHealth = MathF.Round(((stats.tenacity + gameManager.TenacityItemModifier) * 6f) + ((stats.strength + gameManager.StrengthItemModifier) * 1.5f));
        stats.currentHealth = stats.maxHealth;

        stats.baseAttackPower = MathF.Round(((stats.strength + gameManager.StrengthItemModifier) * 2f) + (stats.luck * 0.5f));

        stats.baseCriticalChance = 0.022f * (stats.luck + gameManager.LuckItemModifier) / (1+(0.22f * 0.7f) * stats.luck + gameManager.LuckItemModifier); ; 

        UpdateHealthBar();
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
            stats.isImmune = false;
        }
        else
        {
            Debug.Log($"{gameObject.name} cannot take a turn right now. It's not the player's turn.");
        }
    }

    public void ChooseBlink()
    {
        isChoosingBlink = true;
       
    }
        public void ChooseShield()
    {
        if (gameManager == null)
        {
            Debug.LogWarning("GameManager is not assigned or found in the scene.");
            return;
        }

        if (gameManager.currentTurnIndex == 0)
        {
            isChoosingShield = true;
            Debug.Log($"{gameObject.name} is choosing to Shield.");
        }
        else
        {
            Debug.Log($"{gameObject.name} cannot choose to Shield right now. It's not the player's turn.");
        }
    }
    // Call this method when it's the player's turn
    public void Attack(int skillIndex)
    {
        if (gameManager == null || gameManager.selectedTarget == null && skillIndex != 7)

    public GameObject _MinigameCore;
    // Call this method when it's the player's turn
    public void Attack(int skillIndex)
    {

        _MinigameCore.GetComponent<Minigame_1_core>()._DoneVision();

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

        //Skill selectedSkill = skills[skillIndex];
        float actualAttackPower = stats.baseAttackPower; //+ selectedSkill.additionalAttackPower;
        bool isCritical = false;

        // Calculate critical hit
        if (UnityEngine.Random.value < stats.baseCriticalChance)
        {
            isCritical = true;
            actualAttackPower *= 2f; // Double the attack power for critical hits
            Debug.Log($"{gameObject.name} landed a critical hit!");
        }
        if (skillIndex == 7) // Assuming skillIndex 7 is for "Shield"
        {
            _ShieldGuideObject.SetActive(false);
            DealDamage(gameManager.players[0], 0, false, skillIndex);

        }
        else if (skillIndex == 6) // Assuming skillIndex 6 is for "Blinkshot"
        {
            _blinkGuideObject.SetActive(false);
        }

        DealDamage(gameManager.selectedTarget, actualAttackPower, isCritical, skillIndex);
    }

    private void DealDamage(GameObject target, float damageAmount, bool wasCritical,int skillIndex)
    {
        if (target == null)
        {
            Debug.LogWarning("Target is null. Cannot deal damage.");
            return;
        }
        MonsterHealth monster = target.GetComponentInChildren<MonsterHealth>();
        Animator monAnim = target.GetComponentInChildren<Animator>();
        if (monster != null && damageAmount > 0)
        {
            monster.TakeDamage(damageAmount);
            monAnim.SetTrigger("_hit");
            Debug.Log($"{gameObject.name} attacks {target.name} for {damageAmount} damage.");
        }
        skillText.text = skills[skillIndex].name + " used! " + (wasCritical ? "Critical Hit!" : "");
        // Optionally check if it's your turn (gameManager can expose currentTurnPlayer)
        Invoke(nameof(EndAttack), 2f);
    }
    public void TakeDamage(float damage)
    {
        if (stats.isImmune) return;
        stats.currentHealth = Mathf.Max(0f, stats.currentHealth - damage);

        if (stats.currentHealth <= 0)
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
        if (stats.currentHealth < 0) stats.currentHealth = 0; // Ensure health doesn't go below 0
        if (stats.currentHealth > stats.maxHealth) stats.currentHealth = stats.maxHealth; // Ensure health doesn't exceed maxHealth
        if (healthBar != null)
        {
            healthBar.fillAmount = stats.currentHealth / stats.maxHealth; 
            healthText.text = $"{stats.currentHealth}/{stats.maxHealth}"; 
        }

    }
}
