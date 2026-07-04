using UnityEngine;

public class EnemyContactDamage : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float damageInterval = 1f; // 연속 데미지 방지용 쿨타임
    private float lastDamageTime = -Mathf.Infinity;

    void OnTriggerEnter2D(Collider2D other)
    {
        TryDamagePlayer(other);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        TryDamagePlayer(other);
    }

    void TryDamagePlayer(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (Time.time - lastDamageTime < damageInterval) return;

        lastDamageTime = Time.time;
        other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
    }
}