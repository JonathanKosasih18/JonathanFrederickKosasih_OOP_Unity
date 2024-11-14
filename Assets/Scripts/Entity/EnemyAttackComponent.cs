using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackComponent : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private EnemyBullet bullet;

    private void Start()
    {
        // Set the Collider as a trigger to detect overlaps without physical collisions
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other object has the same tag as this one. If so, return.
        if (other.CompareTag(gameObject.tag))
        {
            return;
        }

        // Check if the collided object has a HitboxComponent
        HitboxComponent hitbox = other.GetComponent<HitboxComponent>();
        if (hitbox != null)
        {
            // Call the Damage method on HitboxComponent, passing the damage value
            hitbox.Damage(damage);
        }

        // Check if the collided object has an InvincibilityComponent
        InvincibilityComponent invincibility = other.GetComponent<InvincibilityComponent>();
        if (invincibility != null)
        {
            // Start the invincibility effect
            invincibility.ActivateInvincibility();
        }
    }
}
