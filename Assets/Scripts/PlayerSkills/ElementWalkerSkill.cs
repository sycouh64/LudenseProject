using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using static UnityEngine.GraphicsBuffer;
using static SkillEnergyManager;
using static SkillList;
using static PlayerStats;
public  class ElementWalkerSkill : MonoBehaviour
{
    [SerializeField] protected float walkerSpeed;
    [SerializeField] protected float damage;
    [SerializeField] protected SkillElement skillElement;
    [SerializeField] protected float skillDestroyTime;
    protected static bool usingWalkerSkill = false;
    protected PlayerStats playerStats;
    StatModifier walkerModifier = new StatModifier(0, ModifierType.Flat, 0, "null", 0);
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public IEnumerator Init(Skill skill) // SkillExecutor 에서 실행함
    {
        usingWalkerSkill = true;
        Debug.Log("신발장착");
        walkerModifier = new StatModifier(skill.skillDamage, ModifierType.Flat, 0, skill.skillName, skill.SkillElement);
        playerStats.AddModifier(walkerModifier);
        while (usingWalkerSkill == true)
        {
            UseEnergy(skill);
            yield return new WaitForSeconds(1);
        }
    }

    private void UseEnergy(Skill skill)
    {
        SkillEnergyManager_Instance.CalculateSkillEnergy(skill);
    }

}
