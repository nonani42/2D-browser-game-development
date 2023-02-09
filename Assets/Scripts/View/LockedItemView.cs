using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class LockedItemView : InteractiveObjectView
    {
        [SerializeField] private string _questStoryId;

        public string QuestStoryId { get => _questStoryId; set => _questStoryId = value; }

        private void Awake()
        {
            gameObject.name = _questStoryId;
        }
    }
}
