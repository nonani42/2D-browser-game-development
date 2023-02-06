using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class ChestController
    {
        private InteractiveObjectView _chestView;
        private SpriteAnimController _coinAnimator;
        private AnimationConfig _config;
        private SpriteRenderer _coinSpriteRenderer;

        private float _speed = 14f;

        public ChestController(InteractiveObjectView chestView)
        {
            _chestView = chestView;

            _config = Resources.Load<AnimationConfig>("BlueChestAnimConfig");
            _coinAnimator = new SpriteAnimController(_config);
            _coinSpriteRenderer = _chestView._spriteRenderer;
            _chestView.Interact += OnInteraction;
        }

        public void Update()
        {
            _coinAnimator.Update();
        }

        private void OnInteraction(InteractiveObjectView player)
        {
            _coinAnimator.StartAnimation(_coinSpriteRenderer, AnimState.Open, false, _speed);
            _chestView.Interact -= OnInteraction;
        }
    }
}
