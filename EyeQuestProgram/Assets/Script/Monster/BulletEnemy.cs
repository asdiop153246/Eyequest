using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public float speed = 10f;
    private Transform target;
    private float lifeTime = 5f;
    public float damage;

    public void SetTarget(Transform playerTarget)
    {
        target = playerTarget;
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
            // Handle hit (optional)
            target.GetComponent<Animator>()?.SetTrigger("_hit");
            target.GetComponent<Player>()?.TakeDamage(damage);
            DisableBullet();
        }
    }

    void DisableBullet()
    {
        gameObject.SetActive(false);
    }
}
