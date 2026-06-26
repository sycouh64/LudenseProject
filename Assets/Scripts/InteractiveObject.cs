using UnityEngine;
using UnityEngine.InputSystem;

public class InteractiveObject : MonoBehaviour
{

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

    public void OnInteraction(InputValue value)
    {
        Debug.Log(123);
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= interactionRadius)
        {
            DoGreenInteraction();
        }
    }

    public void DoGreenInteraction()
    {
        Debug.Log(1);
        if (isSiksik == false) return;
        isSiksik = false;
        anim.SetInteger("siksik", 0);
    }
}
