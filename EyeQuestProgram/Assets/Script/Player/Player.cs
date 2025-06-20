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
    private Animator animator;
    [Header("Game Manager")]
    private GameManager gameManager;
    public GameObject _turnEffect;
    public GameObject _DefeatPanel;

    [Header("Skill Settings")]
    public List<Skill> skills = new List<Skill>();
    public GameObject[] _skillsBullet;
    public GameObject[] _SwipePrefab;
    public Transform _bulletSpawnPoint;
    public TextMeshProUGUI skillText; // Assign in Inspector
    public GameObject Shield;
    public bool isChoosingBlink = false;
    public bool isChoosingShield = false;
    public bool isAction = false;

    public GameObject _gethitEffect;

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


        animator = GetComponent<Animator>();
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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            //DealDamage(gameManager.selectedTarget, 100f, false, 0); // Test Damage
        }
    }
    public void ResetForNewStage()
    {
        stats.currentHealth = stats.maxHealth; // Reset health for new stage
        UpdateHealthBar();
        isChoosingBlink = false;
        isChoosingShield = false;
        _webcamObject.SetActive(false); // Disable the webcam object at start of new stage
        _ShieldGuideObject.SetActive(false);
        _blinkGuideObject.SetActive(false);
    }

    public void ApplyStats()
    {
        Debug.Log("Applying player stats...");
        stats.level = gameManager.stageIndex; // Initialize player level from GameManager
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

        stats.baseCriticalChance = 0.022f * (stats.luck + gameManager.LuckItemModifier) / (1 + (0.22f * 0.7f) * stats.luck + gameManager.LuckItemModifier); ;

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
            isAction = false;
            stats.isImmune = false;
            _turnEffect.SetActive(true); // Activate the turn effect
            Shield.SetActive(false);

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

    public GameObject _MinigameCore;
    // Call this method when it's the player's turn
    public GameObject _GameManagerCore;
    
    public void Attack(int skillIndex)
    {
        if (_MinigameCore != null)
        {
            _MinigameCore.GetComponent<Minigame_1_core>()._DoneVision();
        }

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
        isAction = true;
        float actualAttackPower = stats.baseAttackPower; //+ selectedSkill.additionalAttackPower;
        bool isCritical = false;

        // Calculate critical hit
        if (UnityEngine.Random.value < stats.baseCriticalChance)
        {
            isCritical = true;
            actualAttackPower *= 2f; // Double the attack power for critical hits
            Debug.Log($"{gameObject.name} landed a critical hit!");
        }
        if (animator != null)
        {
            animator.SetTrigger("_attack");
        }
        if (skillIndex == 7) // Assuming skillIndex 7 is for "Shield"
        {
            _ShieldGuideObject.SetActive(false);
            StartCoroutine(DelayedDamage(2, gameManager.selectedTarget.transform, actualAttackPower, isCritical, skillIndex, transform.position));
        }
        else if (skillIndex == 6) // Assuming skillIndex 6 is for "Blinkshot"
        {
            _blinkGuideObject.SetActive(false);
            StartCoroutine(DelayedDamage(2, gameManager.selectedTarget.transform, actualAttackPower, isCritical, skillIndex, _bulletSpawnPoint.position));
        }
        else
        {
            if (_skillsBullet != null && _skillsBullet.Length > skillIndex)
            {
                StartCoroutine(DelayedDamage(2, gameManager.selectedTarget.transform, actualAttackPower, isCritical, skillIndex, _bulletSpawnPoint.position));
            }
            else
            {
                Debug.LogWarning("Bullet prefab not found or index out of range.");
            }
        }

        StartCoroutine(_DelayCooldown());
    }

    IEnumerator _DelayCooldown()
    {
        yield return new WaitForSeconds(2f);
        _GameManagerCore.GetComponent<GameManager>()._isAlreadySelectionSkill = false;
    }
    private IEnumerator DelayedDamage(float delay, Transform target, float damage, bool wasCritical, int skillIndex, Vector3 _spawnPosition)
    {
        yield return new WaitForSeconds(delay);

        // Adjust spawn position based on skill index
        Vector3 adjustedPosition = _spawnPosition;
        if  (skillIndex == 0 && target != null)
        {
            GameObject infinitybullet = Instantiate(_skillsBullet[skillIndex], _spawnPosition, Quaternion.identity);
            InfinityBullet infinityBullet = infinitybullet.GetComponent<InfinityBullet>();
            infinityBullet.SetTarget(gameManager.selectedTarget.transform, damage, wasCritical, skillIndex, this);
        }
        else
        {
            if (skillIndex == 2 && target != null)
            {
                Debug.Log("Performing Vertical Swipe");
                StartCoroutine(PerformSlashAttack(target, damage, wasCritical, 2, 0));
            }
            else if (skillIndex == 3 && target != null)
            {
                Debug.Log("Performing Horizontal Swipe");
                StartCoroutine(PerformSlashAttack(target, damage, wasCritical, 2, 1));
            }
            else if (skillIndex == 4 && target != null)
            {
                Debug.Log("Performing XStrike Swipe");
                StartCoroutine(PerformSlashAttack(target, damage, wasCritical, 2, 2));
            }
            else if (skillIndex == 7 && target != null)
            {
                Debug.Log("Shield Active");
                stats.isImmune = true;
                Shield.SetActive(true);
                DealDamage(target.gameObject, 0, false, skillIndex);
            }
            else
            {
                GameObject bullet = Instantiate(_skillsBullet[skillIndex], adjustedPosition, Quaternion.identity);
                PlayerBullet bulletScript = bullet.GetComponent<PlayerBullet>();
                bulletScript.SetTarget(target, damage, wasCritical, skillIndex, this);
            }

        }
    }
    private IEnumerator PerformSlashAttack(Transform target, float damage, bool isCritical, int skillIndex, int effectindex)
    {
        Vector3 spawnPos = target.position + Vector3.up * 1f;
        GameObject slashEffect = Instantiate(_SwipePrefab[effectindex], spawnPos, Quaternion.Euler(0, 0, 0));

        float forwardAngle = 0f;
        float reverseAngle = 0f;

        // Determine rotation angles
        if (effectindex == 1) // Horizontal Swipe
        {
            forwardAngle = 180f;
            reverseAngle = 0f;
        }
        else if (effectindex == 0) // Vertical Swipe
        {
            forwardAngle = 90f;
            reverseAngle = -90f;
        }
        else if (effectindex == 2)
        {
            forwardAngle = -55;
            reverseAngle = 55;
        }

        float duration = 1f;

        // Forward swipe (instant snap)
        slashEffect.transform.rotation = Quaternion.Euler(0, 0, forwardAngle);
        yield return new WaitForSeconds(duration);

        // Reverse swipe (instant snap)
        slashEffect.transform.rotation = Quaternion.Euler(0, 0, reverseAngle);
        yield return new WaitForSeconds(duration);

        DealDamage(target.gameObject, damage, isCritical, skillIndex);
        //yield return new WaitForSeconds(1f);
        Destroy(slashEffect);
    }



    private void DealDamage(GameObject target, float damageAmount, bool wasCritical, int skillIndex)
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
        Invoke(nameof(EndAttack), 3f);
    }
    public void TakeDamage(float damage)
    {
        if (stats.isImmune) return;
        stats.currentHealth = Mathf.Max(0f, stats.currentHealth - damage);
        GetComponent<Animator>().SetTrigger("_gethit");
        StartCoroutine(ShowGetHitEffect());
        if (stats.currentHealth <= 0)
        {
            Die();
        }

        UpdateHealthBar();
    }
    IEnumerator ShowGetHitEffect()
    {
        if (_gethitEffect != null)
        {
            _gethitEffect.SetActive(true);
            yield return new WaitForSeconds(2f); // Show effect for 0.5 seconds
            _gethitEffect.SetActive(false);
        }
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
        _turnEffect.SetActive(false); // Deactivate the turn effect
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

