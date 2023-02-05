using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private InteractiveObjectView _playerView;
        [SerializeField] private CannonView _cannonView;
        [SerializeField] private EnemyView _groundEnemyView;
        [SerializeField] private EnemyPatrolView _flyingEnemyView;

        [SerializeField] private GeneratorLevelView _generatorLevelView;
        [SerializeField] private UIView _UIView;


        private CameraController cameraController;
        private PlayerController playerController;
        private CannonController cannonController;
        private GroundEnemyController groundEnemyController;
        private FlyingEnemyController flyingEnemyController;

        private GeneratorLevelController generatorLevelController;


        void Awake()
        {
            cameraController = new CameraController(Camera.main.transform, _playerView);
            playerController = new PlayerController(_playerView);
            cannonController = new CannonController(_cannonView, _playerView._transform);
            groundEnemyController = new GroundEnemyController(_groundEnemyView, _playerView._transform);
            flyingEnemyController = new FlyingEnemyController(_flyingEnemyView, _playerView._transform);

            generatorLevelController = new GeneratorLevelController(_generatorLevelView);
            generatorLevelController.Start();

            _UIView.Player = playerController;
        }

        void Update()
        {
            cameraController.Update();
            playerController.Update();
            cannonController.Update();
            groundEnemyController.Update();
            flyingEnemyController.Update();
        }
    }
}
