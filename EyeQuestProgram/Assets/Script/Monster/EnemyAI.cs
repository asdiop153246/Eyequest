using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
// Base class
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Statsprofile statProfile;
    [SerializeField] private float totalStatPoints = 10f;
    protected GameManager gameManager;
    public TextMeshProUGUI ActionText;
    public GameObject _Highlight;
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
    [SerializeField] private Stats baseStats;
    public Stats CurrentStats => baseStats;


    protected virtual void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        ActionText = GameObject.Find("SkillCastText").GetComponent<TextMeshProUGUI>();
        if (statProfile != null && statProfile.IsValid())
        {
            ApplyStatProfile(statProfile, totalStatPoints);
        }
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
                player.TakeDamage(CurrentStats.Damage);
            }
        }

        Invoke(nameof(EndTurn), 1.5f);
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
    protected void ApplyStatProfile(Statsprofile profile, float totalPoints)
    {
        baseStats.Strength = totalPoints * profile.StrengthPercent;
        baseStats.Luck = totalPoints * profile.LuckPercent;
        baseStats.Tenacity = totalPoints * profile.TenacityPercent;

        // Derived stats
        baseStats.Damage = baseStats.Strength * 2f;
        baseStats.CriticalChance = baseStats.Luck * 0.3f;
        baseStats.CriticalPower = baseStats.Strength * 1.5f;
        baseStats.DamageReduction = baseStats.Tenacity * 0.5f;
    }
}

