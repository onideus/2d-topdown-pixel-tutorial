using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    
    private Rigidbody2D _rb;
    private Vector2 _moveDir;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }
    
    private void Move()
    {
        _rb.MovePosition(_rb.position + _moveDir * (moveSpeed * Time.fixedDeltaTime));
    }
    
    public void MoveTo(Vector2 roamDir)
    {
        _moveDir = roamDir;
    }
}
