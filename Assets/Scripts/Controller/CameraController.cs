using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class CameraController
    {
        private LevelObjectView _player;
        private Transform _playerTransform;
        private Transform _cameraTransform;

        private float _cameraSpeed = 1.2f;

        private float X;
        private float Y;

        private Vector3 _destination;

        float offsetValue = 4f;
        private float _offsetX;
        private float _offsetY;

        private float _xAxisInput;
        private float _yAxisInput;

        private float _threshold;

        public CameraController(Transform camera, LevelObjectView player)
        {
            _player = player;
            _playerTransform = player._transform;
            _cameraTransform = camera;
            _threshold = 0.2f;

    }

    public void Update()
        {
            _xAxisInput = Input.GetAxis("Horizontal");
            _yAxisInput = _player._rb.velocity.y;
            X = _playerTransform.position.x;
            Y = _playerTransform.position.y;

            _offsetX = SetOffset(_xAxisInput);
            _offsetY = SetOffset(_yAxisInput);

            _destination = new Vector3(X + _offsetX, Y + _offsetY, _cameraTransform.position.z);

            _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, _destination, _cameraSpeed * Time.deltaTime);
        }

        private float SetOffset(float axis)
        {
            float offset = 0;

            if (axis > _threshold)
            {
                offset = offsetValue;
            }
            else if (axis < -_threshold)
            {
                offset = -offsetValue;
            }
            return offset;
        }
    }
}
