using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlatformerMVC
{
    public class UIView : MonoBehaviour
    {
        [SerializeField] private Slider _healthBar;
        [SerializeField] private Text _healthAmount;

        [SerializeField] private PlayerController _player;

        public PlayerController Player { private get => _player; set => _player = value; }

        public void Start()
        {
            if (Player == null) return;
            Player.HealthModule.ChangeHealth += SetHealth;
            _healthBar.minValue = 0;
            SetHealth(Player.HealthModule.MaxHealth, Player.HealthModule.CurrentHealth);
        }

        public void SetHealth(int maxHealth, int currentHealth)
        {
            _healthBar.maxValue = maxHealth;
            _healthBar.value = currentHealth;
            _healthAmount.enabled = currentHealth > _healthBar.minValue;
            _healthAmount.text = currentHealth.ToString();
        }
    }
}