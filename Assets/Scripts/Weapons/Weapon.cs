using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] public float shootIntervalInSeconds = 3f;

    [Header("Bullets")]
    [SerializeField] private Bullet bulletPrefab;

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;

    private float timer;

    void Awake()
    {
        objectPool = new ObjectPool<Bullet>(BulletFactory, OnGetFromPool, OnReleaseToPool, OnDestroyObject);
    }

    Bullet BulletFactory()
    {
        Bullet bulletInstance = Instantiate(bulletPrefab);
        bulletInstance.objectPool = objectPool;
        return bulletInstance;
    }

    void OnGetFromPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    void OnReleaseToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    void OnDestroyObject(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= shootIntervalInSeconds)
        {
            Shoot();
            timer = 0f;
        }
    }

    public void Shoot()
    {
        Bullet bulletObject = objectPool.Get();

        // Define the offset vector to move the bullet's spawn point upward
        Vector3 offset = new Vector3(0, 0.7f, 0);

        // Set the bullet's position and rotation with the offset applied
        bulletObject.transform.SetPositionAndRotation(transform.position + offset, transform.rotation);
    }
}
