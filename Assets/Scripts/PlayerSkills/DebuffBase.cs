using UnityEngine;
using System.Collections;
using static PlayerStats;
using static UnityEngine.GraphicsBuffer;

public abstract class DebuffBase : MonoBehaviour
{
    protected float interval = 1f;
    protected EnemyScript target;
    protected DebuffType type;
    public float duration;

    public void Init(EnemyScript targetDamageable, float dur)
    {
        target = targetDamageable;
        duration = dur;
        StartCoroutine(TickDebuff());
    }

    private IEnumerator TickDebuff()
    {
        while (true)
        {
            if (duration > 0)
            {
                ApplyDebuff();
                duration -= 1;
            }else
            {
                RemoveDebuff();
            }
            yield return new WaitForSeconds(interval);
        }
    }

    // 자식 클래스에서 실제 디버프 효과 구현
    protected abstract void ApplyDebuff();
    protected abstract void RemoveDebuff();
}

public enum DebuffType
{
    Poison,
    Burn,
    Frozen
}
