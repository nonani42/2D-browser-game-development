using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlatformerMVC
{
    public class PopupView : MonoBehaviour
    {
        [SerializeField] private Text _pressButton;
        [SerializeField] private Text _objStatus;

        private Transform _btnTransform;
        private Transform _objTransform;

        [SerializeField] private PlayerController _player;

        private Vector3 _showPosition;
        private bool _showInteraction;
        private bool _showStatus;
        private Camera _mainCamera;

        private float _offsetX;
        private float _offsetY;

        public PlayerController Player { private get => _player; set => _player = value; }

        public void Awake()
        {
            _btnTransform = _pressButton.transform;
            _objTransform = _objStatus.transform;
            _pressButton.enabled = false;
            _objStatus.enabled = false;
            _showInteraction = false;
            _showStatus = false;

            _mainCamera = Camera.main;

            _offsetX = _pressButton.rectTransform.rect.width;
            _offsetY = _pressButton.rectTransform.rect.height;
        }

        public void Update()
        {
            Vector3 worldPosition = new Vector3(_player.PlayerTransform.position.x, _player.PlayerTransform.position.y, 0);
            Vector3 screenPosition = _mainCamera.WorldToScreenPoint(worldPosition);
            _showPosition = new Vector3(screenPosition.x + _offsetX, screenPosition.y + _offsetY, 0);

            if (!_showStatus && _player.IsLocked)
            {
                CheckStatus();
                _showStatus = _player.IsLocked;
            }
            if (_player.IsInteractable && !_showInteraction)
            {
                ToInteract();
                _showInteraction = _player.IsInteractable;
            }
            if((_showStatus && !_player.IsLocked)||(!_player.IsInteractable && _showInteraction))
            {
                StopPopups();
                _showStatus = false;
                _showInteraction = false;
            }
        }

        public void ToInteract()
        {
            _pressButton.enabled = true;
            _btnTransform.position = _showPosition;
            _pressButton.text = $"Press 'E' to interact.";
        }

        public void CheckStatus()
        {
            _objStatus.enabled = true;
            _objTransform.position = _showPosition;
            _objStatus.text = $"Locked. Find a button to open.";
        }

        public void StopPopups()
        {
            _objStatus.enabled = false;
            _pressButton.enabled = false;
        }
    }
}
