using UnityEngine;
using UnityEngine.InputSystem;
using static SkillEnergyManager;

public class InteractiveObject : MonoBehaviour
{
    // 상호작용 가능한 오브젝트 구현 스크립트
    private Animator anim;
    public Transform interactivePoint;
    public Transform player;
    public float interactionRadius = 10f;
    public bool isSiksik;

    private void Awake()
    {
        isSiksik = true;
        anim = GetComponent<Animator>();
        anim.SetInteger("siksik", 1);
    }

    public void OnInteraction(InputValue value) // F키를 눌렀을 때
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position); // 현재 상호작용 오브젝트와 플레이어의 거리

        if (distance <= interactionRadius)
        {
            if (isSiksik == true)
            {
                DoGreenInteraction();
            }
        }
    }

    public void DoGreenInteraction() // 에너지 획득
    {
        SkillEnergyManager_Instance.greenEnergy += 10;
        if (isSiksik == false) return;
        isSiksik = false;
        anim.SetInteger("siksik", 0);
    }
}
