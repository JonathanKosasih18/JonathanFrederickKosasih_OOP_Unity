using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontal : Enemy
{
    [SerializeField] float speed = 1f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyType == 1)
        {
            MoveEnemy(speed, 0);
        }
        else if (enemyType == 2)
        {
            MoveEnemy(-speed, 0);
        }
        DestroyEnemy();
    }

    void DestroyEnemy()
    {
        Vector2 minScreenBounds = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 maxScreenBounds = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        if (transform.position.x < minScreenBounds.x || transform.position.x > maxScreenBounds.x)
        {
            Destroy(gameObject);
        }
    }
}
