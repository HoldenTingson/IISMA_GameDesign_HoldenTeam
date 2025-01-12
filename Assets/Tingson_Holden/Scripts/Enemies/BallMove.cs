using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the character
    public Rigidbody2D rb; // Rigidbody2D component
    public float health;
    private Vector2 movement; // Stores player movement input
    SpriteRenderer sr;
    private Vector2 direction;

    public int dashing = 0; // Dash duration counter
    private bool dashInput = false; // Tracks if dash input was detected

    void Start()
    {
        health = 100f;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashing == 0 && movement != Vector2.zero)
        {
            dashInput = true;
        }
    }

    // Handle physics in FixedUpdate
    void FixedUpdate()
    {
        if (dashInput && dashing == 0)
        {
            direction = movement.normalized; 
            dashing = 6; 
            dashInput = false; 
        }

        if (dashing > 0)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime * 3);
            dashing--;
            return;
        }

        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        E_attack attack = other.gameObject.GetComponent<E_attack>();
        health -= attack.getDamage();
        setColor();
        
        Debug.Log($"Player got hit by {other.gameObject.name}.");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        E_attack attack = other.gameObject.GetComponent<E_attack>();
        health -= attack.getDamage();
        setColor();
        
        Debug.Log($"Player got hit by {other.gameObject.name}.");
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        E_attack attack = other.gameObject.GetComponent<E_attack>();
        health -= attack.getDamage();
        setColor();
        
        Debug.Log($"Player got hit by {other.gameObject.name}.");
    }

    public void setColor()
    {
        sr.color = new Color(1, health / 100, health / 100, 1);
    }
}
