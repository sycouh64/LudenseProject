using UnityEngine;
using System.Collections;

public class Enemy_1_Script : MonoBehaviour, IHasHP, IDamageable
{
    [SerializeField] public float speed = 10f;
    private float baseDamage;
    [SerializeField] public float finalDamage;

    public bool enemyDamaged;

    private float maxHp = 100f;
    private float nowHp = 100f;

    public float CurrentHP => nowHp;
    public float MaxHP => maxHp;

    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyDamaged = false;
        nowHp = maxHp;
    }
    public void Init(float dmg) // SkillExecutor 에서 실행함
    {
        baseDamage = dmg;
        finalDamage = dmg;
    }
    
    public void TakeDamage(float damage)
    {
        //if (enemyDamaged) return;

        nowHp -= damage;
        enemyDamaged = true;

        if (nowHp <= 0)
        {
            Destroy(gameObject);
        }

        if (anim != null)
            anim.SetInteger("damaged", 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Also check triggers in case the player/enemy use trigger colliders for contact
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(finalDamage);
            }
        }
    }

    //public void OnEnemyDamagedAnimationEnd()
    //{
    //    StartCoroutine(EnemyInvincible());
    //}

    //IEnumerator EnemyInvincible()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    enemyDamaged = false;
    //    anim.SetInteger("damaged", 0);
    //}
}
