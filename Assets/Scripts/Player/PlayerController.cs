using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool FacingLeft
    {
        get => _facingLeft;
        set => _facingLeft = value;
    }
    public static PlayerController Instance;

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float dashMultiplier = 4f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private TrailRenderer trailRenderer;

    private PlayerControls _playerControls;
    private Vector2 _movement;
    private Rigidbody2D _rb;
    private Animator _playerAnimator;
    private SpriteRenderer _playerSpriteRenderer;

    private bool _facingLeft = false;
    private bool _isDashing = false;

    private static readonly int MoveX = Animator.StringToHash("moveX");
    private static readonly int MoveY = Animator.StringToHash("moveY");

    private void Awake()
    {
        Instance = this;
        _playerControls = new PlayerControls();
        _rb = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _playerControls.Combat.Dash.performed += _ => Dash();
    }
    
    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        Move();
        AdjustPlayerFacingDirection();
    }

    private void PlayerInput()
    {
        _movement = _playerControls.Movement.Move.ReadValue<Vector2>();
        _playerAnimator.SetFloat(MoveX, _movement.x);
        _playerAnimator.SetFloat(MoveY, _movement.y);
    }

    private void Move()
    {
        _rb.MovePosition(_rb.position + _movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection()
    {
        var mousePosition = Input.mousePosition;
        var playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        _playerSpriteRenderer.flipX = mousePosition.x < playerScreenPoint.x;
        _facingLeft = mousePosition.x < playerScreenPoint.x;
    }

    private void Dash()
    {
        if (_isDashing) return;
        moveSpeed *= dashMultiplier;
        _isDashing = true;
        trailRenderer.emitting = true;
        StartCoroutine(EndDashRoutine());
    }

    private IEnumerator EndDashRoutine()
    {
        var dashDuration = .2f;
        yield return new WaitForSeconds(dashDuration);
        moveSpeed /= dashMultiplier;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCooldown);
        _isDashing = false;
    }
}
