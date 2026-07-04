using UnityEngine;
using static SkillEnergyManager;

public class SkillExecutor : MonoBehaviour
{
    // 스킬 실행 스크립트

    [SerializeField] GameObject fireballPrefab; // 프리팹 연결
    [SerializeField] GameObject risingVinePrefab;
    public void Execute(SkillList.Skill skill)
    {
        switch (skill.skillName)
        {
            case "파이어볼":
                if (greenEnergy >= 1)
                {
                    SpawnFireball(skill.skillDamage);
                    greenEnergy -= 1;
                }
                break;
            case "무기발화":
                // ActivateIgniteWeapon();
                break;
            case "솟아오르는덩쿨":
                    SpawnRisingVine(skill.skillDamage);
                break;
        }
    }

    void SpawnFireball(float damage)
    {
        // 마우스 위치를 월드 좌표로 변환
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        // 캐릭터 → 마우스 방향 벡터 계산
        Vector2 direction = (mousePos - transform.position).normalized;

        // 파이어볼 생성
        var obj = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
        obj.GetComponent<FireballProjectile>().Init(direction, damage); // 파이어볼 스크립트의 Init 호출
    }

    void SpawnRisingVine(float damage)
    {
        // 마우스 X좌표를 월드 좌표로 변환
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        // 바닥 Y좌표 찾기 — Raycast로 바닥 감지
        RaycastHit2D hit = Physics2D.Raycast(
            new Vector2(mousePos.x, mousePos.y),
            Vector2.down,
            100f,
            LayerMask.GetMask("Ground") 
        );

        if (hit.collider != null)
        {
            Vector3 spawnPos = new Vector3(mousePos.x, hit.point.y, 0);
            var obj = Instantiate(risingVinePrefab, spawnPos, Quaternion.identity);
            obj.GetComponent<RisingVineProjectile>().Init(damage);
        }
    }
}
