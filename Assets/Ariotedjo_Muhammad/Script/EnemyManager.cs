using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager _instance;

    public static EnemyManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<EnemyManager>();
            }
            return _instance;
        }
    }

    private int enemyCount;

    void Start()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    public void EnemyKilled()
    {
        enemyCount--;
    }

    public int GetEnemyCount()
    {
        return enemyCount;
    }
}