using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private PlayerControls _playerControls;
    private Animator _animator;
    
    private static readonly int Attack = Animator.StringToHash("Attack");

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }
    
    private void Start()
    {
        _playerControls.Combat.Attack.started += _ => TriggerAttack();
    }

    private void TriggerAttack()
    {
        _animator.SetTrigger(Attack);
    }
    
    private void Update()
    {
        
    }
}
