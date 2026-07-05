using UnityEngine;

public class RisingVineProjectile : DisposableProjectile
{
    [SerializeField] private float lifetime = 2f;   // 지속 시간
    [SerializeField] private float riseTime = 0.5f; // 솟아오르는 시간


    public void Init(float dmg, float speed, float time)
    {
        damage = dmg;
        projectileSpeed = speed;
        skillDestroyTime = time;
        anim.SetInteger("fire", 1); // 애니메이션 실행
        Destroy(gameObject, lifetime);
    }

    protected override void OnHit()
    {
        // animator.Play("LeafHit");
    }
}
