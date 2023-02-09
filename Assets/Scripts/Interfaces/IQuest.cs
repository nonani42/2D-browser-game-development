using System;
using System.Collections.Generic;

namespace PlatformerMVC
{
    public interface IQuest : IDisposable
    {
        event EventHandler<IQuest> QuestCompleted;

        bool IsCompleted { get; }

        List<string> ItemIds { get; }

        void Reset();
    }
}
