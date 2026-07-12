using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;

public class PlayerStats : MonoBehaviour
{
    // 모노싱글톤
    private static PlayerStats _Instance;

    // 외부에서 접근할 통로 (get 프로퍼티)
    public static PlayerStats PlayerStats_Instance
    {
        get
        {
            // 1. 인스턴스가 아직 없다면 씬에서 찾아본다.
            if (_Instance == null)
            {
                _Instance = FindFirstObjectByType<PlayerStats>();

                // 2. 씬에도 없다면, 하이어라키에 새로 오브젝트를 만든다.
                if (_Instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    _Instance = go.AddComponent<PlayerStats>();
                }
            }
            return _Instance;
        }
    }

    // 유니티 초기화 함수
    private void Awake()
    {
        if (_Instance != null && _Instance != this)
        {
            // 이미 인스턴스가 존재하면 자신을 파괴
            Destroy(gameObject);
            return;
        }

        _Instance = this;
        DontDestroyOnLoad(gameObject);

    }


    private float baseAttack = 0;

    public static List<StatModifier> modifiers = new List<StatModifier>();
    // 버프/디버프 추가
    public void AddModifier(StatModifier modifier)
    {
        modifiers.Add(modifier);
        if (modifier.duration > 0)
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
        foreach (var mod in applicableModifiers.Where(m => m.type == ModifierType.Flat))
            total += mod.value;
        // Percent 나중에
        foreach (var mod in applicableModifiers.Where(m => m.type == ModifierType.Percent))
            total *= (1 + mod.value / 100f);
        return total;
    }

    public float CalculatePlayerDebuffSkillValue(SkillElement DebuffElement, float value)
    {

        float total = value;
        var applicableModifiers = modifiers.Where(m =>
            m.Element == DebuffElement ||
            m.Element == SkillElement.All
        ).ToList();

        // Flat 먼저
        foreach (var mod in applicableModifiers.Where(m => m.type == ModifierType.Flat))
            total += mod.value * 0.1f;
        // Percent 나중에
        foreach (var mod in applicableModifiers.Where(m => m.type == ModifierType.Percent))
            total *= (1 + mod.value / 100f);
        return total;
    }

    private IEnumerator RemoveAfterDuration(StatModifier modifier)
    {
        yield return new WaitForSeconds(modifier.duration);
        modifiers.Remove(modifier);
    }
    public void RemoveModifier(StatModifier modifier)
    {
        modifiers.Remove(modifier);
    }
}
