using UnityEngine;
using static SkillList;

public class LeafStormProjectile : DisposableProjectile
{
    // 리프스톰 발사 및 애니메이션 구현 스크립트

    void Update()
    {
        transform.Translate(Vector2.right * projectileSpeed * Time.deltaTime); // 지정 방향으로 이동
    }
    protected override void OnFire()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    protected override void OnHit()
    {
        // animator.Play("LeafHit");
    }
}
