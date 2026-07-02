using UnityEngine;
using static SkillEnergyManager;

public class SkillExecutor : MonoBehaviour
{
    // 스킬 실행 스크립트

    [SerializeField] GameObject fireballPrefab; // 프리팹 연결

    public void Execute(SkillList.Skill skill, Vector2 fireDirection)
    {
        switch (skill.skillName)
        {
            case "파이어볼":
                if (greenEnergy >= 1)
                {
                    SpawnFireball(fireDirection, skill.skillDamage);
                    greenEnergy -= 1;
                }
                
                break;
            case "무기발화":
                // ActivateIgniteWeapon();
                break;
        }
    }

    void SpawnFireball(Vector2 dir, float damage)
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
}
