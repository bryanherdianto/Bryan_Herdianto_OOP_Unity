using UnityEngine;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    private Label healthLabel;
    private Label enemiesLeftLabel;
    private Label pointsLabel;
    private Label waveLabel;

    private EnemySpawner[] enemySpawners;
    private int totalPoints = 0;

    public void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        healthLabel = root.Q<Label>("HealthLabel");
        enemiesLeftLabel = root.Q<Label>("EnemiesLeftLabel");
        pointsLabel = root.Q<Label>("PointsLabel");
        waveLabel = root.Q<Label>("WaveLabel");
        enemySpawners = FindObjectsOfType<EnemySpawner>();
    }

    public void Update()
    {
        totalPoints = 0;
        var player = FindObjectOfType<Player>();
        var combatManager = FindObjectOfType<CombatManager>();

        healthLabel.text = "Health: " + player.GetComponent<HealthComponent>().getHealth;
        enemiesLeftLabel.text = "Enemies Left: " + combatManager.totalEnemies;
        foreach (var spawner in enemySpawners)
        {
            totalPoints += spawner.totalKill * spawner.spawnedEnemy.level;
        }
        pointsLabel.text = "Points: " + totalPoints;
        waveLabel.text = "Wave: " + combatManager.waveNumber;
    }
}