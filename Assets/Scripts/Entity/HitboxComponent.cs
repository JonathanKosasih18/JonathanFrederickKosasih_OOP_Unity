using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxComponent : MonoBehaviour
{
    [SerializeField] private HealthComponent health;
    [SerializeField] private InvincibilityComponent invincibility;

    void Start()
    {
        // Attempt to get the HealthComponent and InvincibilityComponent from the GameObject
        health = GetComponent<HealthComponent>();
        invincibility = GetComponent<InvincibilityComponent>();

        if (health == null)
        {
            Debug.LogError("HealthComponent is missing on this GameObject.");
        }

        // Check if the collided object has an InvincibilityComponent
        if (invincibility != null)
        {
            // Start the invincibility effect
            invincibility.ActivateInvincibility();
        }
    }

    // Method to apply damage using an integer value
    public void Damage(int damageAmount)
    {
        if (health != null && (invincibility == null || !invincibility.isInvincible))
        {
            health.Subtract(damageAmount);  // Call Subtract on HealthComponent
        }
    }

    // Overloaded method to apply damage using a Bullet object
    public void Damage(Bullet bullet)
    {
        if (health != null && (invincibility == null || !invincibility.isInvincible))
        {
            health.Subtract(bullet.damage);  // Assume Bullet has a damage property
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the other object has the same tag as this one. If so, return.
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null && !invincibility.isInvincible)
            {
                Damage(bullet);
            }
        }
    }
}
