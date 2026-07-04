using UnityEngine;

public class EnemyHurtbox : MonoBehaviour
{
    public EnemyScript enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            enemy.TakeDamage(1);
        }
    }
}
