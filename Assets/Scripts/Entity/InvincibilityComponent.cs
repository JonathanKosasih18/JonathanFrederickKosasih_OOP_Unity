using System.Collections;
using UnityEngine;

public class InvincibilityComponent : MonoBehaviour
{
    [SerializeField] private int blinkingCount = 3;
    [SerializeField] private float blinkInterval = 0.1f;
    [SerializeField] private Material blinkMaterial;
    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    public bool isInvincible = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalMaterial = spriteRenderer.material;
        }
    }

    // Enumerator for the blinking effect
    private IEnumerator BlinkEffect()
    {
        isInvincible = true;

        for (int i = 0; i < blinkingCount; i++)
        {
            // Swap to the blink material
            spriteRenderer.material = blinkMaterial;
            yield return new WaitForSeconds(blinkInterval);

            // Swap back to the original material
            spriteRenderer.material = originalMaterial;
            yield return new WaitForSeconds(blinkInterval);
        }

        isInvincible = false; // End invincibility
    }

    // Public method to activate invincibility if not already invincible
    public void ActivateInvincibility()
    {
        if (!isInvincible)
        {
            StartCoroutine(BlinkEffect());
        }
    }
}
