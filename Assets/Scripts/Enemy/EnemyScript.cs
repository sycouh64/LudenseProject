using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;


public abstract class EnemyScript : MonoBehaviour, IHasHP
{
    [SerializeField] public float originalSpeed = 10f;
    [SerializeField] public float currentSpeed;
    private float baseDamage;
    [SerializeField] public float finalDamage;


    [SerializeField] private float maxHP = 100f;
    public float currentHP = 100f;

    public float OriginalSpeed => originalSpeed;
    public float CurrentSpeed => currentSpeed;
    public float CurrentHP => currentHP;
    public float MaxHP => maxHP;

    private Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
        currentHP = maxHP;
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

   
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.CompareTag("Player"))
        //{
        //    PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
        //    if (playerHealth != null)
        //    {
        //        playerHealth.TakeDamage(finalDamage);
        //    }
        //}
    }
    protected virtual void Die()
    {
        Destroy(gameObject);
    }

}
