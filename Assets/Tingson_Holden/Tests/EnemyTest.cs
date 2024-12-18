using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemyTest
{
    private GameObject enemy;
    private EnemyHealth enemyHealth;

    [SetUp]
    public void SetUp()
    {
        enemy = new GameObject("Enemy");
        enemyHealth = enemy.AddComponent<EnemyHealth>();
        enemyHealth.TestingMode = true;
    }

    [Test]
    public void EnemyCanTakeDamage()
    {
        enemyHealth.TakeDamage(1);
        Assert.Less(enemyHealth._currentHealth, enemyHealth._startingHealth);
    }

    [UnityTest]
    public IEnumerator EnemyCanDie()
    {
        enemyHealth.TakeDamage(enemyHealth._startingHealth);

        yield return new WaitForSeconds(0.1f);

        Assert.IsTrue(enemy == null);
    }
}
