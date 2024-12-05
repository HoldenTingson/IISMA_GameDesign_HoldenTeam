using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    private List<GameObject> enemies;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        enemies = new List<GameObject>();
    }

    private void Start()
    {
        UpdateEnemyList();
    }

    public void UpdateEnemyList()
    {
        enemies.Clear();
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemyObjects)
        {
            enemies.Add(enemy);
        }
    }

    public void RegisterEnemy(GameObject enemy)
    {
        if (!enemies.Contains(enemy))
        {
            enemies.Add(enemy);
        }
    }

    public void UnregisterEnemy(GameObject enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }
    }

    public int GetEnemyCount()
    {
        return enemies.Count;
    }

    // Fungsi untuk memeriksa apakah semua musuh sudah mati
    public bool AllEnemiesDefeated()
    {
        UpdateEnemyList();
        return enemies.Count == 0;
    }
}