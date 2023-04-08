using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    
    private PlayerControls _playerControls;
    private Animator _animator;
    private PlayerController _playerController;
    private ActiveWeapon _activeWeapon;

    private GameObject _slashAnim;

    private static readonly int Attack = Animator.StringToHash("Attack");

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _animator = GetComponent<Animator>();
        _playerController = GetComponentInParent<PlayerController>();
        _activeWeapon = GetComponentInParent<ActiveWeapon>();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void Start()
    {
        _playerControls.Combat.Attack.started += _ => TriggerAttack();
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    private void TriggerAttack()
    {
        _animator.SetTrigger(Attack);

        _slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        _slashAnim.transform.parent = this.transform.parent;
    }

    public void SwingUpFlipAnimation()
    {
        _slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (_playerController.FacingLeft)
        {
            _slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnimation()
    {
        _slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (_playerController.FacingLeft)
        {
            _slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowWithOffset()
    {
        var mousePosition = Input.mousePosition;
        var playerScreenPoint = Camera.main.WorldToScreenPoint(_playerController.transform.position);
        var angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;

        _activeWeapon.transform.rotation = mousePosition.x < playerScreenPoint.x
            ? Quaternion.Euler(0, -180, angle)
            : Quaternion.Euler(0, 0, angle);
    }
}
