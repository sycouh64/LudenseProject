using UnityEngine;
using static SkillList;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public abstract class ElementFieldSkill : MonoBehaviour
{
    protected float projectileSpeed; // 값 받아오기
    protected float interval = 1;
    [SerializeField] protected Animator anim;
    protected float skillDestroyTime = 5;


    [SerializeField] protected DebuffType debuffType;

    private List<EnemyScript> targetsInField = new List<EnemyScript>();


    // Update is called once per frame
    protected void Awake()
    {
        StartCoroutine(TickDebuffApply());
        anim = GetComponent<Animator>();
        Destroy(gameObject, skillDestroyTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<EnemyScript>(out var target))
        {
            Debug.Log("적이닿음");
            target.AddDebuff(debuffType, 5);
            targetsInField.Add(target);
            OnEnemyEnterField(other);
        }
        if (other.CompareTag("Player"))
        {
            OnPlayerEnterField();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<EnemyScript>(out var target))
        {
            OnEnemyExitField(other);
        }

        if (other.CompareTag("Player"))
        {
            targetsInField.Remove(target);
            OnPlayerExitField();
        }
    }

    private IEnumerator TickDebuffApply()
    {
        while (true) 
        {
            foreach (var target in targetsInField.ToList())
            {
                target.AddDebuff(debuffType, 5);
            }
            yield return new WaitForSeconds(1);
        }
    }
    protected void OnEnemyEnterField(Collider2D other) { }
    protected void OnEnemyExitField(Collider2D other) { }
    protected void OnPlayerEnterField() { }
    protected void OnPlayerExitField() { }


}
