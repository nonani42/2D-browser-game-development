using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public enum QuestType
    {
        Coins = 0,
        Buttons = 1,
    }

    [CreateAssetMenu(fileName = "QuestConfig", menuName = "Configs / QuestSystem / QuestConfig", order = 1)]
    public class QuestConfig : ScriptableObject
    {
        public string questId;
        public QuestType type;
    }
}
