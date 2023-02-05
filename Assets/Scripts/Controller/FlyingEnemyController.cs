using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class FlyingEnemyController
    {
        private EnemyPatrolView _enemyView;
        private Transform _playerTransform;

        private Transform[] _patrolPoints;

        private Transform _targetTransform;
        private Transform _enemyTransform;
        private Rigidbody2D _enemyRb;
        private SpriteRenderer _sprite;

        private float _speed = 10f;
        private float _targetDistance = 5f;

        private Vector3 _patrolDirection;
        private int _count = 0;

        private Vector3 _targetDirection;

        public FlyingEnemyController(EnemyPatrolView enemy, Transform player)
        {
            _enemyView = enemy;
            _enemyRb = _enemyView._rb;
            _enemyTransform = _enemyView._transform;
            _sprite = _enemyView._spriteRenderer;

            _playerTransform = player;

            _patrolPoints = _enemyView.PatrolPoints;
        }

        private void Patrol()
        {
            if(_count == _patrolPoints.Length)
            {
                _count = 0;
            }

            _patrolDirection = _patrolPoints[_count].position - _enemyTransform.position;
            _enemyRb.AddForce(_patrolDirection * Time.deltaTime * _speed, ForceMode2D.Force);
            if (Vector2.Distance(_patrolPoints[_count].position, _enemyTransform.position) <= 1f)
            {
                _enemyRb.velocity = Vector3.zero;
                _count++;
            }
        }


        private void Follow()
        {
            _targetDirection = _targetTransform.position - _enemyTransform.position;
            _enemyRb.AddForce(_targetDirection * Time.deltaTime * _speed, ForceMode2D.Force);
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