using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public enum QuestStoryType
    {
        Common = 0,
        Resettable = 1,
    }

    [CreateAssetMenu(fileName = "QuestStoryConfig", menuName = "Configs / QuestSystem / QuestStoryConfig", order = 3)]
    public class QuestStoryConfig : ScriptableObject
    {
        public string questStoryId;
        public QuestConfig[] questsConfig;
        public QuestStoryType questStoryType;
    }
}
