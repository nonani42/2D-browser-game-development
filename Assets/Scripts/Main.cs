using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private LevelObjectView _playerView;
        [SerializeField] private CannonView _cannonView;

        private PlayerController playerController;
        private CannonController cannonController;

        void Awake()
        {
            playerController = new PlayerController(_playerView);
            cannonController = new CannonController(_cannonView._muzzleTransform, _playerView._transform);
        }

        void Update()
        {
            playerController.Update();
            cannonController.Update();
        }
    }
}
