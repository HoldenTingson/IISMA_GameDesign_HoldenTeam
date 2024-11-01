using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class EnemyPathFinding : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    private Rigidbody2D _rb;
    private Vector2 _moveDir;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _moveDir * (_moveSpeed * Time.fixedDeltaTime));
    }

    public void MoveTo(Vector2 targetPosition)
    {
        _moveDir = targetPosition;
    }
}
