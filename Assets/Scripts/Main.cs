using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private LevelObjectView _playerView;
        private PlayerController playerController;

        void Awake()
        {
            playerController = new PlayerController(_playerView);
        }

        void Update()
        {
            playerController.Update();
        }
    }
}
