using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BulletType
{
    Fire,
    Water,
    Lightning,
    Nature,
    Dark,
}

public class BulletPool : MonoBehaviour
{


    public static BulletPool Instance;

    [System.Serializable]
    public class BulletEntry
    {
        public BulletType bulletType;
        public GameObject bulletPrefab;
        public int poolSize = 3;
    }

    public List<BulletEntry> bulletEntries;

    private Dictionary<BulletType, Queue<GameObject>> bulletPools = new Dictionary<BulletType, Queue<GameObject>>();

    void Awake()
    {
        Instance = this;

        foreach (var entry in bulletEntries)
        {
            Queue<GameObject> pool = new Queue<GameObject>();
            for (int i = 0; i < entry.poolSize; i++)
            {
                GameObject bullet = Instantiate(entry.bulletPrefab);
                bullet.SetActive(false);
                pool.Enqueue(bullet);
            }
            bulletPools[entry.bulletType] = pool;
        }
    }

    public GameObject GetBullet(BulletType type)
    {
        if (!bulletPools.ContainsKey(type))
        {
            Debug.LogWarning("Bullet type not found: " + type);
            return null;
        }

        var pool = bulletPools[type];

        if (pool.Count > 0)
        {
            return pool.Dequeue();
        }
        else
        {
            // Instantiate new bullet if pool is empty
            var entry = bulletEntries.Find(e => e.bulletType == type);
            GameObject bullet = Instantiate(entry.bulletPrefab);
            bullet.SetActive(false);
            return bullet;
        }
    }

    public void ReturnBullet(BulletType type, GameObject bullet)
    {
        bullet.SetActive(false);
        if (bulletPools.ContainsKey(type))
        {
            bulletPools[type].Enqueue(bullet);
        }
        else
        {
            Debug.LogWarning("Bullet pool not found for type: " + type);
        }
    }
}
