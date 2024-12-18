using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTest
{
    private GameObject player;
    private PlayerController playerController;
    private Rigidbody2D rb;

    [SetUp]
    public void SetUp()
    {
        player = new GameObject("TestPlayer");
        rb = player.AddComponent<Rigidbody2D>();
        playerController = player.AddComponent<PlayerController>();
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        playerController.TestingMode = true;
    }

    [Test]
    public void PlayerCanMoveUp()
    {
        playerController.PlayerInput();
        rb.velocity = new Vector2(0, 5f); // Simulate upward movement
        Assert.Greater(rb.position.y, 0, "Player should move up.");
    }

    [Test]
    public void PlayerCanMoveDown()
    {
        playerController.PlayerInput();
        rb.velocity = new Vector2(0, -5f); // Simulate downward movement
        Assert.Less(rb.position.y, 0, "Player should move down.");
    }

    [Test]
    public void PlayerCanMoveLeft()
    {
        playerController.PlayerInput();
        rb.velocity = new Vector2(-5f, 0); // Simulate leftward movement
        Assert.Less(rb.position.x, 0, "Player should move left.");
        Assert.IsTrue(playerController.FacingLeft, "Player should be facing left.");
    }

    [Test]
    public void PlayerCanMoveRight()
    {
        playerController.PlayerInput();
        rb.velocity = new Vector2(5f, 0); // Simulate rightward movement
        Assert.Greater(rb.position.x, 0, "Player should move right.");
        Assert.IsFalse(playerController.FacingLeft, "Player should not be facing left.");
    }
}