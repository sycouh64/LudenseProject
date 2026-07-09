using UnityEngine;
using static SkillList;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public abstract class ElementFieldSkill : MonoBehaviour
{
    [SerializeField] protected float projectileSpeed; // 값 받아오기
    protected float interval = 1;
    [SerializeField] protected Animator anim;
    [SerializeField] protected float skillDestroyTime = 5;


    [SerializeField] protected DebuffType debuffType;

    private List<IDamageable> targetsInField = new List<IDamageable>();


    // Update is called once per frame
    protected void Awake()
    {
        anim = GetComponent<Animator>();
        Destroy(gameObject, skillDestroyTime);
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out var target))
        {
            Debug.Log("적이닿음");
            target.AddDebuff(debuffType);
            OnEnemyEnterField(other);
        }
        if (other.CompareTag("Player"))
        {
            OnPlayerEnterField();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out var target))
        {
            target.RemoveDebuff(debuffType);
            OnEnemyExitField(other);
        }

        if (other.CompareTag("Player"))
        {
            targetsInField.Remove(target);
            OnPlayerExitField();
        }
    }
    void OnDestroy()
    {
        // 장판이 사라질 때 범위 안에 남아있는 적 디버프 제거
        foreach (var target in targetsInField.ToList())
        {
            target.RemoveDebuff(debuffType);
        }
    }

    protected void OnEnemyEnterField(Collider2D other) { }
    protected void OnEnemyExitField(Collider2D other) { }
    protected void OnPlayerEnterField() { }
    protected void OnPlayerExitField() { }


}
