using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public EnemySpawner[] enemySpawners;
    public float timer = 0;
    [SerializeField] private float waveInterval = 5f;
    public int waveNumber = 1;
    public int totalEnemies = 0;

    void Start()
    {
        foreach (var spawner in enemySpawners)
        {
            setSpawnStatus(spawner, true);
            if (spawner.isSpawning)
            {
                totalEnemies += spawner.defaultSpawnCount;
            }
        }
    }

    void Update()
    {
        if (totalEnemies == 0)
        {
            foreach (var spawner in enemySpawners)
            {
                setSpawnStatus(spawner, false);
            }
            timer += Time.deltaTime;
            if (timer > waveInterval)
            {
                waveNumber++;
                foreach (var spawner in enemySpawners)
                {
                    setSpawnStatus(spawner, true);
                    spawner.resetSpawnCount();
                    if (spawner.isSpawning)
                    {
                        totalEnemies += spawner.spawnCount;
                    }
                    spawner.setTimerFirst();
                }
                timer = 0;
            }
        }
    }

    public void setSpawnStatus(EnemySpawner spawner, bool status)
    {
        if (spawner.spawnedEnemy is EnemyBoss enemyBoss && enemyBoss.level <= waveNumber)
        {
            spawner.isSpawning = status;
        }
        if (spawner.spawnedEnemy is EnemyForward enemyForward && enemyForward.level <= waveNumber)
        {
            spawner.isSpawning = status;
        }
        if (spawner.spawnedEnemy is EnemyHorizontal enemyHorizontal && enemyHorizontal.level <= waveNumber)
        {
            spawner.isSpawning = status;
        }
        if (spawner.spawnedEnemy is EnemyTargeting enemyTargeting && enemyTargeting.level <= waveNumber)
        {
            spawner.isSpawning = status;
        }
    }
}