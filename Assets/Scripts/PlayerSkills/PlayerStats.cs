using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;

public class PlayerStats : MonoBehaviour
{

    public float baseAttack = 10f;

    public static List<StatModifier> modifiers = new List<StatModifier>();
    // 버프/디버프 추가
    public void AddModifier(StatModifier modifier)
    {
        modifiers.Add(modifier);
        if (modifier.Duration > 0)
            StartCoroutine(RemoveAfterDuration(modifier));
    }

    // 최종 데미지 계산
    public float CalculateDamage(SkillList.Skill skill)
    {
        
        float total = baseAttack + skill.skillDamage;

        var applicableModifiers = modifiers.Where(m =>
            m.Element == skill.SkillElement || 
            m.Element == SkillElement.All
        ).ToList();

        // Flat 먼저
        foreach (var mod in applicableModifiers.Where(m => m.Type == ModifierType.Flat))
            total += mod.Value;

        // Percent 나중에
        foreach (var mod in applicableModifiers.Where(m => m.Type == ModifierType.Percent))
            total *= (1 + mod.Value / 100f);

        return total;
    }

    private IEnumerator RemoveAfterDuration(StatModifier modifier)
    {
        yield return new WaitForSeconds(modifier.Duration);
        modifiers.Remove(modifier);
    }

}
