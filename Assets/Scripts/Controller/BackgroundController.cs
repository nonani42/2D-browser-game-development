using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class BackgroundController
    {
        private GameObject[] _backgrounds;
        private Transform[] _backgroundTransforms;
        private Vector3[] _backgroundstartingPositions;


        private Camera _mainCamera;
        private Transform _mainCameraTransform;


        private int currentIndex;
        private int nextIndex;
        private float offset;
        private float threshold;

        private float backgroundWidth;
        private float screenWidth;

        private float screenRightEdge;
        private float screenLeftEdge;


        public int NextIndex {
            get {
                if (nextIndex >= _backgroundTransforms.Length)
                {
                    nextIndex = 0;
                }
                return nextIndex;
            } set => nextIndex = value; }

        public float ScreenRightEdge
        {
            get
            {
                return _mainCameraTransform.position.x + screenWidth / 2; ;
            }
            set => screenRightEdge = value;
        }

        public float ScreenLeftEdge
        {
            get
            {
                return _mainCameraTransform.position.x - screenWidth / 2;
            }
            set => screenLeftEdge = value;
        } 


        public BackgroundController(GameObject[] backgrounds, Camera camera)
        {
            _mainCamera = camera;
            _mainCameraTransform = camera.transform;
            _backgroundTransforms = new Transform[backgrounds.Length];
            _backgroundstartingPositions = new Vector3[backgrounds.Length];
            for (int i = 0; i < _backgroundTransforms.Length; i++)
            {
                _backgroundTransforms[i] = backgrounds[i].transform;
                _backgroundstartingPositions[i] = _backgroundTransforms[i].position;
            }

            backgroundWidth = backgrounds[NextIndex].GetComponent<SpriteRenderer>().bounds.size.x;

            Vector3 leftSidePxl = new Vector3(_mainCamera.pixelWidth - _mainCamera.pixelWidth, _mainCamera.pixelHeight - _mainCamera.pixelHeight, _mainCameraTransform.position.z);
            Vector3 rightSidePxl = new Vector3(_mainCamera.pixelWidth, _mainCamera.pixelHeight, _mainCameraTransform.position.z);
            screenWidth = Vector3.Distance(_mainCamera.ScreenToWorldPoint(rightSidePxl), _mainCamera.ScreenToWorldPoint(leftSidePxl));

            offset = backgroundWidth;
            threshold = 2f;
            currentIndex = 0;
        }

        public void Update()
        {
            float input = Input.GetAxis("Horizontal");
            if (input > 0)
            {
                if (CheckForEdge(Sides.Right))
                {
                    MoveBackground(offset);
                }
            }
            else if(input < 0)
            {
                if (CheckForEdge(Sides.Left))
                {
                    MoveBackground(-offset);
                }
            }
        }

        private bool CheckForEdge(Sides side)
        {
            float screenSide = 0f;
            float backgroundSide = 0f;

            for (int i = 0; i < _backgroundTransforms.Length; i++)
            {
                switch (side)
                {
                    case (Sides.Left):
                        screenSide = _backgroundTransforms[i].position.x - backgroundWidth / 2;
                        backgroundSide = ScreenLeftEdge;
                        break;
                    case (Sides.Right):
                        screenSide = _backgroundTransforms[i].position.x + backgroundWidth / 2;
                        backgroundSide = ScreenRightEdge;
                        break;

                }
                if (Mathf.Abs(screenSide - backgroundSide) <= threshold)
                {
                    NextIndex = i;
                    return true;
                }
            }
            return false;
        }

        private void MoveBackground(float offset)
        {
            currentIndex = NextIndex;
            NextIndex++;
            Vector3 moveTo = new Vector3(
                _backgroundTransforms[currentIndex].position.x + offset, 
                _backgroundTransforms[NextIndex].position.y, 
                _backgroundTransforms[NextIndex].position.z
                );

            _backgroundTransforms[NextIndex].position = moveTo;
        }

        public void OnReset()
        {
            for (int i = 0; i < _backgroundTransforms.Length; i++)
            {
                _backgroundTransforms[i].position = _backgroundstartingPositions[i];
            }        
        }

        enum Sides
        {
            Left,
            Right,
        }
    }
}
