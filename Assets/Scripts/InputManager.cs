using UnityEngine;
using UnityEngine.InputSystem;
using static SkillList;

public class InputManager : MonoBehaviour
{
    // 스킬 사용 입력 스크립트
    private Skill fireball;
    private Skill risingVine;
    private Skill leafStorm;
    private Skill meteor;
    private Skill icicleShot;
    private SkillExecutor executor;

    
    void Start()
    {
        executor = GetComponent<SkillExecutor>();
        fireball = new FireBall(executor);
        risingVine = new RisingVine(executor);
        leafStorm = new LeafStorm(executor);
        meteor = new Meteor(executor);
        icicleShot = new IcicleShot(executor);
    }
    public void OnSkill_1(InputValue value)
    {
        fireball.Activate();
    }
    public void OnSkill_2(InputValue value)
    {
        risingVine.Activate();
    }
    public void OnSkill_3(InputValue value)
    {
        leafStorm.Activate();
    }

    public void OnSkill_4(InputValue value)
    {
        meteor.Activate();
    }

    public void OnSkill_5(InputValue value)
    {
        icicleShot.Activate();
    }
}
