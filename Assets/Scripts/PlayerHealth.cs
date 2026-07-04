using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    [Range(0f, 1f)]
    public float defense = 0f; // 0 to 1 defense coefficient

    public int CurrentHealth => currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        float finalDamage = damage * (1f - defense);
        currentHealth -= Mathf.RoundToInt(finalDamage);
        Debug.Log($"Player took {finalDamage} damage (raw: {damage}, defense: {defense}). Current HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player Died!");
        // Add death logic here (e.g., restart level, play animation)
    }
}
