using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int scoreValue = 10;

    private int currentHealth;

    public int CurrentHealth => currentHealth;

    public event Action OnDeath;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
            Invoke(nameof(ResetColor), 0.1f);
        }

        Debug.Log(gameObject.name + " took " + damage + " damage. Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void ResetColor()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
    }

    private void Die()
    {
        OnDeath?.Invoke();

        GameManager gameManager = FindFirstObjectByType<GameManager>();

        if (gameManager != null && scoreValue > 0)
        {
            gameManager.AddScore(scoreValue);
        }

        StartCoroutine(DeathEffect());
    }

    private System.Collections.IEnumerator DeathEffect()
    {
        float duration = 0.1f;
        float elapsed = 0f;

        Vector3 startScale = transform.localScale;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float progress = elapsed / duration;

            transform.localScale = Vector3.Lerp(
                startScale,
                Vector3.zero,
                progress
            );

            yield return null;
        }

        Destroy(gameObject);
    }
}