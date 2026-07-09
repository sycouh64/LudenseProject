using UnityEngine;
using System.Collections;

public interface IDamageable : IHasHP
{
    bool IsDead { get; }
    void TakeDamage(float damage);
    IEnumerator GetFrozenDebuff(float speed);
    void AddDebuff(DebuffType debuff);
    void RemoveDebuff(DebuffType debuff);
}
