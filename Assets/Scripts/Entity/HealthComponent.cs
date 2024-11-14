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

    // Initialization
    void Start()
    {
        health = maxHealth;
    }

    // Method to subtract health
    public void Subtract(int damage)
    {
        health -= damage;

        // Check if health is below or equal to zero
        if (health <= 0)
        {
            Destroy(gameObject); // Destroy the object
        }
    }
}
