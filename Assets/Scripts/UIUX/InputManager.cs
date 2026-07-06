using UnityEngine;
using UnityEngine.InputSystem;
using static SkillList;
using static EnemyList;
using static SkillArbiter;
using static PlayerElementManager;
using NUnit.Framework.Constraints;

public class InputManager : MonoBehaviour
{
    // 스킬 사용 입력 스크립트

    private Enemy enemy_1;
    private EnemySpawnManager spawnManager;

    
    
    void Start()
    {
        spawnManager = GetComponent<EnemySpawnManager>();
        enemy_1 = new Enemy_1(spawnManager);
    }
    public void OnSkill_1(InputValue value)
    {
        Debug.Log("useSkill1");
        SkillArbiter_Instance.SkillDecider(0);
    }
    public void OnSkill_2(InputValue value)
    {
        SkillArbiter_Instance.SkillDecider(1);
    }
    public void OnSkill_3(InputValue value)
    {
        SkillArbiter_Instance.SkillDecider(2);
    }

    public void OnEnemy(InputValue value)
    {
        enemy_1.Activate(new Vector2(1f, 0f));
    }

    public void OnRedSkill(InputValue value)
    {
        PlayerElementManager_Instance.ElementChange(1);
    }
    public void OnGreenSkill(InputValue value)
    {
        PlayerElementManager_Instance.ElementChange(2);
    }
    public void OnBlueSkill(InputValue value)
    {
        PlayerElementManager_Instance.ElementChange(3);
    }
}
