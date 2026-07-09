using UnityEngine;
using System.Collections.Generic;
using System;
using static SkillList;
using static UnityEditor.Progress;

public class SkillSlotAllocater : MonoBehaviour
{
    // 모노싱글톤
    private static SkillSlotAllocater _Instance;

    // 외부에서 접근할 통로 (get 프로퍼티)
    public static SkillSlotAllocater SkillSlotAllocater_Instance
    {
        get
        {
            // 1. 인스턴스가 아직 없다면 씬에서 찾아본다.
            if (_Instance == null)
            {
                _Instance = FindFirstObjectByType<SkillSlotAllocater>();

                // 2. 씬에도 없다면, 하이어라키에 새로 오브젝트를 만든다.
                if (_Instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    _Instance = go.AddComponent<SkillSlotAllocater>();
                }
            }
            return _Instance;
        }
    }

    // 유니티 초기화 함수

    private void Awake()
    {
        if (_Instance != null && _Instance != this)
        {
            // 이미 인스턴스가 존재하면 자신을 파괴
            Destroy(gameObject);
            return;
        }

        _Instance = this;
        DontDestroyOnLoad(gameObject);

        // 스킬 슬롯 스킬 할당 코드
        //redSkill
        redSkillSlot[0] = skillList[0];
        redSkillSlot[1] = skillList[1];
        redSkillSlot[2] = skillList[5];
        redSkillSlot[3] = skillList[8];
        //greenSkill
        greenSkillSlot[0] = skillList[2];
        greenSkillSlot[1] = skillList[3];
        greenSkillSlot[2] = skillList[6];
        greenSkillSlot[3] = skillList[9];
        //blueSkill
        blueSkillSlot[0] = skillList[4];
        blueSkillSlot[1] = skillList[7];
        blueSkillSlot[2] = skillList[10];
    }
    public static Skill[] redSkillSlot = new Skill[4];
    public static Skill[] greenSkillSlot = new Skill[4];
    public static Skill[] blueSkillSlot = new Skill[4];

}
