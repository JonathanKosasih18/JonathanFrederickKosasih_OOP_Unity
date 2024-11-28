using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVertical : Enemy
{
    [SerializeField] float speed = 1f;
    public CombatManager combatManager;

    void Start()
    {
        combatManager = GameObject.Find("CombatManager").GetComponent<CombatManager>();
        level = 1;
    }

    public string name = "EnemyVertical";

    // Update is called once per frame
    void Update()
    {
        MoveEnemy(0, speed);
        DestroyEnemy();
    }

    void DestroyEnemy()
    {
        Vector2 minScreenBounds = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        if (transform.position.y < minScreenBounds.y)
        {
            combatManager.totalEnemies--;
            Destroy(gameObject);
        }
    }
}
