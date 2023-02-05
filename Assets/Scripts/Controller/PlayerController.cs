using System;
using UnityEngine;

namespace PlatformerMVC
{
    public class PlayerController
    {

        private DestroyableObjectsView _playerView;

        private Transform _playerTransform;
        private Rigidbody2D _playerRb;
        private SpriteRenderer _playerSpriteRenderer;
        private Collider2D _playerCollider;


        private HealthModule _healthModule;
        private PlayerMovementModule _playerMovementModule;

        private Vector3 _playerStartPosition;
        int _maxHealthPoint;


        public HealthModule HealthModule { get => _healthModule; private set => _healthModule = value; }
        public PlayerMovementModule PlayerMovementModule { get => _playerMovementModule; private set => _playerMovementModule = value; }

        public PlayerController(DestroyableObjectsView player)
        {
            _playerView = player;

            _playerTransform = _playerView._transform;
            _playerRb = _playerView._rb;
            _playerSpriteRenderer = _playerView._spriteRenderer;
            _playerCollider = _playerView._collider;
            _maxHealthPoint = _playerView.HealthPoint;

            HealthModule = new HealthModule(_maxHealthPoint, _maxHealthPoint);
            PlayerMovementModule = new PlayerMovementModule(_playerCollider, _playerRb, _playerTransform, _playerSpriteRenderer);


            _playerView.TakeDamage += HealthModule.GetDamage;
            HealthModule.CharacterDied += Died;

            _playerStartPosition = _playerTransform.position;
        }

        public void Update()
        {
            PlayerMovementModule.Update();
        }

        private void Died()
        {
            _playerTransform.position = _playerStartPosition;
            HealthModule.SetHealth(_maxHealthPoint, _maxHealthPoint);
        }
    }
}
