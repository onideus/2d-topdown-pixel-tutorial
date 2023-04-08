using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;

    private int _currentHealth;

    private void Awake()
    {
        _currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        DetectDeath();
        Debug.Log("Health: " + _currentHealth);
    }

    private void DetectDeath()
    {
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
