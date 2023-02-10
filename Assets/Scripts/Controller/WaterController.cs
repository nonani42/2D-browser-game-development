using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class WaterController
    {
        private LevelObjectView _waterView;
        private SpriteAnimController _waterAnimator;
        private AnimationConfig _config;
        private SpriteRenderer _waterSpriteRenderer;

        private float _speed = 14f;

        public WaterController(LevelObjectView waterView)
        {
            _waterView = waterView;
            _config = Resources.Load<AnimationConfig>("WaterAnimCfg"); ;
            _waterAnimator = new SpriteAnimController(_config); ;
            _waterSpriteRenderer = _waterView._spriteRenderer;
        }

        public void Update()
        {
            _waterAnimator.Update();
            _waterAnimator.StartAnimation(_waterSpriteRenderer, AnimState.Idle, true, _speed);
        }
    }
}
