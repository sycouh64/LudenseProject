using UnityEngine;

public abstract class DisposableProjectile : MonoBehaviour
{
    [SerializeField] protected float projectileSpeed; // 값 받아오기
    [SerializeField] protected float damage;
    [SerializeField] protected Vector2 direction;
    [SerializeField] protected Animator anim;
    [SerializeField] protected float skillDestroyTime;

    protected virtual void Awake()
    {   
        anim = GetComponent<Animator>();
        Destroy(gameObject, 10f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out var target))
        {
            float finalDamage = CalculateDamage();
            target.TakeDamage(finalDamage);
            OnHit();
            Destroy(gameObject);
        }
    }

    // 데미지 계산 — 필요하면 자식 클래스에서 오버라이드
    protected virtual float CalculateDamage()
    {
        return damage;
    }

    // 충돌 시 추가 처리 — 필요하면 자식 클래스에서 오버라이드
    protected virtual void OnHit() { }
}
