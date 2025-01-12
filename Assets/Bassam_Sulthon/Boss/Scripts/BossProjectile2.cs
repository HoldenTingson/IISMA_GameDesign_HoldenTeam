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
 
        Vector3 direction = (target - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        rb.velocity = direction * speed;

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