using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class CannonController
    {
        private CannonView _cannon;
        private Transform _muzzleTransform;
        private Transform _targetTransform;
        private EmitterController emitterController;


        private Vector3 _dir;
        private Vector3 _axis;
        private float _angle;

        private float _targetDistance;


        public CannonController(CannonView cannon, Transform target)
        {
            _cannon = cannon;
            _muzzleTransform = _cannon._muzzleTransform;
            _targetTransform = target;

            _targetDistance = 10f;

            emitterController = new EmitterController(_cannon._emitterTransform, _cannon._bullets);

        }

        public void Update()
        {
            if (Vector3.Distance(_muzzleTransform.position, _targetTransform.position) <= _targetDistance)
            {
                _dir = _targetTransform.position - _muzzleTransform.position;
                MoveMuzzle();
                emitterController.Update();
            }
        }

        private void MoveMuzzle()
        {
            _angle = Vector3.Angle(Vector3.down, _dir);
            _axis = Vector3.Cross(Vector3.down, _dir);

            _muzzleTransform.rotation = Quaternion.AngleAxis(_angle, _axis);
        }
    }
}
