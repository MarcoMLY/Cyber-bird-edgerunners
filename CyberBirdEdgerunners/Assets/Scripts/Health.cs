using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth;
    public int MaxHealth { get => _maxHealth; }

    private int _health;
    public int HealthProperty
    {
        get => _health;
        set
        {
            if (value < 0)
            {
                _health = 0;
            }
            _health = value;
        }
    }

    private void Awake()
    {
        _health = _maxHealth;
    }

    public virtual void Damage(GameObject attacker, int damage)
    {
        HealthProperty -= damage;
        if (HealthProperty <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
