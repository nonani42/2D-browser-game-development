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
        private SpriteRenderer _chestSpriteRenderer;

        private QuestObjectView _lootView;


        private string _questStoryId;


        private float _speed = 14f;

        public string QuestStoryId { get => _questStoryId; private set => _questStoryId = value; }

        public ChestController(LockedItemView chestView)
        {
            _chestView = chestView;

            _config = _chestView.Config;
            _chestAnimator = new SpriteAnimController(_config);
            _chestSpriteRenderer = _chestView._spriteRenderer;
            _questStoryId = _chestView.QuestStoryId;
            _lootView = _chestView.Loot;
            _lootView.ProcessComplete();
        }

        public void Update()
        {
            _chestAnimator.Update();
        }

        public void Unlock(IQuestStory story)
        {
            _chestAnimator.StartAnimation(_chestSpriteRenderer, AnimState.Open, false, _speed);
            _lootView.GetComponent<QuestObjectView>().enabled = true;
            _lootView.ProcessActivate();
            _chestView._collider.enabled = false;
        }
    }
}
