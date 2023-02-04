using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private InteractiveObjectView _playerView;
        [SerializeField] private CannonView _cannonView;
        [SerializeField] private InteractiveObjectView _groundEnemyView;
        [SerializeField] private GeneratorLevelView _generatorLevelView;


        private CameraController cameraController;
        private PlayerController playerController;
        private CannonController cannonController;
        private EmitterController emitterController;
        private EnemyController groundEnemyController;
        private GeneratorLevelController generatorLevelController;


        void Awake()
        {
            cameraController = new CameraController(Camera.main.transform, _playerView);
            playerController = new PlayerController(_playerView);
            cannonController = new CannonController(_cannonView._muzzleTransform, _playerView._transform);
            emitterController = new EmitterController(_cannonView._emitterTransform, _cannonView._bullets);
            groundEnemyController = new EnemyController(_groundEnemyView, _playerView._transform);

            generatorLevelController = new GeneratorLevelController(_generatorLevelView);
            generatorLevelController.Start();
        }

        void Update()
        {
            cameraController.Update();
            playerController.Update();
            cannonController.Update();
            emitterController.Update();
            groundEnemyController.Update();
        }
    }
}
