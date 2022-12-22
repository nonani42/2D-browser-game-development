using UnityEngine;

namespace PlatformerMVC
{
    public class PlayerController
    {
        private LevelObjectView _playerView;

        private AnimationConfig _config;
        private SpriteAnimController _playerAnimator;
        private Transform _playerTransform;

        private float _xAxisInput;
        private bool _isJump;

        private float _walkSpeed = 3f;
        private float _animSpeed = 10f;
        private float _movingThreshold = 0.1f;

        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);

        private bool _isMoving;

        private float _jumpForce = 9f;
        private float _jumpThreshold = 1f;
        private float _g = -9.8f;
        private float _groundLevel = -0.5f;
        private float _yVelocity;


        public PlayerController(LevelObjectView player)
        {
            _playerView = player;
            _playerTransform = _playerView._transform;

            _config = Resources.Load<AnimationConfig>("SpriteAnimCfg");
            _playerAnimator = new SpriteAnimController(_config);

        }

        private void MoveTowards()
        {
            _playerTransform.position += Vector3.right * (_walkSpeed * Time.deltaTime * (_xAxisInput < 0 ? -1 : 1));
            _playerTransform.localScale = _xAxisInput < 0 ? _leftScale : _rightScale;
        }

        public bool IsGrounded()
        {
            return _playerTransform.position.y <= _groundLevel && _yVelocity <= 0;
        }

        public void Update()
        {
            _playerAnimator.Update();

            _xAxisInput = Input.GetAxis("Horizontal");
            _isJump = Input.GetAxis("Vertical") > 0;//Input.GetKeyDown(KeyCode.Space);
            _isMoving = Mathf.Abs(_xAxisInput) > _movingThreshold;

            if (_isMoving)
            {
                MoveTowards();
            }

            if (IsGrounded())
            {
                _playerAnimator.StartAnimation(_playerView._spriteRenderer, _isMoving ? AnimState.Run : AnimState.Idle, true, _animSpeed);

                if (_isJump && _yVelocity <= 0)
                {
                    _yVelocity = _jumpForce;
                }
                else if (_yVelocity < 0)
                {
                    _yVelocity = 0;
                    _playerTransform.position = new Vector3(_playerTransform.position.x, _groundLevel, _playerTransform.position.z);
                }
            }
            else
            {
                if (Mathf.Abs(_yVelocity) > _jumpThreshold)
                {
                    _playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.Jump, true, _animSpeed);
                }
                _yVelocity += _g * Time.deltaTime;
                _playerTransform.position += Vector3.up * (_yVelocity * Time.deltaTime);
            }
        }
    }
}
