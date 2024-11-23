using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float time =5f;
    void Start()
    {
        Invoke(nameof(destroyBullet), time);
    }


    void destroyBullet()
    {
        Destroy(gameObject);
    }
}
