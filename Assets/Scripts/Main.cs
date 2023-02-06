using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private DestroyableObjectsView _playerView;
        [SerializeField] private CannonView _cannonView;
        [SerializeField] private DestroyableObjectsView _groundEnemyView;
        [SerializeField] private EnemyPatrolView _flyingEnemyView;

        [SerializeField] private GeneratorLevelView _generatorLevelView;

        [SerializeField] private InteractiveObjectView _chestView;

        [SerializeField] private UIView _UIView;

        //
        [SerializeField] private QuestObjectView[] _coinViews;


        private CameraController cameraController;
        private PlayerController playerController;
        private CannonController cannonController;
        private GroundEnemyController groundEnemyController;
        private FlyingEnemyController flyingEnemyController;

        private GeneratorLevelController generatorLevelController;

        private ChestController chestController;

        //
        private QuestController questController;
        private QuestCoinModel questModel;
        private CoinController[] coinControllers;



        void Awake()
        {
            cameraController = new CameraController(Camera.main.transform, _playerView);
            playerController = new PlayerController(_playerView);
            cannonController = new CannonController(_cannonView, _playerView._transform);
            groundEnemyController = new GroundEnemyController(_groundEnemyView, _playerView._transform);
            flyingEnemyController = new FlyingEnemyController(_flyingEnemyView, _playerView._transform);

            generatorLevelController = new GeneratorLevelController(_generatorLevelView);
            generatorLevelController.Start();

            chestController = new ChestController(_chestView);


            _UIView.Player = playerController;

            //
            questModel = new QuestCoinModel();
            foreach (var coin in _coinViews)
            {
                questController = new QuestController(_playerView, coin, questModel);
                questController.Reset();
            }

            coinControllers = new CoinController[_coinViews.Length];
            for (int i = 0; i < _coinViews.Length; i++)
            {
                coinControllers[i] = new CoinController(_coinViews[i]);
            }
        }

        void Update()
        {
            cameraController.Update();
            playerController.Update();
            cannonController.Update();
            groundEnemyController.Update();
            flyingEnemyController.Update();
            chestController.Update();
            foreach (var item in coinControllers)
            {
                item.Update();
            }
        }
    }
}
