using UnityEngine;
using UnityEngine.InputSystem;
using System;
using static SkillList;
using static EnemyList;
using static SkillArbiter;
using static PlayerElementManager;
using NUnit.Framework.Constraints;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
    // 스킬 사용 입력 스크립트

    private Enemy enemy_1;
    private EnemySpawnManager spawnManager;

    private List<Action> tabActionList;
    private int tabIndex = 0;


    void Start()
    {
        tabActionList = new List<Action>()
        {
            () => PlayerElementManager_Instance.ElementChange(1), // Red
            () => PlayerElementManager_Instance.ElementChange(2), // Green
            () => PlayerElementManager_Instance.ElementChange(3), // Blue
        };
        spawnManager = GetComponent<EnemySpawnManager>();
        enemy_1 = new Enemy_1(spawnManager);
    }
    public void OnSkill_1(InputValue value)
    {
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
    public void OnSkill_4(InputValue value)
    {
        SkillArbiter_Instance.SkillDecider(3);
    }

    public void OnEnemy(InputValue value)
    {
        enemy_1.Activate(new Vector2(1f, 0f));
    }

    public void OnTab(InputValue value)
    {
        if (!value.isPressed) return;

        tabActionList[tabIndex].Invoke();

        tabIndex++;
        if (tabIndex >= tabActionList.Count)
        {
            tabIndex = 0;
        }
    }


    //public void OnMap(InputValue value)
    //{ 

    //}
}
