using UnityEngine;
using UnityEngine.InputSystem;
using static SkillList;

public class InputManager : MonoBehaviour
{
    // 스킬 사용 입력 스크립트
    private Skill fireball;
    private Skill risingVine;
    private SkillExecutor executor;

    
    void Start()
    {
        executor = GetComponent<SkillExecutor>();
        fireball = new FireBall(executor);
        risingVine = new SkillList.RisingVine(executor);
    }
    public void OnSkill_1(InputValue value)
    {
        fireball.Activate();
    }
    public void OnSkill_2(InputValue value)
    {
        risingVine.Activate();
    }
}
