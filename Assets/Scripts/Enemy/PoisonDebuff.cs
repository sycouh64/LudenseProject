using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static PlayerStats;

public class PoisonDebuff : DebuffBase
{
    protected override void ApplyDebuff()
    {
        float poisonDamage = 10;

        // 최대체력의 10% 독 데미지
        target.TakeDamage(target.MaxHP * PlayerStats_Instance.CalculatePlayerDebuffSkillValue(SkillElement.Green, poisonDamage));
    }

    protected override void RemoveDebuff()
    {
        Destroy(this);
    }
}
