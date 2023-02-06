using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    [CreateAssetMenu(fileName = "QuestItemConfig", menuName = "Configs / QuestSystem / QuestItemConfig", order = 2)]
    public class QuestItemConfig : ScriptableObject
    {
        public int questId;
        public List<int> questItemId;
    }
}
