using UnityEngine;
using static SkillList;

public class MeteorProjectile : DisposableProjectile
{
    [SerializeField] private float speed = 5f;

    private Vector3 startPos;
    private Vector3 targetPos;
    private float journeyLength;    // 전체 이동 거리
    private float distanceCovered;  // 현재까지 이동한 거리

    [SerializeField] private Vector3 startScale = new Vector3(0.3f, 0.3f, 1f); // 시작 크기
    [SerializeField] private Vector3 endScale = new Vector3(10f, 10f, 1f);        // 도착 크기


    public override void Init(Vector2 dir, Skill skill, float finalDmg)
    {
        finalDamage = finalDmg;
        // 마우스 위치 (목표 지점)
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        
        startPos = dir;
        targetPos = mousePos;
        projectileSpeed = skill.skillSpeed;
        skillDestroyTime = skill.skillTime;

        journeyLength = Vector3.Distance(startPos, targetPos);
        transform.localScale = startScale;

        // 회전 적용 없이 방향 벡터만 저장
        direction = (targetPos - startPos).normalized;

        // anim.Play("MeteorFly");
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        distanceCovered = Vector3.Distance(startPos, transform.position);
        float progress = Mathf.Clamp01(distanceCovered / journeyLength);
        transform.localScale = Vector3.Lerp(startScale, endScale, progress);
    }

    protected override void OnHit()
    {
        // animator.Play("LeafHit");
    }

}
