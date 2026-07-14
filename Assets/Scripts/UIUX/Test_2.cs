using UnityEngine;
using static Test_1;

public class Test_2 : MonoBehaviour
{
    Test_1 test_1 = new Test_1();
    private void Awake()
    {
        Test_1_Instance.a = 1;
        Test_1_Instance.OnTest();
    }


}
