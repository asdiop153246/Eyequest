using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityBullet : MonoBehaviour
{
    public float duration = 2f; // How long the bullet takes before hitting
    public float size = 1f;     // Size of the infinity path
    public float spinSpeed = 2f;
    public float yOffset = 1.5f; // Adjust this value as needed

    private Transform target;
    private Vector3 startPos;
    private float elapsed;
    private float damage;
    private bool wasCritical;
    private int skillIndex;
    private Player attacker;

    public void SetTarget(Transform target, float damage, bool wasCritical, int skillIndex, Player attacker)
    {
        this.target = target;
        this.damage = damage;
        this.wasCritical = wasCritical;
        this.skillIndex = skillIndex;
        this.attacker = attacker;

        startPos = transform.position;
        elapsed = 0f;
    }

    void Update()
    {
        if (target == null) return;

        elapsed += Time.deltaTime;
        float t = Mathf.Clamp01(elapsed / duration);

        // Position on infinity (lemniscate) path
        float angle = t * 2 * Mathf.PI * spinSpeed;
        float x = size * Mathf.Sin(angle);
        float y = size * Mathf.Sin(angle) * Mathf.Cos(angle);

        // Interpolate towards the target

        Vector3 targetOffset = target.position + Vector3.up * yOffset;
        Vector3 center = Vector3.Lerp(startPos, targetOffset, t);
        transform.position = center + transform.right * x + transform.up * y;

        if (t >= 1f)
        {
            HitTarget();
        }
    }

    void HitTarget()
    {
        MonsterHealth monster = target.GetComponentInChildren<MonsterHealth>();
        Animator monsterAnim = target.GetComponentInChildren<Animator>();
        if (target.GetComponentInChildren<MonsterHealth>())
        {
            monster.TakeDamage(damage);
            monsterAnim.SetTrigger("_hit");
        }

        attacker.skillText.text = attacker.skills[skillIndex].name + " used! " + (wasCritical ? "Critical Hit!" : "");
        attacker.Invoke(nameof(attacker.EndAttack), 1.5f);

        Destroy(gameObject);
    }
}
