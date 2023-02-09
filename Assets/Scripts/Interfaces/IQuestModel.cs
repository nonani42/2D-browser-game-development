using UnityEngine;

namespace PlatformerMVC
{
    public interface IQuestModel
    {
        bool TryComplete(QuestObjectView view, IQuest quest);
    }
}
