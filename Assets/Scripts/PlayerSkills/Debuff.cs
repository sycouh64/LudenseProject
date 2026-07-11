using UnityEngine;
using System.Collections;

public abstract class Debuff : MonoBehaviour
{
    public DebuffType debuffName;
    public float debuffDamage;
    public float debuffValue;
    public float debuffDuration;
    public Debuff(DebuffType name, float damage, float value, float duration)
    {
        debuffName = name;
        debuffDamage = damage;
        debuffValue = value;
        debuffDuration = duration;
    }

    public virtual void DebuffApply()
    {

    }
}

public enum DebuffType
{
    Poison,
    Burn,
    Frozen
}
