using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
// Base class
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Statsprofile statProfile;
    private float totalStatPoints;
    [SerializeField] protected GameManager gameManager;
    public TextMeshProUGUI ActionText;
    public GameObject _Highlight;
    public Transform bulletSpawnPoint;
    public int _MonsterID;
    [System.Serializable]
    public class Stats
    {
        public float Strength;
        public float Luck;
        public float Tenacity;
        public float maxHealth;
        public float Damage;
        public float CriticalChance;
        public float CriticalPower;
        public float DamageReduction;

    }
    [SerializeField] private Stats baseStats = new Stats();
    public Stats CurrentStats => baseStats;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    protected virtual void Start()
    {
        
        ActionText = GameObject.Find("SkillCastText").GetComponent<TextMeshProUGUI>();
        
    }

    public virtual void TakeTurn()
    {
        Debug.Log($"{gameObject.name} is thinking...");
        Invoke(nameof(PerformAction), 3f);
    }

    protected virtual void PerformAction()
    {
        GameObject target = gameManager.GetRandomPlayer();
        if (target != null)
        {
            Debug.Log($"{gameObject.name} attacks {target.name} for {CurrentStats.Damage} damage.");
            ActionText.text = $"{gameObject.name} used Normal Attack!";
            Player player = target.GetComponent<Player>();
            if (player != null)
            {
                GetComponentInChildren<Animator>()?.SetTrigger("_attack");
                StartCoroutine(delayBullet());
                //player.TakeDamage(CurrentStats.Damage);
            }
        }

        
    }
    IEnumerator delayBullet()
    {
        yield return new WaitForSeconds(1.2f);
        ShootAtPlayer(gameManager.GetRandomPlayer());
        yield return new WaitForSeconds(1.5f);
        Invoke(nameof(EndTurn), 1.5f);
    }
    public void ShootAtPlayer(GameObject player)
    {
        GameObject bulletObj = BulletPool.Instance.GetBullet();

        if (bulletSpawnPoint != null)
        {
            bulletObj.transform.position = bulletSpawnPoint.position;
            bulletObj.transform.rotation = bulletSpawnPoint.rotation;
        }
        else
        {
            bulletObj.transform.position = transform.position;
            bulletObj.transform.rotation = Quaternion.identity;
        }

        bulletObj.SetActive(true);

        BulletEnemy bullet = bulletObj.GetComponent<BulletEnemy>();
        bullet.damage = CurrentStats.Damage;
        bullet.SetTarget(player.transform);
    }

    protected void EndTurn()
    {
        ActionText.text = "";
        gameManager.NextTurn();
    }

    void OnMouseDown()
    {
        if (gameManager != null && gameManager.currentTurnIndex == 0)
        {
            gameManager.SelectTarget(this.gameObject);
        }
    }
    protected void ApplyStatProfile(Statsprofile profile, float totalStatPoints)
    {
        Debug.Log($"Applying stat profile for {gameObject.name} with total stat points: {totalStatPoints}");
        Debug.Log($"profile stats points: {profile.statsPoints}, StrengthPercent: {profile.StrengthPercent}, LuckPercent: {profile.LuckPercent}, TenacityPercent: {profile.TenacityPercent}");
        totalStatPoints = profile.statsPoints * gameManager.statsModifier;

        baseStats.Strength = totalStatPoints * profile.StrengthPercent;
        baseStats.Luck = totalStatPoints * profile.LuckPercent;
        baseStats.Tenacity = totalStatPoints * profile.TenacityPercent;

        // Derived stats
        baseStats.Damage = MathF.Round((baseStats.Strength * 2f) + (baseStats.Luck * 0.5f));
        baseStats.maxHealth = MathF.Round((baseStats.Tenacity * 6f) + (baseStats.Strength * 1.5f));
        baseStats.CriticalChance = 0.22f * baseStats.Luck / (1f + 0.22f * baseStats.Luck);
        baseStats.CriticalPower = 2f;
    }
    public virtual void ApplyTierModifier(GameManager.EnemyTier tier, float baseModifier)
    {
  
        float finalMultiplier = baseModifier;

        switch (tier)
        {
            case GameManager.EnemyTier.Miniboss:
                finalMultiplier = 8;
                break;
            case GameManager.EnemyTier.Boss:
                finalMultiplier = 16;
                break;
        }

        if (statProfile != null && statProfile.IsValid())
        {
            Debug.Log($"Applying tier modifier for {gameObject.name} with tier {tier} and modifier {finalMultiplier}");
            ApplyStatProfile(statProfile, statProfile.statsPoints * finalMultiplier);
        }

        // Optional: change appearance or effects
        if (tier == GameManager.EnemyTier.Boss)
        {
            GetComponentInChildren<Animator>().GetComponent<Transform>().localScale *= 1.5f; // Example: make boss larger
        }
        else if (tier == GameManager.EnemyTier.Miniboss)
        {
            GetComponentInChildren<Animator>().GetComponent<Transform>().localScale *= 1.2f; // Example: make miniboss larger
        }
    }
}

