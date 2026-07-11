using UnityEngine;
using static SkillList;

public class WallSkillProjectile : MonoBehaviour
{
    [SerializeField] private float duration = 5f;       // 벽 지속 시간
    [SerializeField] private float knockbackForce = 10f; // 튕겨나가는 힘

    public void Init(Skill skill)
    {
        duration = skill.skillTime;
        knockbackForce = skill.skillValue;
        Destroy(gameObject, duration);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 적이 닿았을 때만 튕겨냄
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody2D enemyRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                // 벽에서 멀어지는 방향으로 튕겨냄
                Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;
                enemyRb.linearVelocity = Vector2.zero; // 기존 속도 초기화
                enemyRb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }
}
