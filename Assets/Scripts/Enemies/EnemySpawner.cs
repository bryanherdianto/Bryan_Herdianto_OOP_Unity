using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public Enemy spawnedEnemy;

    [SerializeField] private int minimumKillsToIncreaseSpawnCount = 2;
    public int totalKill = 0;
    private int totalKillWave = 0;

    [SerializeField] private float spawnInterval = 3f;

    [Header("Spawned Enemies Counter")]
    public int spawnCount = 0;
    public int defaultSpawnCount = 1;
    public int spawnCountMultiplier = 1;
    public int multiplierIncreaseCount = 1;

    public CombatManager combatManager;
    public bool isSpawning = false;

    public float timer = 0;

    void Start()
    {
        Assert.IsTrue(spawnedEnemy != null, "Tambahkan 1 Prefab Enemy terlebih dahulu!");
        spawnCount = defaultSpawnCount;
    }

    private void Update()
    {
        if (isSpawning && spawnCount > 0)
        {
            timer += Time.deltaTime;
            if (totalKillWave >= minimumKillsToIncreaseSpawnCount)
            {
                defaultSpawnCount += defaultSpawnCount * spawnCountMultiplier;
                spawnCountMultiplier += multiplierIncreaseCount;
                totalKillWave = 0;
            }
            if (timer > spawnInterval)
            {
                spawnCount--;
                Instantiate(spawnedEnemy);
                timer = 0;
            }
        }
    }

    public void IncreaseKillCount()
    {
        totalKill++;
        totalKillWave++;
    }

    public void resetSpawnCount()
    {
        spawnCount = defaultSpawnCount;
    }

    public void setTimerFirst()
    {
        timer = spawnInterval;
    }
}