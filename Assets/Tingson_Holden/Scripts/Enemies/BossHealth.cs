using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private GameObject fill; // Health bar fill
    private EnemyHealth _health;

    private void Start()
    {
        _health = GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        if (_health._currentHealth > 0)
        {
            // Scale the health bar fill based on current health
            float healthPercentage = (float)_health._currentHealth / _health._startingHealth;
            fill.transform.localScale = new Vector3(healthPercentage, 1f, 1f);
        }
        else
        {
            fill.transform.localScale = new Vector3(0f, 1f, 1f);
        }
    }
}