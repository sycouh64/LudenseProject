using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;

public class SkillList : MonoBehaviour
{
    // 스킬 기본값 지정 스크립트

    public class Skill
    {
        public string skillName;
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

        public virtual void Activate() { }
    }

    //class IgniteWeapon : Skill
    //{
    //    public IgniteWeapon() : base("무기발화", "fire", "utility", 0, 5){ }

    //    protected override void Activate()
    //    {
            
    //    }
    //}

    public class FireBall : Skill
    {
        private SkillExecutor executor;
        private Vector2 direction;

        public FireBall(SkillExecutor exec)
            : base("파이어볼", "fire", "attack", 30, 0) 
        {
            executor = exec;
        }
        public override void Activate()
        {
            executor.Execute(this);
        }
    }

    public class RisingVine : Skill
    {
        private SkillExecutor executor;
        private Vector2 direction;

        public RisingVine(SkillExecutor exec)
            : base("솟아오르는덩쿨", "plant", "attack", 20, 0)
        {
            executor = exec;
        }
        public override void Activate()
        {
            executor.Execute(this);
        }
    }
}
