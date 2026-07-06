using UnityEngine;

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
        // 만약 씬에 실수로 GameManager를 여러 개 배치했다면, 중복된 것은 파괴한다.
        if (SkillEnergyManager_Instance == null)
        {
            _Instance = this;

            // 씬이 바뀌어도 이 오브젝트가 파괴되지 않고 유지되도록 설정
            DontDestroyOnLoad(gameObject);
        }
        else if (SkillEnergyManager_Instance != this)
        {
            Destroy(gameObject);
        }

        greenEnergy = 100;
        redEnergy = 100;
        blueEnergy = 100;
    }
    [SerializeField] public float redEnergy;
    [SerializeField] public float greenEnergy;
    [SerializeField] public float blueEnergy;

    public void CalculateSkillEnergy(SkillList.Skill skill)
    {
        switch (skill.skillElement) 
        {
            case "red":
                redEnergy -= skill.skillValue;
                break;
            case "green":
                greenEnergy -= skill.skillValue;
                break;
            case "blue":
                blueEnergy -= skill.skillValue;
                break;
        }
    }

    // public void GetEnergy()
}
