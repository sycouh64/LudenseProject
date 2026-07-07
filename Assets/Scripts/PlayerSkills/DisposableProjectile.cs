using UnityEngine;
using static SkillList;

public abstract class DisposableProjectile : MonoBehaviour
{
    [SerializeField] protected float projectileSpeed; // 값 받아오기
    [SerializeField] protected Vector2 direction;
    [SerializeField] protected Animator anim;
    [SerializeField] protected float skillDestroyTime;
    [SerializeField] protected Skill thisSkill;
    [SerializeField] protected float finalDamage;
    protected virtual void Awake()
    {   
        anim = GetComponent<Animator>();
        Destroy(gameObject, 10f);
    }

    public abstract void Init(Vector2 dir, Skill skill, float finalDamage); // SkillExecutor 에서 실행함

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out var target))
        {
            Debug.Log(finalDamage);
            target.TakeDamage(finalDamage);
            OnHit();
            Destroy(gameObject);
        }
    }
    // 충돌 시 추가 처리 — 필요하면 자식 클래스에서 오버라이드
    protected virtual void OnHit() { }
}
