using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class LockedItemView : InteractiveObjectView
    {
        [SerializeField] private string _questStoryId;
        [SerializeField] private QuestObjectView _loot;
        [SerializeField] private AnimationConfig _config;


        public string QuestStoryId { get => _questStoryId; private set => _questStoryId = value; }
        public QuestObjectView Loot { get => _loot; private set => _loot = value; }
        public AnimationConfig Config { get => _config; private set => _config = value; }

        private void Awake()
        {
            gameObject.name = _questStoryId;
        }
    }
}
