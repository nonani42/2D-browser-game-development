using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private GameObject[] _background;

        [SerializeField] private DestroyableObjectsView _playerView;
        [SerializeField] private QuestView _questView;
        [SerializeField] private UIView _UIView;
        [SerializeField] private GeneratorLevelView _generatorLevelView;

        [Header("Enemies")] 
        [SerializeField] private CannonView[] _cannonViews;
        [SerializeField] private DestroyableObjectsView[] _groundEnemyViews;
        [SerializeField] private EnemyPatrolView[] _flyingEnemyViews;

        [Header("Locked Items")]
        [SerializeField] private LockedItemView[] _chestViews;
        [SerializeField] private LockedItemView[] _doorViews;

        [Header("Quest Items")]
        [SerializeField] private QuestObjectView[] _coinViews;


        private CameraController cameraController;
        private PlayerController playerController;
        private BackgroundController backgroundController;
        private GeneratorLevelController generatorLevelController;
        private QuestConfiguratorController questConfiguratorController;
        private QuestDistributorController questDistributorController;


        private List<CannonController> cannonControllers;
        private List<GroundEnemyController> groundEnemyControllers;
        private List<FlyingEnemyController> flyingEnemyControllers;

        private List<ChestController> chestControllers;
        private List<DoorController> doorControllers;

        private List<CoinController> coinControllers;


        void Awake()
        {
            cameraController = new CameraController(Camera.main.transform, _playerView);
            playerController = new PlayerController(_playerView);
            _UIView.Player = playerController;
            questConfiguratorController = new QuestConfiguratorController(_questView, _playerView);
            backgroundController = new BackgroundController(_background, Camera.main);
            //generatorLevelController = new GeneratorLevelController(_generatorLevelView);
            //generatorLevelController.Start();

            cannonControllers = new List<CannonController>();
            groundEnemyControllers = new List<GroundEnemyController>();
            flyingEnemyControllers = new List<FlyingEnemyController>();
            chestControllers = new List<ChestController>();
            coinControllers = new List<CoinController>();
            doorControllers = new List<DoorController>();


            for (int i = 0; i < _cannonViews.Length; i++)
            {
                cannonControllers.Add(new CannonController(_cannonViews[i], _playerView._transform));
            }
            for (int i = 0; i < _groundEnemyViews.Length; i++)
            {
                groundEnemyControllers.Add(new GroundEnemyController(_groundEnemyViews[i], _playerView._transform));
            }
            for (int i = 0; i < _flyingEnemyViews.Length; i++)
            {
                flyingEnemyControllers.Add(new FlyingEnemyController(_flyingEnemyViews[i], _playerView._transform));
            }
            for (int i = 0; i < _chestViews.Length; i++)
            {
                chestControllers.Add(new ChestController(_chestViews[i]));
            }
            for (int i = 0; i < _doorViews.Length; i++)
            {
                doorControllers.Add(new DoorController(_doorViews[i]));
            }

            for (int i = 0; i < _coinViews.Length; i++)
            {
                coinControllers.Add(new CoinController(_coinViews[i]));
            }

            questDistributorController = new QuestDistributorController(playerController, questConfiguratorController.QuestStoryList, doorControllers.ToArray(), chestControllers.ToArray());
        }

        void Update()
        {
            cameraController.Update();
            playerController.Update();
            backgroundController.Update();

            foreach (var item in cannonControllers)
            {
                item.Update();
            }
            foreach (var item in groundEnemyControllers)
            {
                item.Update();
            }
            foreach (var item in flyingEnemyControllers)
            {
                item.Update();
            }
            foreach (var item in chestControllers)
            {
                item.Update();
            }
            foreach (var item in coinControllers)
            {
                item.Update();
            }
        }
    }
}
