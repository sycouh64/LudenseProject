using JetBrains.Annotations;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;
using static SkillExecutor;
using static SkillList;

public class SkillList : MonoBehaviour
{
    // 스킬 기본값 지정 스크립트

    public class Skill
    {
        public string skillName;
        public string skillElement;
        public string skillType;
        public float skillDamage;
        public float skillValue;
        public float skillSpeed;
        public float skillTime;
        public Skill(string name, string element, string type, float damage, float value, float speed, float time)
        {
            skillName = name;
            skillElement = element;
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
        public FireBall() : base("파이어볼", "red", "attack", 15, 0, 10, 5) { }
    }

    public class Meteor : Skill
    {
        public Meteor() : base("메테오", "red", "attack", 50, 0, 10, 10) { }
    }

    public class IcicleShot : Skill
    {
        public IcicleShot() : base("고드름발사", "blue", "attack", 12, 0, 30, 5) { }
    }
    public class RisingVine : Skill
    {
        public RisingVine() : base("솟아오르는덩쿨", "green", "CC", 10, 0, 30, 5) { }
    }
    public class LeafStorm : Skill
    {
        public LeafStorm() : base("리프스톰", "green", "attack", 10, 0, 20, 5) { }
    }
}
