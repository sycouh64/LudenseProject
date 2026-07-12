using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static PlayerStats;

public class FrozenDebuff : DebuffBase
{
    protected override void ApplyDebuff()
    {
        float slowSpeed = 0.5f;
        target.currentSpeed = target.originalSpeed * PlayerStats_Instance.CalculatePlayerDebuffSkillValue(SkillElement.Blue, slowSpeed);
    }
    public void FrozenBreak()
    {
        target.currentHP -= 100;
    }
    protected override void RemoveDebuff()
    {
        target.currentSpeed = target.originalSpeed;
        Destroy(this);
    }
}
