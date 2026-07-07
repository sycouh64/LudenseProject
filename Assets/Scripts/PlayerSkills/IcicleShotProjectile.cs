using UnityEngine;
using static SkillList;

public class IcicleShotProjectile : DisposableProjectile
{
    // 파이어볼 발사 및 애니메이션 구현 스크립트

    public override void Init(Vector2 dir, Skill skill, float finalDmg) // SkillExecutor 에서 실행함
    {
        finalDamage = finalDmg;
        direction = dir.normalized; // 벡터 정규화
        projectileSpeed = skill.skillSpeed;
        skillDestroyTime = skill.skillTime;
        // 방향 벡터를 각도로 변환해서 오브젝트 회전
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        // anim.SetInteger("fire", 1); // 애니메이션 실행
    }

    void Update()
    {
        transform.Translate(Vector2.right * projectileSpeed * Time.deltaTime); // 지정 방향으로 이동
    }


    protected override void OnHit()
    {
        // animator.Play("LeafHit");
    }
}
