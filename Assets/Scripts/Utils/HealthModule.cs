using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class HealthModule
    {
        public Action<int, int> ChangeHealth { get; set; }
        public Action CharacterDied { get; set; }

        public int MaxHealth { get => _maxHealth; private set => _maxHealth = value; }
        public int CurrentHealth { get => _currentHealth; private set => _currentHealth = value; }

        private int _maxHealth;
        private int _currentHealth;

        public HealthModule(int maxHealth, int currentHealth)
        {
            SetHealth(maxHealth, currentHealth);
        }

        public void GetDamage(DestroyableObjectsView enemyView)
        {
            SetHealth(MaxHealth, CurrentHealth - enemyView.DamagePoint);
            if (CurrentHealth <= 0)
            {
                CharacterDied?.Invoke();
            }
        }

        public void SetHealth(int maxHealth, int currentHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = currentHealth;
            ChangeHealth?.Invoke(MaxHealth, CurrentHealth);
        }

        public void SetMaxHealth(int maxHealth)
        {
            MaxHealth = maxHealth;
            ChangeHealth?.Invoke(MaxHealth, CurrentHealth);
        }
    }
}
