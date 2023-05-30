using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Data;

public class PlayerHealth : AdvancedHealth
{
    [SerializeField] private DataInt _healthData;

    private void Update()
    {
        _healthData.SetData(HealthProperty);
    }

    public void Heal(int amount)
    {
        HealthProperty += amount;
        if (HealthProperty > MaxHealth)
        {
            HealthProperty = MaxHealth;
        }
    }
}
