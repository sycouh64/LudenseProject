using UnityEngine;

public class IcicleShotProjectile : MonoBehaviour
{
    // 파이어볼 발사 및 애니메이션 구현 스크립트

    [SerializeField] public float speed = 10f;
    [SerializeField] public float damage = 20f;
    private Vector2 direction;
    // private Animator anim;

    void Awake()
    {
        // anim = GetComponent<Animator>();
        Destroy(gameObject, 3f); // 5초 후 자동 소멸
    }

    public void Init(Vector2 dir, float dmg) // SkillExecutor 에서 실행함
    {
        direction = dir.normalized; // 벡터 정규화
        damage = dmg;
        // 방향 벡터를 각도로 변환해서 오브젝트 회전
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        // anim.SetInteger("fire", 1); // 애니메이션 실행
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime); // 지정 방향으로 이동
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) // 적과 충돌 시
        {
            other.GetComponent<EnemyScript>()?.TakeDamage(damage);
            speed = 0;
            Destroy(gameObject, 0.5f);
        }
    }
}
