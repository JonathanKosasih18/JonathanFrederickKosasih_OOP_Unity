using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    // Maximum health value
    [SerializeField] private int maxHealth = 100;

    // Current health value
    private int health;

    // Getter for health
    public int Health
    {
        get { return health; }
    }

    public EnemySpawner[] enemySpawner;
    public CombatManager combatManager;

    // Initialization
    void Start()
    {
        health = maxHealth;
        combatManager = GameObject.Find("CombatManager").GetComponent<CombatManager>();
        enemySpawner = combatManager.enemySpawners;
    }

    // Method to subtract health
    public void Subtract(int damage)
    {
        health -= damage;

        // Check if health is below or equal to zero
        if (health <= 0)
        {
            for (int i = 0; i < enemySpawner.Length; i++)
            {
                if (enemySpawner[i].spawnedEnemy.name == gameObject.GetComponent<Enemy>().name.Replace("(Clone)", "").Trim()) // Check if the enemy spawner is the same as the enemy
                {
                    enemySpawner[i].totalKill++;
                    enemySpawner[i].totalKillWave++;
                }
                Debug.Log("Enemy Spawner: " + enemySpawner[i].spawnedEnemy.name);
                Debug.Log(gameObject.GetComponent<Enemy>().name.Replace("(Clone)", "").Trim());
            }
            Debug.Log("Enemy Killed");
            combatManager.totalEnemies--;
            combatManager.points += 10;
            Destroy(gameObject); // Destroy the object
        }
    }
}
