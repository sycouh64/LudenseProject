using UnityEngine;

public class SkillEnergyManager : MonoBehaviour
{
    // 스킬 에너지 획득 스크립트

    public static float redEnergy;
    public static float greenEnergy;
    public static float blueEnergy;

    private void Start()
    {
        greenEnergy = 0;
    }
}
