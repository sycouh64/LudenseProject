using System;
using Unity.VisualScripting;
using UnityEngine;
using static SkillEnergyManager;

public class PlayerElementManager : MonoBehaviour
{
    // 모노싱글톤
    private static PlayerElementManager _Instance;

    // 외부에서 접근할 통로 (get 프로퍼티)
    public static PlayerElementManager PlayerElementManager_Instance
    {
        get
        {
            // 1. 인스턴스가 아직 없다면 씬에서 찾아본다.
            if (_Instance == null)
            {
                _Instance = FindFirstObjectByType<PlayerElementManager>();

                // 2. 씬에도 없다면, 하이어라키에 새로 오브젝트를 만든다.
                if (_Instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    _Instance = go.AddComponent<PlayerElementManager>();
                }
            }
            return _Instance;
        }
    }

    // 유니티 초기화 함수
    private void Awake()
    {
        // 만약 씬에 실수로 GameManager를 여러 개 배치했다면, 중복된 것은 파괴한다.
        if (PlayerElementManager_Instance == null)
        {
            _Instance = this;

            // 씬이 바뀌어도 이 오브젝트가 파괴되지 않고 유지되도록 설정
            DontDestroyOnLoad(gameObject);
        }
        else if (PlayerElementManager_Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public PlayerElement playerCurrentElement;

    public enum PlayerElement
    {
        None = 0,
        Red = 1,
        Green = 2,
        Blue = 3,
    }

    public void ElementChange(int num)
    {
        switch (num)
        {
            case 1:
                Debug.Log("case1");
                if (SkillEnergyManager_Instance.redEnergy > 0) playerCurrentElement = PlayerElement.Red;
                break;
            case 2:
                Debug.Log("case2");
                if (SkillEnergyManager_Instance.greenEnergy > 0) playerCurrentElement = PlayerElement.Green;
                break;  
            case 3:
                Debug.Log("case3");
                if (SkillEnergyManager_Instance.blueEnergy > 0) playerCurrentElement = PlayerElement.Blue;
                break;
            default:
                playerCurrentElement = PlayerElement.None;
                break;
        }
    }
}
