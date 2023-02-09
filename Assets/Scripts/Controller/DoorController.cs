using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class DoorController
    {
        private LockedItemView _doorView;
        private string _questStoryId;

        public DoorController(LockedItemView door)
        {
            _doorView = door;
            _questStoryId = _doorView.QuestStoryId;
        }

        public string QuestStoryId { get => _questStoryId; private set => _questStoryId = value; }

        public void Unlock(IQuestStory story)
        {
            Color alpha = _doorView._spriteRenderer.color;
            alpha.a = 0.5f;
            _doorView._spriteRenderer.color = alpha;
            _doorView._collider.enabled = false;
        }
    }
}
