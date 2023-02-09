using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    [CreateAssetMenu(fileName = "QuestItemConfig", menuName = "Configs / QuestSystem / QuestItemConfig", order = 2)]
    public class QuestItemConfig : ScriptableObject
    {
        public string questId;
        public List<string> questItemId;
    }
}
