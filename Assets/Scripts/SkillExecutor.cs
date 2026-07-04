using UnityEngine;
using System.Collections;
using static SkillEnergyManager;

public class SkillExecutor : MonoBehaviour
{
    // 스킬 실행 스크립트

    [SerializeField] GameObject fireballPrefab; // 프리팹 연결
    [SerializeField] GameObject risingVinePrefab;
    [SerializeField] GameObject icicleShotPrefab;
    [SerializeField] GameObject leafStormPrefab;
    private int leafCount = 3; // 잎 개수
    private float leafInterval = 0.1f; // 잎 간의 시간 텀 (초)
    [SerializeField] private GameObject meteorPrefab;
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
            case "메테오":
                SpawnMeteor(skill.skillDamage);
                break;
            case "무기발화":
                // ActivateIgniteWeapon();
                break;
            case "고드름발사":
                StartCoroutine(SpawnIcicleShot(skill.skillDamage));
                break;
            case "솟아오르는덩쿨":
                SpawnRisingVine(skill.skillDamage);
                break;
            case "리프스톰":
                StartCoroutine(SpawnLeafStorm(skill.skillDamage));
                break ;
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

    void SpawnMeteor(float damage) 
    {
        // 마우스 위치 (목표 지점)
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        // 시작 위치: 목표 지점 기준 왼쪽 위
        Vector3 startOffset = new Vector3(-8f, 6f, 0);  // 좌측 상단 거리 조절
        Vector3 startPos = mousePos + startOffset;

        var obj = Instantiate(meteorPrefab, startPos, Quaternion.identity);
        obj.GetComponent<MeteorProjectile>().Init(startPos, mousePos, damage);
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

    private IEnumerator SpawnLeafStorm(float damage)
    {
        // 마우스 방향 계산 (파이어볼과 동일)
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector2 baseDirection = (mousePos - transform.position).normalized;

        for (int i = 0; i < leafCount; i++)
        {
            float yOffset = 0;
            yOffset = (i % 2) * 0.2f;
            Vector3 spawnPos = transform.position + new Vector3(0, yOffset, 0);

            var obj = Instantiate(leafStormPrefab, spawnPos, Quaternion.identity);
            obj.GetComponent<LeafStormProjectile>().Init(baseDirection, damage);

            // 다음 잎까지 대기
            yield return new WaitForSeconds(leafInterval);
        }
    }
    private IEnumerator SpawnIcicleShot(float damage)
    {
        // 마우스 방향 계산 (파이어볼과 동일)
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector2 baseDirection = (mousePos - transform.position).normalized;

        for (int i = 0; i < leafCount; i++)
        {
            float yOffset = 0;
            yOffset = (i % 2) * 0.2f;
            Vector3 spawnPos = transform.position + new Vector3(0, yOffset, 0);

            var obj = Instantiate(icicleShotPrefab, spawnPos, Quaternion.identity);
            obj.GetComponent<IcicleShotProjectile>().Init(baseDirection, damage);

            // 다음 잎까지 대기
            yield return new WaitForSeconds(leafInterval);
        }
    }
}
