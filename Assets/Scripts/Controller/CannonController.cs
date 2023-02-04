using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class CannonController
    {
        private Transform _muzzleTransform;
        private Transform _targetTransform;

        private Vector3 _dir;
        private Vector3 _axis;
        private float _angle;

        private float _targetDistance;


        public CannonController(Transform muzzle, Transform target)
        {
            _muzzleTransform = muzzle;
            _targetTransform = target;

            _targetDistance = 10f;
        }

        public void Update()
        {
            if (Vector3.Distance(_muzzleTransform.position, _targetTransform.position) > _targetDistance)
            {
                _dir = Vector3.down;
            }
            else
            {
                _dir = _targetTransform.position - _muzzleTransform.position;
            }
            MoveMuzzle();
        }

        private void MoveMuzzle()
        {
            _angle = Vector3.Angle(Vector3.down, _dir);
            _axis = Vector3.Cross(Vector3.down, _dir);

            _muzzleTransform.rotation = Quaternion.AngleAxis(_angle, _axis);
        }
    }
}
