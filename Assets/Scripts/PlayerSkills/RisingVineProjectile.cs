using UnityEngine;

public class RisingVineProjectile : MonoBehaviour
{
    [SerializeField] private float lifetime = 2f;   // 지속 시간
    [SerializeField] private float riseTime = 0.5f; // 솟아오르는 시간

    [SerializeField] protected float projectileSpeed; // 값 받아오기
    [SerializeField] protected float damage;
    [SerializeField] protected Vector2 direction;
    [SerializeField] protected Animator anim;
    [SerializeField] protected float skillDestroyTime;
    protected void Awake()
    {
        anim = GetComponent<Animator>();
        Destroy(gameObject, 10f);
    }
    public void Init(float dmg, float speed, float time)
    {
        damage = dmg;
        projectileSpeed = speed;
        skillDestroyTime = time;
        anim.SetInteger("fire", 1); // 애니메이션 실행
        Destroy(gameObject, lifetime);
    }
    protected float CalculateDamage()
    {
        return damage;
    }
    protected void OnHit()
    {
        // animator.Play("LeafHit");
    }
}
