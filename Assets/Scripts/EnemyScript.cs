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

    public int enemyHp = 3;


    public void TakeDamage(int damage)
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
        if (enemyHp <= 0)
        {
            Destroy(gameObject);
        }

        anim.SetInteger("damaged",1);
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
    }
}
