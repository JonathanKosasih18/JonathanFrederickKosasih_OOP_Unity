using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemyType;

    void Start()
    {

    }


    void Update()
    {

    }

    public void MoveEnemy(float horizontal_speed, float vertical_speed)
    {
        Vector3 pos = transform.position;
        Vector3 velocity = new Vector3(horizontal_speed * Time.deltaTime, -vertical_speed * Time.deltaTime, 0);
        pos += transform.rotation * velocity;
        transform.position = pos;
    }

    void SpawnEnemy()
    {
        Vector2 minScreenBounds = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 maxScreenBounds = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        GameObject enemy = (GameObject)Instantiate(enemyPrefab);

        if (enemyType == 0) // Vertical Enemy
        {
            enemy.transform.position = new Vector2(Random.Range(minScreenBounds.x, maxScreenBounds.x), maxScreenBounds.y);
        }
        else if (enemyType == 1) // Horizontal Enemy from left
        {
            enemy.transform.position = new Vector2(minScreenBounds.x, Random.Range(minScreenBounds.y, maxScreenBounds.y));
        }
        else if (enemyType == 2) // Horizontal Enemy from right
        {
            enemy.transform.position = new Vector2(maxScreenBounds.x, Random.Range(minScreenBounds.y, maxScreenBounds.y));
        }
        else if (enemyType == 3) // Targeting Enemy
        {
            enemy.transform.position = new Vector2(Random.Range(minScreenBounds.x, maxScreenBounds.x), maxScreenBounds.y);
        }
        else if (enemyType == 4) // Boss Enemy
        {
            enemy.transform.position = new Vector2(Random.Range(minScreenBounds.x, maxScreenBounds.x), maxScreenBounds.y);
        }
    }
}
