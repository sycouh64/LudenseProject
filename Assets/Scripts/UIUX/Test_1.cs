using UnityEngine;

public class Test_1 : MonoBehaviour
{

    public int a = 1;
    // 모노싱글톤
    private static Test_1 _Instance;

    // 외부에서 접근할 통로 (get 프로퍼티)
    public static Test_1 Test_1_Instance
    {
        get
        {
            // 1. 인스턴스가 아직 없다면 씬에서 찾아본다.
            if (_Instance == null)
            {
                _Instance = FindFirstObjectByType<Test_1>();

                // 2. 씬에도 없다면, 하이어라키에 새로 오브젝트를 만든다.
                if (_Instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    _Instance = go.AddComponent<Test_1>();
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

    public void OnTest()
    {
        a += 1;
    }
}
