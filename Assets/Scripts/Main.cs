using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private AnimationConfig _config;
        [SerializeField] private LevelObjectView _playerView;
        private SpriteAnimController _playerAnimator;

        void Awake()
        {
            _config = Resources.Load<AnimationConfig>("SpriteAnimCfg");
            _playerAnimator = new SpriteAnimController(_config);
            _playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.Idle, true, 10f);

        }

        void Update()
        {
            _playerAnimator.Update();
        }
    }
}
