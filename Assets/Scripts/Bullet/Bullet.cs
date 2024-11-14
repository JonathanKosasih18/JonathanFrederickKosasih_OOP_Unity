using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20;
    public int damage = 10;
    private Rigidbody2D rb;
    private Vector2 minScreenBounds;
    private Vector2 maxScreenBounds;

    public IObjectPool<Bullet> objectPool;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Camera mainCamera = Camera.main;
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        minScreenBounds = mainCamera.transform.position - new Vector3(cameraWidth / 2, cameraHeight / 2);
        maxScreenBounds = mainCamera.transform.position + new Vector3(cameraWidth / 2, cameraHeight / 2);
    }

    void Update()
    {
        MoveBullet();

        // Return to pool if bullet goes off-screen
        if (transform.position.x < minScreenBounds.x || transform.position.x > maxScreenBounds.x ||
            transform.position.y < minScreenBounds.y || transform.position.y > maxScreenBounds.y)
        {
            ReturnToPool();
        }
    }

    void MoveBullet()
    {
        Vector3 pos = transform.position;
        Vector3 velocity = new Vector3(0, bulletSpeed * Time.deltaTime, 0);
        pos += transform.rotation * velocity;
        transform.position = pos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Return bullet to pool on collision
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ReturnToPool();
        }
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        if (objectPool != null)
        {
            objectPool.Release(this);
        }
    }
}
