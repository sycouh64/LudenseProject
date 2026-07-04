using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHP = 100f;
    private float currentHP;

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;

    public event Action<float, float> OnHealthChanged;

    void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(currentHP, 0);

        Debug.Log($"[Player] {damage} 데미지 받음 → 현재 HP: {currentHP} / {maxHP}");

        OnHealthChanged?.Invoke(currentHP, maxHP);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHP += amount;
        currentHP = Mathf.Min(currentHP, maxHP);

        Debug.Log($"[Player] {amount} 회복 → 현재 HP: {currentHP} / {maxHP}");

        OnHealthChanged?.Invoke(currentHP, maxHP);
    }

    void Die()
    {
        Debug.Log("[Player] 사망");
    }
}