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
        // 만약 씬에 실수로 GameManager를 여러 개 배치했다면, 중복된 것은 파괴한다.
        if (_Instance == null)
        {
            _Instance = this;

            // 씬이 바뀌어도 이 오브젝트가 파괴되지 않고 유지되도록 설정
            DontDestroyOnLoad(gameObject);
        }
        else if (_Instance != this)
        {
            Destroy(gameObject);
        }
        
        //redSkill
        redSkillSlot[0] = skillList[0];
        redSkillSlot[1] = skillList[1];
        //greenSkill
        greenSkillSlot[0] = skillList[2];
        greenSkillSlot[1] = skillList[3];
        //blueSkill
        blueSkillSlot[0] = skillList[4];
    }

    public static Skill[] redSkillSlot = new Skill[3];
    public static Skill[] greenSkillSlot = new Skill[3];
    public static Skill[] blueSkillSlot = new Skill[3];

}
