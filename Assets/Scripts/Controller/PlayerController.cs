using System;
using UnityEngine;

namespace PlatformerMVC
{
    public class PlayerController
    {
        public Action<int, int> ChangeHealth { get; set; }

        private InteractiveObjectView _playerView;

        private AnimationConfig _config;
        private SpriteAnimController _playerAnimator;
        private ContactPooler _contactPooler;

        private Transform _playerTransform;
        private Rigidbody2D _playerRb;

        private float _xAxisInput;
        private bool _isJump;

        private float _walkSpeed = 150f;
        private float _animSpeed = 14f;
        private float _movingThreshold = 0.1f;

        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);

        private bool _isMoving;

        private float _jumpForce = 8f;
        private float _jumpThreshold = 1f;
        private float _xVelocity = 0;
        private float _yVelocity = 0;

        private int _maxHealth = 100;
        private int _currentHealth;



        public int MaxHealth { get => _maxHealth; private set => _maxHealth = value; }
        public int CurrentHealth { get => _currentHealth; private set => _currentHealth = value; }
        public bool IsMoving { get => _isMoving; private set => _isMoving = value; }
        public Transform PlayerTransform { get => _playerTransform; private set => _playerTransform = value; }

        public PlayerController(InteractiveObjectView player)
        {
            _playerView = player;
            _playerRb = _playerView._rb;
            PlayerTransform = _playerView._transform;

            _config = Resources.Load<AnimationConfig>("SpriteAnimCfg");
            _playerAnimator = new SpriteAnimController(_config);

            _contactPooler = new ContactPooler(_playerView._collider);

            _playerView.TakeDamage += GetDamage;
            CurrentHealth = MaxHealth;
        }

        private void MoveTowards()
        {
            _xVelocity = _walkSpeed * Time.fixedDeltaTime * (_xAxisInput < 0 ? -1 : 1);
            _playerRb.velocity = new Vector2(_xVelocity, _yVelocity);
            PlayerTransform.localScale = _xAxisInput < 0 ? _leftScale : _rightScale;
        }


        private void GetDamage(EnemyView enemyView)
        {
            CurrentHealth -= enemyView.DamagePoint;
            ChangeHealth?.Invoke(MaxHealth, CurrentHealth);
            Debug.Log(CurrentHealth);
        }

        public void Update()
        {
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                _playerView._spriteRenderer.enabled = false;
            }

            _playerAnimator.Update();
            _contactPooler.Update();

            _xAxisInput = Input.GetAxis("Horizontal");
            _isJump = Input.GetAxis("Vertical") > 0;//Input.GetKeyDown(KeyCode.Space);
            IsMoving = Mathf.Abs(_xAxisInput) > _movingThreshold;
            _yVelocity = _playerRb.velocity.y;


            if ((_contactPooler.LeftContact || _contactPooler.RightContact) && !_contactPooler.IsGrounded)
            {
                IsMoving = false;
            }

            if (IsMoving)
            {
                MoveTowards();
            }
            else
            {
                _xVelocity = 0;
                _playerRb.velocity = new Vector2(_xVelocity, _playerRb.velocity.y);
            }

            _playerAnimator.StartAnimation(_playerView._spriteRenderer, IsMoving ? AnimState.Run : AnimState.Idle, true, _animSpeed);

            if (_contactPooler.IsGrounded)
            {

                if (_isJump && _yVelocity <= _jumpThreshold)
                {
                    _playerRb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                }
            }
            else
            {
                if (Mathf.Abs(_yVelocity) > _jumpThreshold)
                {
                    _playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.Jump, true, _animSpeed);
                }
            }

        }
    }
}
