using UnityEngine;
using static PlayerMove;
using UnityEngine.InputSystem;
using static UnityEngine.UI.Image;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class PlayerWallCheck : MonoBehaviour
{
    private Rigidbody2D rb;
    public static bool isWall;
    float wallCheckDistance = 0.85f;
    public LayerMask wallLayer;
    public static float direction;
    RaycastHit hit;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (inputVec.x > 0)
        {
            direction = 1;
        }else if (inputVec.x < 0)
        {
            direction = -1;
        }

        isWall = Physics2D.Raycast(transform.position, Vector2.right * Mathf.Abs(transform.localScale.x) * direction, wallCheckDistance, wallLayer);
        Debug.DrawRay(transform.position, Vector2.right * direction * wallCheckDistance, Color.red);
    }
}
