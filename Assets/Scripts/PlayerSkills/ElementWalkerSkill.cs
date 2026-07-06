using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using static UnityEngine.GraphicsBuffer;
using static SkillEnergyManager;
using static SkillList;

public  class ElementWalkerSkill : MonoBehaviour
{
    [SerializeField] protected float walkerSpeed;
    [SerializeField] protected float damage;
    [SerializeField] protected string skillElement;
    [SerializeField] protected float skillDestroyTime;
    protected bool usingWalkerSkill = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        
    }
    public IEnumerator Init(Skill skill) // SkillExecutor 에서 실행함
    {
        usingWalkerSkill = true;
        damage = skill.skillDamage;
        walkerSpeed = skill.skillSpeed;
        skillDestroyTime = skill.skillTime;
        skillElement = skill.skillElement;

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
