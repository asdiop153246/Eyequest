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
    public BulletType myBulletType;
    public Transform bulletSpawnPoint;
    public int _MonsterID;
    private GameManager.EnemyTier _enemyTier;

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

    public void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            TakeTurn();
        }
    }

    public GameObject _Thinking;

    public AudioClip[] _SoundSFX;
    public virtual void TakeTurn()
    {
        if(_SoundSFX[0])
        GetComponent<AudioSource>().PlayOneShot(_SoundSFX[0]);
        Debug.Log($"{gameObject.name} is thinking...");
        if(_Thinking)
            _Thinking.SetActive(true);

        Invoke(nameof(PerformAction), 3f);

       
    }

    protected virtual void PerformAction()
    {
        if (_Thinking)
            _Thinking.SetActive(false);

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
        
        if (_enemyTier == GameManager.EnemyTier.Boss)
        {
            yield return new WaitForSeconds(0.5f);
        }
        else if (_enemyTier == GameManager.EnemyTier.Miniboss)
        {
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
        }
        if (_SoundSFX[1])
            GetComponent<AudioSource>().PlayOneShot(_SoundSFX[1]);
        ShootAtPlayer(gameManager.GetRandomPlayer());
        //ShootAtPlayer(gameManager._Player.GetComponent<Player>()._PlayerHitTarget);
        yield return new WaitForSeconds(1f);
        Invoke(nameof(EndTurn), 1.5f);
    }
    public void ShootAtPlayer(GameObject target)
    {
        if (target == null) return;
        Debug.Log($"Target name {target.name}");
        GameObject bullet = BulletPool.Instance.GetBullet(myBulletType);

        /*if (myBulletType == BulletType.Nature) // or BulletType.Nature, depending on your enum
        {
            // Spawn at player's ground position
            Vector3 groundPosition = target.transform.position;
            groundPosition.y = -1f; // adjust to just above ground level
            bullet.transform.position = groundPosition;
            StartCoroutine(DelaybeforeDestroyNature(bullet, target));
            return;
        }*/
        /*else
        {*/
            // Normal spawn from bullet point
            bullet.transform.position = bulletSpawnPoint.position;
        //}

        bullet.transform.rotation = Quaternion.identity;
        bullet.SetActive(true);
        if (_enemyTier == GameManager.EnemyTier.Boss)
        {
            bullet.GetComponent<BulletEnemy>().SetTarget(target.transform, myBulletType, CurrentStats.Damage, 15f);
        }
        else if (_enemyTier == GameManager.EnemyTier.Miniboss)
        {
            bullet.GetComponent<BulletEnemy>().SetTarget(target.transform, myBulletType, CurrentStats.Damage, 15f);
        }
        else
        {
            bullet.GetComponent<BulletEnemy>().SetTarget(target.transform, myBulletType, CurrentStats.Damage,15f);
        }
        
    }

    IEnumerator DelaybeforeDestroyNature(GameObject _bullet, GameObject _target)
    {
        _bullet.SetActive(true);
        
        if (_enemyTier == GameManager.EnemyTier.Boss)
        {
            yield return new WaitForSeconds(4f);
        }
        else if (_enemyTier == GameManager.EnemyTier.Miniboss)
        {
            yield return new WaitForSeconds(3f);
        }
        else
        {
            yield return new WaitForSeconds(2f);
        }

        _target.GetComponent<Player>().TakeDamage(CurrentStats.Damage, myBulletType);
        BulletPool.Instance.ReturnBullet(BulletType.Nature, _bullet);
        
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
        
        Debug.Log($"profile stats points: {profile.statsPoints}, StrengthPercent: {profile.StrengthPercent}, LuckPercent: {profile.LuckPercent}, TenacityPercent: {profile.TenacityPercent}");
        Debug.Log($"Applying stat profile for {gameObject.name} with total stat points: {totalStatPoints}");
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
        _enemyTier = tier;
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
            ApplyStatProfile(statProfile, statProfile.statsPoints + finalMultiplier);
        }

        // Optional: change appearance or effects
        if (tier == GameManager.EnemyTier.Boss)
        {
            GetComponentInChildren<Animator>().GetComponent<Transform>().localScale *= 1f; // Example: make boss larger
        }
        else if (tier == GameManager.EnemyTier.Miniboss)
        {
            GetComponentInChildren<Animator>().GetComponent<Transform>().localScale *= 1f; // Example: make miniboss larger
        }
    }
}

