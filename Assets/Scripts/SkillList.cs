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


    public class FireBall : Skill
    {
        private SkillExecutor executor;

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

    public class Meteor : Skill
    {
        private SkillExecutor executor;

        public Meteor(SkillExecutor exec)
            : base("메테오", "fire", "attack", 80, 0)
        {
            executor = exec;
        }
        public override void Activate()
        {
            executor.Execute(this);
        }
    }

    public class IcicleShot : Skill
    {
        private SkillExecutor executor;

        public IcicleShot(SkillExecutor exec)
            : base("고드름발사", "water", "attack", 20, 0)
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

        public RisingVine(SkillExecutor exec)
            : base("솟아오르는덩쿨", "plant", "CC", 20, 0)
        {
            executor = exec;
        }
        public override void Activate()
        {
            executor.Execute(this);
        }
    }
    public class LeafStorm : Skill
    {
        private SkillExecutor executor;
        private Vector2 direction;

        public LeafStorm(SkillExecutor exec)
            : base("리프스톰", "plant", "attack", 20, 0)
        {
            executor = exec;
        }
        public override void Activate()
        {
            executor.Execute(this);
        }
    }
}
