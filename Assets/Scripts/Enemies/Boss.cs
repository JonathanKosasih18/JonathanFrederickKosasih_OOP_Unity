using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Boss : Enemy
{
    [SerializeField] float speed = 1f;

    [Header("Weapon Stats")]
    [SerializeField] public float shootIntervalInSeconds = 3f;

    [Header("Bullets")]
    [SerializeField] private EnemyBullet bulletPrefab;

    [Header("Bullet Pool")]
    private IObjectPool<EnemyBullet> enemyBulletPool;  // Changed to IObjectPool<EnemyBullet>

    private float timer;

    void Awake()
    {
        enemyBulletPool = new ObjectPool<EnemyBullet>(BulletFactory, OnGetFromPool, OnReleaseToPool, OnDestroyObject);
    }

    EnemyBullet BulletFactory()
    {
        EnemyBullet bulletInstance = Instantiate(bulletPrefab);
        bulletInstance.enemyBulletPool = enemyBulletPool;  // Ensure pool is assigned to bullet instance
        return bulletInstance;
    }

    void OnGetFromPool(EnemyBullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    void OnReleaseToPool(EnemyBullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    void OnDestroyObject(EnemyBullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    void Start()
    {
        
    }

    void Update()
    {
        MoveEnemy(speed, 0);
        WallBounce();

        timer += Time.deltaTime;
        if (timer >= shootIntervalInSeconds)
        {
            Shoot();
            timer = 0f;
        }
    }

    void WallBounce()
    {
        Vector2 minScreenBounds = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 maxScreenBounds = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        if (transform.position.x < minScreenBounds.x || transform.position.x > maxScreenBounds.x)
        {
            speed = -speed;
        }
    }

    void DestroyEnemy()
    {
        Vector2 minScreenBounds = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 maxScreenBounds = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        if (transform.position.y < minScreenBounds.y)
        {
            Destroy(gameObject);
        }
    }

    public void Shoot()
    {
        EnemyBullet bulletObject = enemyBulletPool.Get();
        bulletObject.transform.SetPositionAndRotation(transform.position, transform.rotation);
    }
}
