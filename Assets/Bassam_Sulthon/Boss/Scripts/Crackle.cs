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
    BoxCollider2D boxCollider;
    

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        damage = 20;
        timer = 0f; 
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= delay && !isFading)
        {
            isFading = true;
            timer = 0f;
            boxCollider.enabled = false;
        }

        if (isFading)
        {
            float alpha = Mathf.Clamp01(1f - (timer / fadeDuration)); 
            Color currentColor = sr.color;
            sr.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);

            if (timer >= fadeDuration)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth.Instance.TakeDamage(2);
        }
    }
}