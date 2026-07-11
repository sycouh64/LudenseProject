using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;


public abstract class EnemyScript : MonoBehaviour, IDamageable
{
    [SerializeField] public float originalSpeed = 10f;
    [SerializeField] public float currentSpeed;
    private float baseDamage;
    [SerializeField] public float finalDamage;


    [SerializeField] private float maxHP = 100f;
    private float currentHP = 100f;
    private List<DebuffType> activeDebuffs = new List<DebuffType>();

    public float OriginalSpeed => originalSpeed;
    public float CurrentSpeed => currentSpeed;
    public float CurrentHP => currentHP;
    public float MaxHP => maxHP;
    public bool IsDead => currentHP <= 0;

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
        float finalDamage = damage;
        if (activeDebuffs.Contains(DebuffType.Frozen))
        {
            Debug.Log("빙결디버프터짐");
            finalDamage += 100;
            RemoveDebuff(DebuffType.Frozen);
        }
        // 빙결디버프일 경우 10의 추가 데미지를 입음
        currentHP -= finalDamage;

        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }

        if (anim != null)
            anim.SetInteger("damaged", 1);
    }
    public virtual IEnumerator GetFrozenDebuff(float slowSpeed)
    {
        currentSpeed -= slowSpeed;
        yield return new WaitForSeconds(3);
        currentSpeed += slowSpeed;
    }
    public virtual void AddDebuff(DebuffType debuff)
    {
        if (!activeDebuffs.Contains(debuff))
        {
            Debug.Log("디버프적용됨");
            activeDebuffs.Add(debuff);
            GetDebuffList(debuff)?.Add(this);    // 해당 디버프 리스트에 자동 추가
        }
    }
    public virtual void RemoveDebuff(DebuffType debuff)
    {
        activeDebuffs.Remove(debuff);
        GetDebuffList(debuff)?.Remove(this);     // 해당 디버프 리스트에서 자동 제거
    }

    protected virtual List<IDamageable> GetDebuffList(DebuffType debuff)
    {
        return debuff switch
        {
            DebuffType.Poison => EnemyDebuff.poisonDebuffEnemyList,
            DebuffType.Burn => EnemyDebuff.burnDebuffEnemyList,
            DebuffType.Frozen => EnemyDebuff.frozenDebuffEnemyList,
            _ => null
        };
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
        // 죽을 때 모든 디버프 리스트에서 자동 제거
        foreach (var debuff in activeDebuffs.ToList())
            RemoveDebuff(debuff);

        Destroy(gameObject);
    }

}
