using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public Enemy spawnedEnemy;

    [SerializeField] private int minimumKillsToIncreaseSpawnCount = 3;
    public int totalKill = 0;
    public int totalKillWave = 0;

    [SerializeField] private float spawnInterval = 3f;


    [Header("Spawned Enemies Counter")]
    public int spawnCount = 0;
    public int defaultSpawnCount = 1;
    public int spawnCountMultiplier = 1;
    public int multiplierIncreaseCount = 1;

    public CombatManager combatManager;
    public bool isSpawning = false;
    public float timer = 0;

    void Start()
    {
        spawnInterval = 5f;
        spawnCount = defaultSpawnCount;
    }

    void Update()
    {
        if (isSpawning && spawnCount > 0)
        {
            timer += Time.deltaTime;
            if (totalKillWave >= minimumKillsToIncreaseSpawnCount)
            {
                defaultSpawnCount += defaultSpawnCount * spawnCountMultiplier;
                spawnCountMultiplier += multiplierIncreaseCount;
                totalKillWave = 0;
            }
            if (timer > spawnInterval)
            {
                spawnCount--;
                Instantiate(spawnedEnemy);
                combatManager.totalEnemies--;
                timer = 0;
                spawnInterval = 3f;
            }
        }
    }

    public void SetSpawnCount()
    {
        Debug.Log("Setting spawn count for " + spawnedEnemy.name);
        spawnCount = defaultSpawnCount;
    }
}
