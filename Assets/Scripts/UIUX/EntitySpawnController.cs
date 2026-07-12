using UnityEngine;
using static EnemyList;
using static SkillList;
public class EntitySpawnController : MonoBehaviour
{
    private Enemy enemy_1;
    private EnemySpawnManager enemySpawnManager;
    void Start()
    {
        enemySpawnManager = GetComponent<EnemySpawnManager>();
        enemy_1 = new Enemy_1(enemySpawnManager);

        enemy_1.Activate(new Vector2(1f, 0f));
        enemy_1.Activate(new Vector2(1f, 0f));
        enemy_1.Activate(new Vector2(1f, 0f));
    }

}
