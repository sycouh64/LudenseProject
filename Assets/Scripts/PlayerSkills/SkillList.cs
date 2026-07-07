using JetBrains.Annotations;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;
using static SkillExecutor;
using static SkillList;

public enum SkillElement
{
    All,    // 전체 적용
    Red,
    Green,
    Blue,
    None,
}
public enum SkillType
{
    None,
    Attack,
    Utility,
    CC,
}
public class SkillList : MonoBehaviour
{
    // 스킬 기본값 지정 스크립트

    public class Skill
    {
        public string skillName;
        public SkillElement SkillElement;
        public SkillType skillType;
        public float skillDamage;
        public float skillValue;
        public float skillSpeed;
        public float skillTime;
        public Skill(string name, SkillElement element, SkillType type, float damage, float value, float speed, float time)
        {
            skillName = name;
            SkillElement = element;
            skillType = type;
            skillDamage = damage;
            skillValue = value;
            skillSpeed = speed;
            skillTime = time;
        }
    }

    public static List<Skill> skillList = new List<Skill>()
    {
        new FireBall(),
        new Meteor(),
        new RisingVine(),
        new LeafStorm(),
        new IcicleShot(),
    };

    public class FireBall : Skill
    {
        public FireBall() : base("파이어볼", SkillElement.Red, SkillType.Attack, 15, 0, 10, 5) { }
    }

    public class Meteor : Skill
    {
        public Meteor() : base("메테오", SkillElement.Red, SkillType.Attack, 50, 0, 10, 10) { }
    }

    public class IcicleShot : Skill
    {
        public IcicleShot() : base("고드름발사", SkillElement.Blue, SkillType.Attack, 12, 0, 30, 5) { }
    }
    public class RisingVine : Skill
    {
        public RisingVine() : base("솟아오르는덩쿨", SkillElement.Green, SkillType.CC, 10, 0, 30, 5) { }
    }
    public class LeafStorm : Skill
    {
        public LeafStorm() : base("리프스톰", SkillElement.Green, SkillType.Attack, 10, 0, 20, 5) { }
    }
    public class FireWallker : Skill
    {
        public FireWallker() : base("불의걸음", SkillElement.Red, SkillType.Utility, 100, 0, 20, 5) { }
    }
    public class NatureWalker : Skill
        {
            public NatureWalker() : base("자연의걸음", SkillElement.Green, SkillType.Utility, 100, 0, 20, 5) { }
        }

    public class FrozenWalker : Skill
    {
        public FrozenWalker() : base("차가운걸음", SkillElement.Green, SkillType.Utility, 100, 0, 20, 5) { }
    }
    
}
