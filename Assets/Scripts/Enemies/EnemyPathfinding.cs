using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D _rb;
    private Vector2 _moveDir;
    private Knockback _knockback;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _knockback = GetComponent<Knockback>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (_knockback.gettingKnockedBack)
        {
            return;
        }

        _rb.MovePosition(_rb.position + _moveDir * (moveSpeed * Time.fixedDeltaTime));
    }

    public void MoveTo(Vector2 roamDir)
    {
        _moveDir = roamDir;
    }
}
