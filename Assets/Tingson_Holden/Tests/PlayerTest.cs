using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTest
{
    private GameObject player;
    private PlayerHealth playerHealth;

    [SetUp]
    public void SetUp()
    {
        player = new GameObject("Player");
        playerHealth = player.AddComponent<PlayerHealth>();
        playerHealth.TestingMode = true;
    }

    [Test]
    public void PlayerCanTakeDamage()
    {
        playerHealth.TakeDamage(1);
        Assert.Less(playerHealth._currentHealth, playerHealth._maxHealth);
    }

    [Test]
    public void PlayerCanDie()
    {
        playerHealth.TakeDamage(playerHealth._maxHealth);
        Assert.IsTrue(playerHealth.isDead);
    }

}