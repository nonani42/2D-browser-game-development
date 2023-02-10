using System;
using UnityEngine;

namespace PlatformerMVC
{
    public class PlayerController
    {
        public Action ResetAfterDeath { get; set; }
        public Action ISeeLocked { get; set; }
        public Action ISeeInteractable { get; set; }


        private DestroyableObjectsView _playerView;

        private Transform _playerTransform;
        private Rigidbody2D _playerRb;
        private SpriteRenderer _playerSpriteRenderer;
        private Collider2D _playerCollider;


        private HealthModule _healthModule;
        private PlayerMovementModule _playerMovementModule;
        private CoinCounter _coinCounter;


        private Vector3 _playerStartPosition;
        int _maxHealthPoint;
        int _startingCoins;

        private LayerMask _mask;

        private bool isLocked;
        private bool isInteractable;


        public HealthModule HealthModule { get => _healthModule; private set => _healthModule = value; }
        public PlayerMovementModule PlayerMovementModule { get => _playerMovementModule; private set => _playerMovementModule = value; }
        public CoinCounter CoinCounter { get => _coinCounter; set => _coinCounter = value; }

        public bool IsLocked 
        { 
            get 
            {
                return CheckForObj("LockedObjects");
            }
            set => isLocked = value; }
        
        public bool IsInteractable
        {
            get
            {
                return CheckForObj("Buttons");
            }
            set => isInteractable = value; }

        public Transform PlayerTransform { get => _playerTransform; set => _playerTransform = value; }

        public PlayerController(DestroyableObjectsView player)
        {
            _playerView = player;

            PlayerTransform = _playerView._transform;
            _playerRb = _playerView._rb;
            _playerSpriteRenderer = _playerView._spriteRenderer;
            _playerCollider = _playerView._collider;
            _maxHealthPoint = _playerView.HealthPoint;

            HealthModule = new HealthModule(_maxHealthPoint, _maxHealthPoint);
            PlayerMovementModule = new PlayerMovementModule(_playerCollider, _playerRb, PlayerTransform, _playerSpriteRenderer);
            CoinCounter = new CoinCounter(_startingCoins);

            _playerView.TakeDamage += HealthModule.GetDamage;
            HealthModule.CharacterDied += Died;
            _playerView.PickUpCoin += CoinCounter.AddCoins;
            _playerStartPosition = PlayerTransform.position;
        }

        public void Update()
        {
            PlayerMovementModule.Update();
        }

        private void Died()
        {
            PlayerTransform.position = _playerStartPosition;
            HealthModule.SetHealth(_maxHealthPoint, _maxHealthPoint);
            ResetAfterDeath?.Invoke();
        }


        private RaycastHit2D CheckForObj(string mask)
        {
            _mask = LayerMask.GetMask(mask);
            RaycastHit2D ray = new RaycastHit2D();
            if (PlayerTransform.localScale.x > 0)
            {
                Debug.DrawRay(PlayerTransform.position, Vector2.right, Color.blue);
                ray = Physics2D.Raycast(PlayerTransform.position, Vector2.right, 1f, _mask);
            }

            if (PlayerTransform.localScale.x < 0)
            {
                Debug.DrawRay(PlayerTransform.position, Vector2.left, Color.blue);
                ray = Physics2D.Raycast(PlayerTransform.position, Vector2.left, 1f, _mask);
            }
            return ray;
        }
    }
}
