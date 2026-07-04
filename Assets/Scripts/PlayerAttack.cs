using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    // 플레이어 기본공격인데 작동안함. 이전 코드에서 긁어온 것. 추후 수정 예정
    private Animator anim;
    public static bool isAttacking;

    public Transform attackPoint;
    public float radius;
    public LayerMask enemyLayer;
    public static int attackStack;
    public float attackPower = 15f; // Decent player attack power (< 20)

    private void Awake()
    {
        attackStack = 0;
        isAttacking = false;
        anim = GetComponent<Animator>();
    }

    public void OnAttack(InputValue value)
    {
        
        //if (isAttacpking) return;
        //isAttacking = true;
        //if (attackStack <= 2)
        //{
        //    anim.SetInteger("isAttacking", 1);
        //}else{
        //    anim.SetInteger("isAttacking", 2);
        //}
        
    }

    public void OnAnimationEnd()
    {
        anim.SetInteger("isAttacking", 0);
        StartCoroutine(RefreshAttack());
    }

    IEnumerator RefreshAttack()
    {
        yield return new WaitForSeconds(0.2f);
        isAttacking = false;
    }

    public void AttackHit()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, radius, enemyLayer);

        foreach (Collider2D hit in hits)
        {
            if (attackStack <= 2)
            {
                hit.GetComponent<EnemyScript>()?.TakeDamage(attackPower);
            }
            else{
                hit.GetComponent<EnemyScript>()?.TakeDamage(Mathf.Min(20f, attackPower * 1.2f)); // Cap heavy attack at 20 as requested
            }
        }
    }
    
}
