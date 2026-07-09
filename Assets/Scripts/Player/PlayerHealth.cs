using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHasHP
{
    [SerializeField] public float maxHP = 100;
    [SerializeField] private float currentHP;

    [SerializeField] public float speed = 20;
    [SerializeField] public static float currentSpeed;
    [Range(0f, 1f)] public float defense = 0f; // 0 to 1 사이의 값


    public float CurrentHP => currentHP;
    public float MaxHP => maxHP;
    public float CurrentSpeed=> currentSpeed;
    public float OriginalSpeed => speed;
    private void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(float damage)
    {
        float finalDamage = damage * (1f - defense);
        currentHP -= Mathf.RoundToInt(finalDamage);

        if (currentHP <= 0)
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