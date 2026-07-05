using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnManager : MonoBehaviour
{

    [SerializeField] GameObject enemy_1_Prefab; // 프리팹 연결
    public void SpawnEnemy(EnemyList.Enemy enemy, Vector2 locate)
    {
        switch (enemy.enemyName)
        {
            case "적_1":
                SpawnEnemy_1(enemy.enemyDamage, locate, enemy.enemyHP);
                break;
        }
    }

    void SpawnEnemy_1(float damage, Vector2 locate, float hp)
    {
        // 마우스 위치 (목표 지점)
        Vector3 startPos = locate;
        startPos.z = 0;

        var obj = Instantiate(enemy_1_Prefab, startPos, Quaternion.identity);
        obj.GetComponent<Enemy_1_Script>().Init(damage);
    }
}
