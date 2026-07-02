using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using static PlayerWallCheck;

public class PlayerMove : MonoBehaviour
{
    // 플레이어 움직임 구현
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

        if (isWall == true) // 벽타기 구현
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

        if (isGrounded) // 땅에서 움직임
        {
            playerVel.x = inputVec.x * xMoveSpeed;
        }else if (isWall == false && inputVec.x != 0) // 공중에서 움직임
        { 
            playerVel.x = direction * xMoveSpeed;
        }
        
        if (jumpPressed)
        {
            if (isWall) // 벽인 상태에서 점프 시 튕겨져 나감
            {
                playerVel.x = playerVel.x + - direction * xMoveSpeed;
                playerVel.y = playerVel.y + jumpPower;
                jumpPressed = false;
            }else if (isGrounded) // 땅에서 점프
            {
                playerVel.y = playerVel.y + jumpPower;
                jumpPressed = false;
            }
        }

        rb.linearVelocity = new Vector2(playerVel.x, playerVel.y);
        jumpPressed = false;
    }


    public void NoGravity() // 중력 끄는 함수
    {
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // 위아래 속력 제거
    }
    void YesGravity() // 중력 키는 함수
    {
        rb.gravityScale = originalGravity;
    }

    public void OnMove(InputValue value) // inputVec에 Vector값 할당 및 inputVec가 0이 아니게 되며 이동 가능해짐.(inputVec가 -1 또는 1일 때만 움직이도록 구현)
    {
        inputVec = value.Get<Vector2>();
    }

    public void OnJump(InputValue value) // 점프
    {
        jumpPressed = true;
    }
}
