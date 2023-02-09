using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class QuestView : MonoBehaviour
    {
        [SerializeField] private QuestConfig[] _questConfigs;
        [SerializeField] private QuestItemConfig[] _questItemConfigs;
        [SerializeField] private QuestStoryConfig[] _storyQuestConfigs;

        [SerializeField] private QuestObjectView[] _questObjects;


        public QuestObjectView[] QuestObjects { get => _questObjects; private set => _questObjects = value; }
        public QuestStoryConfig[] StoryQuestConfigs { get => _storyQuestConfigs; private set => _storyQuestConfigs = value; }
        public QuestConfig[] QuestConfigs { get => _questConfigs; private set => _questConfigs = value; }
        public QuestItemConfig[] QuestItemConfigs { get => _questItemConfigs; private set => _questItemConfigs = value; }
    }
}
