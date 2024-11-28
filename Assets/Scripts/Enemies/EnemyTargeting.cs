using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargeting : Enemy
{
    float distanceToPlayer;
    public float speed = 2f;
    public CombatManager combatManager;
    void Start()
    {
        combatManager = GameObject.Find("CombatManager").GetComponent<CombatManager>();
        level = 2;
    }

    public string name = "EnemyTargeting";

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            Vector2 chaseDirection = (player.transform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        DestroyEnemy();
    }

    void DestroyEnemy()
    {
        Vector2 minScreenBounds = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 maxScreenBounds = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        if (transform.position.x < minScreenBounds.x || transform.position.x > maxScreenBounds.x || transform.position.y < minScreenBounds.y || transform.position.y > maxScreenBounds.y)
        {
            Debug.Log("Enemy out of bounds");
            combatManager.totalEnemies--;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object collided with has the tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy collided with player");
            combatManager.totalEnemies--;
            Destroy(gameObject);
        }
    }
}
