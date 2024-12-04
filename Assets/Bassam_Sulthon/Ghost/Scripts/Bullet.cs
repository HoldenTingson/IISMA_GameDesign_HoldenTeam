using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : E_attack
{
    public float time =5f;
    void Start()
    {
        Invoke(nameof(destroyBullet), time);
        damage = 20;
    }

    void destroyBullet()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            destroyBullet();   
        }
    }
}
