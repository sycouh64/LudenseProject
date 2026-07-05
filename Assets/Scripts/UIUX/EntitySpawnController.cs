using UnityEngine;
using static EnemyList;
using static SkillList;
public class EntitySpawnController : MonoBehaviour
{
    private Enemy enemy_1;
    private EnemySpawnManager enemySpawnManager;
    void Start()
    {
        Debug.Log(1);
        enemySpawnManager = GetComponent<EnemySpawnManager>();
        enemy_1 = new Enemy_1(enemySpawnManager);

        enemy_1.Activate(new Vector2(1f, 0f));
        if (enemy_1 != null)
        {
            Debug.Log(1557);
        }
    }

}
