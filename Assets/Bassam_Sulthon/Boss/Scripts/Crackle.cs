using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crackle : E_attack
{
    SpriteRenderer sr;
    private float delay = 0.5f; // Time to wait before fading
    private float fadeDuration = 1f; // Duration for the fade-out
    private float timer; // Tracks time since object spawned
    private bool isFading = false; // Flag to track when to start fading
    BoxCollider2D collider;
    

    void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        damage = 20;
        timer = 0f; // Initialize the timer
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= delay && !isFading)
        {
            isFading = true; // Start fading after the delay
            timer = 0f; // Reset the timer for fading
            collider.enabled = false;
        }

        if (isFading)
        {
            // Gradually reduce the alpha value during the fade duration
            float alpha = Mathf.Clamp01(1f - (timer / fadeDuration)); // Calculate alpha based on fade progress
            Color currentColor = sr.color;
            sr.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);

            // Destroy the object when fully faded
            if (timer >= fadeDuration)
            {
                Destroy(gameObject);
            }
        }
    }
}