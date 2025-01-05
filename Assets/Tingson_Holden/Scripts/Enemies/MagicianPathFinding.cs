using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianPathFinding : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    private Rigidbody2D _rb;
    private Vector2 _moveDir;
    private KnockBack _knockBack;
    private SpriteRenderer _spriteRenderer;
    private bool attacking = false;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _knockBack = GetComponent<KnockBack>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_knockBack.GettingKnockedBack)
        {
            return;
        }

        if (attacking)
        {
            return;
        }
        _rb.MovePosition(_rb.position + _moveDir * (_moveSpeed * Time.fixedDeltaTime));

        if (_moveDir.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
    }

    public void MoveTo(Vector2 targetPosition)
    {
        _moveDir = targetPosition;
    }

    public void attackChange()
    {
        attacking = attacking != true;
    }
}
