using System;
using UnityEngine;

public class BossProjectile2 : E_attack
{
    private Rigidbody2D rb;
    public Vector3 target;
    public float speed = 10f;
    private CapsuleCollider cc;

    private void Awake()
    {
        damage = 20;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider>();
        // Calculate direction toward the target
        Vector3 direction = (target - transform.position).normalized;

        // Set projectile rotation to face the target
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Apply velocity in the calculated direction
        rb.velocity = direction * speed;

        // Destroy the projectile after a fixed duration
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth.Instance.TakeDamage(2);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}