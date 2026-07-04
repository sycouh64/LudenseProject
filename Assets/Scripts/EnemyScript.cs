using System.Collections;
using UnityEngine;
using static PlayerAttack;

using System.Collections;
using UnityEngine;
using static PlayerAttack;

public class EnemyScript : MonoBehaviour
{
    // 적 스크립트
    // 데미지 및 체력 구현, 무적타이밍 구현, 애니메이션과 연결하여 사라짐 구현
    // 추후 수정 예정(임시)
    private Animator anim;
    public bool enemyDamaged;

    public float maxHp = 100f;
    public float enemyHp = 100f;
    public float contactDamage = 10f; // Enemy's offensive (attack)


    public void TakeDamage(float damage)
    {   
        if (enemyDamaged) return;

        if (damage != 3)
        {
            attackStack++;
        }
        else{
            attackStack = 0;
        }

        enemyHp -= damage;
        enemyDamaged = true;
        
        Debug.Log($"Enemy took {damage} damage. Current HP: {enemyHp}");

        if (enemyHp <= 0)
        {
            Destroy(gameObject);
        }

        if (anim != null)
            anim.SetInteger("damaged", 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(contactDamage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Also check triggers in case the player/enemy use trigger colliders for contact
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(contactDamage);
            }
        }
    }

    public void OnEnemyDamagedAnimationEnd()
    {
        StartCoroutine(EnemyInvincible());
    }

    IEnumerator EnemyInvincible()
    {
        yield return new WaitForSeconds(0.5f);
        enemyDamaged = false;
        anim.SetInteger("damaged", 0);
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyDamaged = false;
        enemyHp = maxHp;
    }
}
