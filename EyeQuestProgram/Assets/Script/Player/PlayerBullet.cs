using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 10f;
    private Transform target;
    private float lifeTime = 5f;

    private float damage;
    private bool isCritical;
    private int skillIndex;
    private Player owner;

    public void SetTarget(Transform selectedTarget, float damageAmount, bool wasCritical, int skillIndexUsed, Player playerOwner)
    {
        target = selectedTarget;
        damage = damageAmount;
        isCritical = wasCritical;
        skillIndex = skillIndexUsed;
        owner = playerOwner;

        Invoke(nameof(DisableBullet), lifeTime);
    }

    void Update()
    {
        if (target == null)
        {
            DisableBullet();
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            ApplyDamage();
            DisableBullet();
        }
    }

    void ApplyDamage()
    {
        MonsterHealth monster = target.GetComponentInChildren<MonsterHealth>();
        Animator monAnim = target.GetComponentInChildren<Animator>();

        if (monster != null && damage > 0)
        {
            monster.TakeDamage(damage);
            monAnim?.SetTrigger("_hit");
            Debug.Log($"{owner.name} dealt {damage} {(isCritical ? "CRITICAL" : "")} with skill {skillIndex}");
        }

        if (owner != null)
        {
            owner.skillText.text = owner.skills[skillIndex].name + " used! " + (isCritical ? "Critical Hit!" : "");
            owner.Invoke(nameof(owner.EndAttack), 3f);
        }
    }

    void DisableBullet()
    {
        gameObject.SetActive(false);
    }
}

