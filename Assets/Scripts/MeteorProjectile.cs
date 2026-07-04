using UnityEngine;

public class MeteorProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private float damage;
    private Vector3 startPos;
    private Vector3 targetPos;
    private float journeyLength;    // 전체 이동 거리
    private float distanceCovered;  // 현재까지 이동한 거리
    private Vector2 direction; // 방향만 저장

    [SerializeField] private Vector3 startScale = new Vector3(0.3f, 0.3f, 1f); // 시작 크기
    [SerializeField] private Vector3 endScale = new Vector3(10f, 10f, 1f);        // 도착 크기

    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        Destroy(gameObject, 10f);
    }

    public void Init(Vector3 start, Vector3 target, float dmg)
    {
        startPos = start;
        targetPos = target;
        damage = dmg;

        journeyLength = Vector3.Distance(startPos, targetPos);
        transform.localScale = startScale;

        // 회전 적용 없이 방향 벡터만 저장
        direction = (targetPos - startPos).normalized;

        anim.Play("MeteorFly");
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        distanceCovered = Vector3.Distance(startPos, transform.position);
        float progress = Mathf.Clamp01(distanceCovered / journeyLength);
        transform.localScale = Vector3.Lerp(startScale, endScale, progress);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyScript>()?.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
