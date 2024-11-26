using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float maxHealth = 100;
    private float health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public float getHealth
    {
        get { return health; }
    }

    public void Subtract(float value)
    {
        health -= value;
        if (health < 0)
        {
            EnemySpawner[] enemySpawners = FindObjectsOfType<EnemySpawner>();
            foreach (EnemySpawner spawner in enemySpawners)
            {
                if (spawner.spawnedEnemy.name == gameObject.name.Replace("(Clone)", "").Trim())
                {
                    spawner.IncreaseKillCount();
                }
            }

            CombatManager combatManager = FindObjectOfType<CombatManager>();
            combatManager.totalEnemies--;

            Destroy(gameObject);
        }
    }
}
