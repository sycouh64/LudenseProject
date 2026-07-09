using UnityEngine;
using static SkillList;
public class SkillEnergyManager : MonoBehaviour
{
    // 스킬 에너지 획득 스크립트

    // 모노싱글톤
    private static SkillEnergyManager _Instance;

    // 외부에서 접근할 통로 (get 프로퍼티)
    public static SkillEnergyManager SkillEnergyManager_Instance
    {
        get
        {
            // 1. 인스턴스가 아직 없다면 씬에서 찾아본다.
            if (_Instance == null)
            {
                _Instance = FindFirstObjectByType<SkillEnergyManager>();

                // 2. 씬에도 없다면, 하이어라키에 새로 오브젝트를 만든다.
                if (_Instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    _Instance = go.AddComponent<SkillEnergyManager>();
                }
            }
            return _Instance;
        }
    }

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

        greenEnergy = 100;
        redEnergy = 100;
        blueEnergy = 100;
    }
    [SerializeField] public float redEnergy;
    [SerializeField] public float greenEnergy;
    [SerializeField] public float blueEnergy;

    public void CosumeSkillEnergy(Skill skill)
    {
        switch (skill.SkillElement) 
        {
            case SkillElement.Red:
                redEnergy -= skill.skillValue;
                break;
            case SkillElement.Green:
                greenEnergy -= skill.skillValue;
                break;
            case SkillElement.Blue:
                blueEnergy -= skill.skillValue;
                break;
        }
    }

    // public void GetEnergy()
}
