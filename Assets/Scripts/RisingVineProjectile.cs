using UnityEngine;

public class RisingVineProjectile : MonoBehaviour
{
    private float damage;
    private Animator anim;

    [SerializeField] private float lifetime = 2f;   // 지속 시간
    [SerializeField] private float riseTime = 0.5f; // 솟아오르는 시간

    void Awake()
    {
        anim = GetComponent<Animator>();
        Destroy(gameObject, 2f); // 5초 후 자동 소멸
    }

    public void Init(float dmg)
    {
        damage = dmg;
        anim.SetInteger("fire", 1); // 애니메이션 실행
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyScript>()?.TakeDamage(damage);
        }
    }
}
