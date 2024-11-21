using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public EnemySpawner[] enemySpawners;
    public float timer = 0;
    [SerializeField] private float waveInterval = 5f;
    public int waveNumber = 1;
    public int totalEnemies = 0;

    void Start()
    {
        for (int i = 0; i < enemySpawners.Length; i++)
        {
            SetSpawningStatus(enemySpawners[i], true);
            if (enemySpawners[i].isSpawning)
            {
                totalEnemies += enemySpawners[i].defaultSpawnCount;
            }
        }
    }

    void Update()
    {
        if (totalEnemies == 0)
        {
            for (int i = 0; i < enemySpawners.Length; i++)
            {
                SetSpawningStatus(enemySpawners[i], false);
            }
            timer += Time.deltaTime;
            if (timer >= waveInterval)
            {
                timer = 0;
                waveNumber++;
                totalEnemies = 0;

                for (int i = 0; i < enemySpawners.Length; i++)
                {
                    SetSpawningStatus(enemySpawners[i], true);
                    Debug.Log("Setting spawn count for " + enemySpawners[i].spawnedEnemy.name);
                    enemySpawners[i].SetSpawnCount();
                    totalEnemies += enemySpawners[i].spawnCount;
                }
            }
        }
    }

    public void SetSpawningStatus(EnemySpawner enemySpawner, bool status)
    {
        // if (enemySpawner.spawnedEnemy is Boss && Boss.level <= waveNumber)
        // {
        //     enemySpawner.isSpawning = status;
        // }
        // else if (enemySpawner.spawnedEnemy is EnemyTargeting && EnemyTargeting.level <= waveNumber)
        // {
        //     enemySpawner.isSpawning = status;
        // }
        // else if (enemySpawner.spawnedEnemy is EnemyVertical && EnemyVertical.level <= waveNumber)
        // {
        //     enemySpawner.isSpawning = status;
        // }
        // else if (enemySpawner.spawnedEnemy is EnemyHorizontal && EnemyHorizontal.level <= waveNumber)
        // {
        //     enemySpawner.isSpawning = status;
        // }
        enemySpawner.isSpawning = status;
    }
}
