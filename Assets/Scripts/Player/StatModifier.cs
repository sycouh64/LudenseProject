using UnityEngine;

public class StatModifier
{
    public float value;
    public ModifierType type;
    public float duration;
    public string source;
    public SkillElement Element;  // 어떤 속성에 적용되는지

    public StatModifier(float value_, ModifierType type_, float duration_, string source_, SkillElement element_ = SkillElement.All)
    {
        value = value_;
        type = type_;
        duration = duration_;
        source = source_;
        Element = element_;
    }
}


public enum ModifierType
{
    Flat, // 고정값
    Percent // 퍼센트
}


