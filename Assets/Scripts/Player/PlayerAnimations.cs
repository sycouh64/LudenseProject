using UnityEngine;
using static PlayerMove;
using UnityEngine.InputSystem;

public class PlayerAnimations : MonoBehaviour
{
    // 구현안됨
    private Rigidbody2D rb;
    private Animator anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        {
            anim.SetFloat("MoveX", Mathf.Abs(inputVec.x));
        }
    }
}
