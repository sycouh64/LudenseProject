using UnityEngine;
using UnityEngine.InputSystem;
using static SkillList;

public class InputManager : MonoBehaviour
{
    // 스킬 사용 입력 스크립트
    private Skill fireball;
    private SkillExecutor executor;
    
    void Start()
    {
        executor = GetComponent<SkillExecutor>();
        fireball = new FireBall(executor);
    }
    public void OnSkill_1(InputValue value)
    {
        fireball.Activate();
    }
}
