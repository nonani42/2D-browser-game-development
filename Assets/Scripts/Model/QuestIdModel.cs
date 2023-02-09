using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class QuestIdModel : IQuestModel
    {
        List<string> itemIds = new List<string>();

public bool TryComplete(QuestObjectView view, IQuest quest)
        {
            int count = 0;
            itemIds = quest.ItemIds;
            if (itemIds.Contains(view.Id))
            {
                count++;
            }
            return count == itemIds.Count;
        }
    }
}
