using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainUI : MonoBehaviour
{
    public int health = 100;
    public int points = 0;
    public int wave = 0;
    public int enemies = 0;
    private Label healthLabel;
    private Label pointsLabel;
    private Label waveLabel;
    private Label enemiesLabel;

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        healthLabel = root.Q<Label>("Health");
        pointsLabel = root.Q<Label>("Points");
        waveLabel = root.Q<Label>("Wave");
        enemiesLabel = root.Q<Label>("Enemies");
    }

    void Update()
    {
        CombatManager combatManager = FindObjectOfType<CombatManager>();
        if (combatManager != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            points = combatManager.points;
            health = player.GetComponent<HealthComponent>().Health;
            wave = combatManager.waveNumber;
            enemies = combatManager.totalEnemies;

            pointsLabel.text = "POINTS: " + points;
            healthLabel.text = "HEALTH: " + health;
            waveLabel.text = "WAVE: " + wave;
            enemiesLabel.text = "ENEMIES: " + enemies;
        }
    }
}
