using UnityEngine;

public class EnemyList : MonoBehaviour
{
    public class Enemy
    {
        public string enemyName;
        public float enemyDamage;
        public float enemyHP;
        public Enemy(string name, float damage, float hp)
        {
            enemyName = name;
            enemyDamage = damage;
            enemyHP = hp;
        }
        public virtual void Activate(Vector2 locate) { }
    }

    public class Enemy_1 : Enemy
    {
        private EnemySpawnManager spawnManager;

        public Enemy_1(EnemySpawnManager spawn)
            : base("적_1", 15, 100)
        {
            spawnManager = spawn;
        }
        public override void Activate(Vector2 locate)
        {
            spawnManager.SpawnEnemy(this, locate);
        }
    }
}
