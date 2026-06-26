using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;
using static AttackingProperties;

public class SkillList : MonoBehaviour
{

    public class Skill
    {
        protected string skillName = "dtq";
        public string skillProperty;
        public string skillType;
        public float skillDamage;
        public float skillValue;
        public Skill(string name, string property, string type, float damage, float value)
        {
            skillName = name;
            skillProperty = property;
            skillType = type;
            skillDamage = damage;
            skillValue = value;
        }

        protected virtual void Activate() { }
    }

    class IgniteWeapon : Skill
    {
        public IgniteWeapon() : base("무기발화", "fire", "utility", 0, 5){ }

        protected override void Activate()
        {
            
        }
    }

    class FireBall : Skill
    {
        public FireBall() : base("파이어볼", "fire", "attack", 20, 0) { }
        protected override void Activate()
        {
            Debug.Log(777);
        }
    }
}
