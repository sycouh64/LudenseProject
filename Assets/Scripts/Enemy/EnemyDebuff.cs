using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;
using static PlayerStats;

public class EnemyDebuff : MonoBehaviour
{
    public static List<IDamageable> poisonDebuffEnemyList = new List<IDamageable>();
    public static List<IDamageable> burnDebuffEnemyList = new List<IDamageable>();
    public static List<IDamageable> frozenDebuffEnemyList = new List<IDamageable>();

    private void Awake()
    {
        StartCoroutine(ApplyDebuff());
    }

    private IEnumerator ApplyDebuff()
    {
        while (true)
        {
            PoisonDebuffApply();
            BurnDebuffApply();
            FrozenDebuffApply();
            yield return new WaitForSeconds(1);
        }
    }

    private void PoisonDebuffApply()
    {
        Debug.Log("독적용");
        foreach (var target in poisonDebuffEnemyList.ToList())
        {
            float poisonDamage = 10;
            
            // 최대체력의 10% 독 데미지
            target.TakeDamage(target.MaxHP * PlayerStats_Instance.CalculatePlayerDebuffSkillValue(SkillElement.Green, poisonDamage));

            // 죽었으면 리스트에서 제거
            if (target.IsDead)
                poisonDebuffEnemyList.Remove(target);
        }
    }
    private void BurnDebuffApply()
    {
        Debug.Log("화상적용");
        foreach (var target in burnDebuffEnemyList.ToList())
        {
            // 10의 화염 데미지
            float burnDamage = 10;
            target.TakeDamage(PlayerStats_Instance.CalculatePlayerDebuffSkillValue(SkillElement.Red, burnDamage));

            // 죽었으면 리스트에서 제거
            if (target.IsDead)
                burnDebuffEnemyList.Remove(target);
        }
    }
    private void FrozenDebuffApply()
    {
        Debug.Log("빙결적용");
        foreach (var target in frozenDebuffEnemyList.ToList())
        {
            // 현재이속의 50% 감소
            float slowSpeed = 0.5f;
            target.GetFrozenDebuff(target.CurrentSpeed * PlayerStats_Instance.CalculatePlayerDebuffSkillValue(SkillElement.Blue, slowSpeed));

            // 죽었으면 리스트에서 제거
            if (target.IsDead)
                frozenDebuffEnemyList.Remove(target);
        }
    }
}
public enum DebuffType
{
    Poison,
    Burn,
    Frozen
}
