using UnityEngine;

public class StatModifier
{
    public float Value;
    public ModifierType Type;
    public float Duration;
    public string Source;
    public SkillElement Element;  // 어떤 속성에 적용되는지

    public StatModifier(float value, ModifierType type, float duration, string source, SkillElement element = SkillElement.All)
    {
        Value = value;
        Type = type;
        Duration = duration;
        Source = source;
        Element = element;
    }
}


public enum ModifierType
{
    Flat, // 고정값
    Percent // 퍼센트
}


