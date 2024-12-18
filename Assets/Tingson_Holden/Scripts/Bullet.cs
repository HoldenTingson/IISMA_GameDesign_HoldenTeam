using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : E_attack
{
    public float time =3f;

    void Start()
    {
        Invoke(nameof(destroyBullet), time);
    }

    void destroyBullet()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth.Instance.TakeDamage(3);
            destroyBullet();   
        }
    }
}
