using UnityEngine;
using static EnemyList;
using static SkillList;
using static SkillEnergyManager;
using static SkillExecutor;
using static PlayerElementManager;
using static SkillSlotAllocater;
using System;

public class SkillArbiter : MonoBehaviour 
{
    // 모노싱글톤
    private static SkillArbiter _Instance;
    // 외부에서 접근할 통로 (get 프로퍼티)
    public static SkillArbiter SkillArbiter_Instance
    {
        get
        {
            // 1. 인스턴스가 아직 없다면 씬에서 찾아본다.
            if (_Instance == null)
            {
                _Instance = FindFirstObjectByType<SkillArbiter>();

                // 2. 씬에도 없다면, 하이어라키에 새로 오브젝트를 만든다.
                if (_Instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    _Instance = go.AddComponent<SkillArbiter>();
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
    }


    private Enemy enemy_1;
    private EnemySpawnManager spawnManager;

    void Start()
    {
        // spawnManager = GetComponent<EnemySpawnManager>();
        enemy_1 = new Enemy_1(spawnManager);
    }

    public void SkillCast(Skill skill)
    {
        if (SkillCostCaculater(skill) == false) return;
        SkillExecutor_Instance.Execute(skill);
    }
    public void SkillDecider(int num)
    {
        switch (PlayerElementManager_Instance.playerCurrentElement)
        {
            case PlayerElement.Red:
                SkillCast(redSkillSlot[num]);
                break;
            case PlayerElement.Green:
                SkillCast(greenSkillSlot[num]);
                break;
            case PlayerElement.Blue:
                SkillCast(blueSkillSlot[num]);
                break;
        }
    }
    public bool SkillCostCaculater(Skill skill) // 에너지보다 스킬 코스트가 작을 때 스킬 사용 (true 리턴)
    {
        switch (skill.SkillElement)
        {
            case SkillElement.Red:
                if (SkillEnergyManager_Instance.redEnergy < skill.skillValue)
                {
                    return false;
                }
                else return true;
            case SkillElement.Blue:
                if (SkillEnergyManager_Instance.greenEnergy < skill.skillValue)
                {
                    return false;
                }
                else return true;
            case SkillElement.Green:
                if (SkillEnergyManager_Instance.blueEnergy < skill.skillValue)
                {
                    return false;
                }
                else return true;
            default:
                return false;
        }
    }
}
