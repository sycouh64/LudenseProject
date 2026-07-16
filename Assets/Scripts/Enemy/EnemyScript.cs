using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;


public abstract class EnemyScript : MonoBehaviour, IHasHP
{
    [SerializeField] public float originalSpeed = 1f;
    [SerializeField] public float currentSpeed;
    private float baseDamage;
    [SerializeField] public float finalDamage;
    [SerializeField] public float attackDamage = 10f;

    [SerializeField] private float maxHP = 100f;
    public float currentHP = 100f;

    public float OriginalSpeed => originalSpeed;
    public float CurrentSpeed => currentSpeed;
    public float CurrentHP => currentHP;
    public float MaxHP => maxHP;

    private Animator anim;
    protected Rigidbody2D rb;
    protected Transform playerTransform;
    private Vector3 originalLocalScale;

    [Header("움직임")]
    [SerializeField] protected float jumpForce = 8f;
    [SerializeField] protected LayerMask groundLayer;
    private float decisionTimer;
    private int currentDirection = 1; // -1: 왼쪽, 1: 오른쪽, 0: 정지
    private float nextJumpTime;
    private Collider2D enemyCollider;

    void Awake()
    {
        anim = GetComponent<Animator>();
        currentHP = maxHP;
        currentSpeed = originalSpeed;
        rb = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<Collider2D>();
        originalLocalScale = transform.localScale;

        // 적끼리 충돌 무시
        int enemyLayer = gameObject.layer;
        if (enemyLayer != 0)
        {
            Physics2D.IgnoreLayerCollision(enemyLayer, enemyLayer, true);
        }

        if (groundLayer.value == 0)
        {
            groundLayer = 1 << 3; //Ground 레이어
        }
    }
    public virtual void Init(float dmg) // SkillExecutor 에서 실행함
    {
        baseDamage = dmg;
        finalDamage = dmg;
    }

    public virtual void TakeDamage(float damage)
    {
        // 빙결 체크 — 중복 진입 방지
        if (GetComponent<FrozenDebuff>() != null)
        {
            Debug.Log("빙결디버프터짐");
            GetComponent<FrozenDebuff>().FrozenBreak();
            Destroy(GetComponent<FrozenDebuff>());
        }
        currentHP -= damage;

        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }

        if (anim != null)
            anim.SetInteger("damaged", 1);
    }


    public void AddDebuff(DebuffType type, float duration)
    {
        switch (type)
        {
            case DebuffType.Poison:
                if (GetComponent<PoisonDebuff>() == null)
                {
                    gameObject.AddComponent<PoisonDebuff>().Init(this, duration);
                } else GetComponent<PoisonDebuff>().duration += 1;
                break;
            case DebuffType.Burn:
                if (GetComponent<BurnDebuff>() == null)
                {
                    gameObject.AddComponent<BurnDebuff>().Init(this, duration);
                } else GetComponent<BurnDebuff>().duration += 1;
                break;
            case DebuffType.Frozen:
                if (GetComponent<FrozenDebuff>() == null)
                {
                    gameObject.AddComponent<FrozenDebuff>().Init(this, duration);
                } else GetComponent<FrozenDebuff>().duration += 1;   
                break;
        }
    }

    protected virtual void Start()
    {
        ChooseNewState();
    }

    protected virtual void Update()
    {
        decisionTimer -= Time.deltaTime;
        if (decisionTimer <= 0f)
        {
            ChooseNewState();
        }

        CheckObstaclesAndEdges();
    }

    protected virtual void FixedUpdate()
    {
        if (rb != null)
        {
            // Apply constant horizontal speed (0 when idle)
            rb.linearVelocity = new Vector2(currentDirection * currentSpeed, rb.linearVelocity.y);

            // Flip the sprite based on direction
            if (currentDirection != 0)
            {
                transform.localScale = new Vector3(currentDirection * Mathf.Abs(originalLocalScale.x), originalLocalScale.y, originalLocalScale.z);
            }
        }
    }

    private void ChooseNewState()
    {
        // Randomly pick a state: move left (-1), stay idle (0), or move right (1)
        int[] choices = { -1, 0, 1 };
        currentDirection = choices[Random.Range(0, choices.Length)];

        // Hold this state for a random short interval (0.3s ~ 0.7s)
        decisionTimer = Random.Range(0.3f, 0.7f);
    }

    private void CheckObstaclesAndEdges()
    {
        if (currentDirection == 0 || enemyCollider == null) return;

        // 1. Wall Detection: Check if there's an obstacle in front of us
        float xOffset = currentDirection * (enemyCollider.bounds.extents.x + 0.1f);
        Vector2 wallOrigin = new Vector2(enemyCollider.bounds.center.x + xOffset, enemyCollider.bounds.center.y);
        RaycastHit2D wallHit = Physics2D.Raycast(wallOrigin, Vector2.right * currentDirection, 0.1f, groundLayer);

        // 2. Edge Detection: Check if there's ground ahead
        Vector2 edgeOrigin = new Vector2(enemyCollider.bounds.center.x + (currentDirection * enemyCollider.bounds.extents.x), enemyCollider.bounds.min.y);
        RaycastHit2D edgeHit = Physics2D.Raycast(edgeOrigin, Vector2.down, 0.5f, groundLayer);

        // If we hit a wall or there's no ground ahead (edge), turn around
        if (wallHit.collider != null || edgeHit.collider == null)
        {
            currentDirection = -currentDirection;
            decisionTimer = Random.Range(0.3f, 0.7f);
        }
    }

    private void FindPlayer()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

}
