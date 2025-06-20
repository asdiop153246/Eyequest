using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    private Transform target;
    private Transform _Movetarget;
    public float speed = 5f;
    [SerializeField] private float _damage;
    private BulletType bulletType;


    public void SetTarget(Transform target, BulletType type, float damage)
    {
        this.target = target;
        _Movetarget = target.GetComponent<Player>()._PlayerHitTarget.transform;
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

        transform.position = Vector3.MoveTowards(transform.position, _Movetarget.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, _Movetarget.position) < 0.2f)
        {
            target.GetComponent<Player>()?.TakeDamage(_damage); // example damage
            
            BulletPool.Instance.ReturnBullet(bulletType, gameObject);
        }
    }
}
