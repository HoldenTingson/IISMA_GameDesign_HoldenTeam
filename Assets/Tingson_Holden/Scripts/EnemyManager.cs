using System;
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
        enemies.Clear(); // Clear the current list to avoid duplicates
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemyObjects)
        {
            if (enemy.activeInHierarchy) // Only consider active enemies
            {
                enemies.Add(enemy);
            }
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
        Debug.Log(enemies.Count);
        return enemies.Count;
    }

    // Check if all enemies are defeated
    public bool AllEnemiesDefeated()
    {
        UpdateEnemyList();
        return enemies.Count == 0;
    }
}