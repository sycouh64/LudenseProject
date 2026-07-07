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
        // 만약 씬에 실수로 GameManager를 여러 개 배치했다면, 중복된 것은 파괴한다.
        if (PlayerStats_Instance == null)
        {
            _Instance = this;

            // 씬이 바뀌어도 이 오브젝트가 파괴되지 않고 유지되도록 설정
            DontDestroyOnLoad(gameObject);
        }
        else if (PlayerStats_Instance != this)
        {
            Destroy(gameObject);
        }
    }


    public float baseAttack = 10f;

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

    private IEnumerator RemoveAfterDuration(StatModifier modifier)
    {
        yield return new WaitForSeconds(modifier.duration);
        modifiers.Remove(modifier);
    }

}
