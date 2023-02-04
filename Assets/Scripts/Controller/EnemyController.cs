using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class EnemyController
    {
        InteractiveObjectView _enemyView;
        Transform _playerTransform;



        private Transform _targetTransform;
        private Transform _enemyTransform;
        private Rigidbody2D _enemyRb;
        private SpriteRenderer _sprite;

        private float _speed = 300f;
        private float _targetDistance = 5f;

        private Vector3 _patrolDirection;
        private Vector3 _targetDirection;
        private Vector3 _rayDirection;
        LayerMask mask = LayerMask.GetMask("Platform");


        public EnemyController(InteractiveObjectView enemy, Transform player)
        {
            _enemyView = enemy;
            _enemyRb = _enemyView._rb;
            _enemyTransform = _enemyView._transform;
            _sprite = _enemyView._spriteRenderer;

            _playerTransform = player;

            _patrolDirection = _enemyTransform.right;
        }

        private void Patrol()
        {
            _rayDirection = _patrolDirection - _enemyTransform.up;

            if (Physics2D.Raycast(_enemyTransform.position, _rayDirection, 2f, mask))
            {
                _enemyRb.AddForce(_patrolDirection * _speed * Time.deltaTime, ForceMode2D.Force);
            }
            else
            {
                _enemyRb.velocity = Vector3.zero;
                _patrolDirection = -_patrolDirection;
            }
        }

        private void Follow()
        {
            _targetDirection = _targetTransform.position - _enemyTransform.position;
            _enemyRb.AddForce(new Vector3(_targetDirection.x, _enemyTransform.position.y, _targetDirection.z) * _speed * Time.fixedDeltaTime, ForceMode2D.Force);
        }


        public void Update()
        {
            if (CheckForTarget())
            {
                Follow();
            }
            else
            {
                Patrol();
            }
        }

        private bool CheckForTarget()
        {
            if (Vector3.Distance(_playerTransform.position, _enemyTransform.position) <= _targetDistance)
            {
                _targetTransform = _playerTransform;
            }
            else
            {
                _targetTransform = null;
            }
            return _targetTransform != null;
        }
    }
}
