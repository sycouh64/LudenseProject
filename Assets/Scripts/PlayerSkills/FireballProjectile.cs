using UnityEngine;
using static SkillList;

public class FireballProjectile : DisposableProjectile
{
    
    public override void Init(Vector2 dir, Skill skill, float finalDmg)
    {
        finalDmg = finalDamage;
        direction = dir.normalized;
        damage = skill.skillDamage;
        projectileSpeed = skill.skillSpeed;
        skillDestroyTime = skill.skillTime;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        anim.SetInteger("fire", 1); 
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

