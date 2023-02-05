using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class GroundEnemyController
    {
        DestroyableObjectsView _enemyView;
        Transform _playerTransform;



        private Transform _targetTransform;
        private Transform _enemyTransform;
        private Rigidbody2D _enemyRb;
        private SpriteRenderer _sprite;

        private float _speed = 30f;
        private float _targetDistance = 5f;

        private Vector3 _patrolDirection;
        private Vector3 _targetDirection;

        private float _rayLength = 2f;
        private Vector3 _rayDirection;
        private LayerMask _mask = LayerMask.GetMask("Platform");


        public GroundEnemyController(DestroyableObjectsView enemy, Transform player)
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

            if (CheckForSurface())
            {
                _enemyRb.AddForce(_patrolDirection * Time.deltaTime * _speed, ForceMode2D.Force);
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

            if (CheckForSurface())
            {
                _enemyRb.AddForce(new Vector3(_targetDirection.x, _enemyTransform.position.y, _targetDirection.z) * Time.deltaTime * _speed, ForceMode2D.Force);
            }
            else
            {
                _enemyRb.velocity = Vector3.zero;
            }
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

        private RaycastHit2D CheckForSurface()
        {
            return Physics2D.Raycast(_enemyTransform.position, _rayDirection, _rayLength, _mask);
        }
    }
}
