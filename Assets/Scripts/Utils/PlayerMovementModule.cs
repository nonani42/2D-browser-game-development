using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class PlayerMovementModule
    {
        private SpriteAnimController _playerAnimator;
        private ContactPooler _contactPooler;
        private Transform _playerTransform;
        private Rigidbody2D _playerRb;
        private SpriteRenderer _playerSpriteRenderer;
        private AnimationConfig _config;

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

        public PlayerMovementModule(Collider2D playerCollider, Rigidbody2D playerRb, Transform playerTransform, SpriteRenderer playerSpriteRenderer)
        {
            _playerRb = playerRb;
            _playerSpriteRenderer = playerSpriteRenderer;
            _playerTransform = playerTransform;

            _config = Resources.Load<AnimationConfig>("PlayerAnimCfg");
            _playerAnimator = new SpriteAnimController(_config);
            _contactPooler = new ContactPooler(playerCollider);
        }

        public void Update()
        {
            _playerAnimator.Update();
            _contactPooler.Update();

            _xAxisInput = Input.GetAxis("Horizontal");
            _isJump = Input.GetAxis("Vertical") > 0;//Input.GetKeyDown(KeyCode.Space);
            _isMoving = Mathf.Abs(_xAxisInput) > _movingThreshold;
            _yVelocity = _playerRb.velocity.y;

            if ((_contactPooler.LeftContact || _contactPooler.RightContact) && !_contactPooler.IsGrounded)
            {
                _isMoving = false;
            }

            if (_isMoving)
            {
                MoveTowards();
            }
            else
            {
                _xVelocity = 0;
                _playerRb.velocity = new Vector2(_xVelocity, _playerRb.velocity.y);
            }

            _playerAnimator.StartAnimation(_playerSpriteRenderer, _isMoving ? AnimState.Run : AnimState.Idle, true, _animSpeed);

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
                    _playerAnimator.StartAnimation(_playerSpriteRenderer, AnimState.Jump, true, _animSpeed);
                }
            }
        }

        private void MoveTowards()
        {
            _xVelocity = _walkSpeed * Time.fixedDeltaTime * (_xAxisInput < 0 ? -1 : 1);
            _playerRb.velocity = new Vector2(_xVelocity, _yVelocity);
            _playerTransform.localScale = _xAxisInput < 0 ? _leftScale : _rightScale;
        }
    }
}
