using UnityEngine;
using static PlayerStats;

public class BurnDebuff : DebuffBase
{
    protected override void ApplyDebuff()
    {
        float burnDamage = 10;
        target.TakeDamage(PlayerStats_Instance.CalculatePlayerDebuffSkillValue(SkillElement.Red, burnDamage));
    }
    protected override void RemoveDebuff()
    {
        Destroy(this);
    }
}