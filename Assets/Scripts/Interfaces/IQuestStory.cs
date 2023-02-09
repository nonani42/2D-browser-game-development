using System;
using System.Collections.Generic;

namespace PlatformerMVC
{
    public interface IQuestStory : IDisposable
    {
        event Action<IQuestStory> QuestStoryCompleted;

        List<IQuest> QuestList { get; }
        bool IsDone { get; }
        string QuestStoryId { get; }
    }
}
