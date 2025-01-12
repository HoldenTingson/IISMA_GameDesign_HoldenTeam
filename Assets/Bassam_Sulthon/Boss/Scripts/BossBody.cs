using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBody : MonoBehaviour
{
    private CapsuleCollider2D cc;
    [SerializeField] private GameObject fill;

    void Start()
    {
        cc = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            if (fill.transform.localScale.x > 0)
            {
                fill.transform.localScale -= new Vector3(0.05f, 0f, 0f);
            }
        }
    }
}
