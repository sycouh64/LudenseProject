using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using static PlayerWallCheck;

public class PlayerMove : MonoBehaviour
{
    public float xMoveSpeed = 5f;
    public float yMoveSpeed = 3f;

    public float jumpPower = 20f;
    public float originalGravity;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    public static Vector2 inputVec;
    private bool isGrounded;
    public bool jumpPressed;
    void Awake()
    {
        direction = 1;
        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;
    }

    void Update()
    {

        if (isWall == true)
        {
            NoGravity();
            rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocityY + inputVec.y * yMoveSpeed);
            
        }else
        {
            YesGravity();
            rb.transform.localScale = new Vector3(direction, 1, 1);
            
        }


        // 바닥 체크
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

    }

    private void FixedUpdate()
    {
        Vector2 playerVel = rb.linearVelocity;

        if (isGrounded) 
        {
            playerVel.x = inputVec.x * xMoveSpeed;
        }else if (isWall == false && inputVec.x != 0) { 
            playerVel.x = direction * xMoveSpeed;
        }
        
        if (jumpPressed)
        {
            if (isWall)
            {
                playerVel.x = playerVel.x + - direction * xMoveSpeed;
                playerVel.y = playerVel.y + jumpPower;
                jumpPressed = false;
            }else if (isGrounded) {
                playerVel.y = playerVel.y + jumpPower;
                jumpPressed = false;
            }
        }

        rb.linearVelocity = new Vector2(playerVel.x, playerVel.y);
        jumpPressed = false;
    }


    public void NoGravity()
    {
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // 위아래 속력 제거
    }
    void YesGravity()
    {
        rb.gravityScale = originalGravity;
    }

    public void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        jumpPressed = true;
    }
}
