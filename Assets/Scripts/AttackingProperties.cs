using UnityEngine;

public class AttackingProperties : MonoBehaviour
{
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
    }

    public class SkillTable
    {
        public static Skill fire_1 = new Skill("화염구", "fire", "attack", 20, 2);
        public static Skill fire_2 = new Skill("무기 발화", "fire", "utility", 5, 0);
        public static Skill fire_3 = new Skill("불의 장벽", "fire", "cc", 3, 5);
        public static Skill water_1 = new Skill("얼음 송곳", "water", "attack", 20, 3);
        public static Skill water_2 = new Skill("물의 걸음", "water", "utility", 0, 20);
        public static Skill water_3 = new Skill("해일", "water", "cc", 30, 10);
    }


}
