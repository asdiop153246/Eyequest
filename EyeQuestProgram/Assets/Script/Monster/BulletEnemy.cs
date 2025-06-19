using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    private Transform target;
    public float speed = 5f;
    [SerializeField] private float _damage;
    private BulletType bulletType;


    public void SetTarget(Transform target, BulletType type, float damage)
    {
        this.target = target;
        this.bulletType = type;
        this._damage = damage;
    }

    void Update()
    {
        if (target == null)
        {
            BulletPool.Instance.ReturnBullet(bulletType, gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            target.GetComponent<Player>()?.TakeDamage(_damage); // example damage
            
            BulletPool.Instance.ReturnBullet(bulletType, gameObject);
        }
    }
}
