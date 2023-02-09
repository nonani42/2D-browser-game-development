using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class ChestController
    {
        private LockedItemView _chestView;
        private SpriteAnimController _chestAnimator;
        private AnimationConfig _config;
        private SpriteRenderer _coinSpriteRenderer;

        private string _questStoryId;


        private float _speed = 14f;

        public string QuestStoryId { get => _questStoryId; private set => _questStoryId = value; }

        public ChestController(LockedItemView chestView)
        {
            _chestView = chestView;

            _config = Resources.Load<AnimationConfig>("BlueChestAnimConfig");
            _chestAnimator = new SpriteAnimController(_config);
            _coinSpriteRenderer = _chestView._spriteRenderer;
            _questStoryId = _chestView.QuestStoryId;
        }

        public void Update()
        {
            _chestAnimator.Update();
        }

        public void Unlock(IQuestStory story)
        {
            _chestAnimator.StartAnimation(_coinSpriteRenderer, AnimState.Open, false, _speed);
        }
    }
}
