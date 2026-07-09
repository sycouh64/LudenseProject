using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using static UnityEngine.GraphicsBuffer;
using static SkillEnergyManager;
using static SkillList;
using static PlayerStats;
public class ElementWalkerSkill : MonoBehaviour
{
    [SerializeField] protected float walkerSpeed;
    [SerializeField] protected float damage;
    [SerializeField] protected SkillElement skillElement;
    [SerializeField] protected float skillDestroyTime;
    protected static bool usingWalkerSkill = false;
    static StatModifier walkerModifier = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Init(Skill skill) // SkillExecutor 에서 실행함
    {
        if (walkerModifier != null && walkerModifier.Element == skill.SkillElement)
        {
            RemoveCurrentModifier();
            Debug.Log(usingWalkerSkill);
            usingWalkerSkill = false;
            return;
        }
        RemoveCurrentModifier();
        StopAllCoroutines();

        usingWalkerSkill = true;
        Debug.Log(skill.skillName);
        Debug.Log("신발장착");
        walkerModifier = new StatModifier(skill.skillDamage, ModifierType.Flat, 0, skill.skillName, skill.SkillElement);
        PlayerStats_Instance.AddModifier(walkerModifier);
        StartCoroutine(UseEnergy(skill));
    }

    private IEnumerator UseEnergy(Skill skill)
    {
        while (usingWalkerSkill == true)
        {
            SkillEnergyManager_Instance.CosumeSkillEnergy(skill);
            yield return new WaitForSeconds(1);
        }
    }

    private void RemoveCurrentModifier()
    {
        if (walkerModifier != null)
        {
            PlayerStats_Instance.RemoveModifier(walkerModifier);
            walkerModifier = null;
        }
    }
}
