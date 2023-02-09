using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PlatformerMVC
{
    public class QuestStoryController : IQuestStory
    {
        public event Action<IQuestStory> QuestStoryCompleted;


        private List<IQuest> _questList = new List<IQuest>();

        public bool IsDone => _questList.All(value => value.IsCompleted);

        public List<IQuest> QuestList { get => _questList; private set => _questList = value; }

        private int index;

        private bool isOrdered;

        private string questStoryId;

        private QuestStoryType _questStoryType;

        public string QuestStoryId { get => questStoryId; set => questStoryId = value; }


        public QuestStoryController(List<IQuest> questList, QuestStoryType questStoryType)
        {
            _questList = questList;
            _questStoryType = questStoryType;
            Reset();
        }

        private void OnQuestCompleted(object sender, IQuest quest)
        {
            if (!_questList.Contains(quest)) return;
            if (index >= _questList.Count)
            {
                Reset();
            }

            isOrdered = quest == _questList[index] && isOrdered;
            quest.QuestCompleted -= OnQuestCompleted;
            index++;

            if (IsDone)
            {
                if (_questStoryType == QuestStoryType.Resettable)
                {
                    if (!isOrdered)
                    {
                        Reset();
                    }
                    else
                    {
                        QuestStoryCompleted?.Invoke(this);
                    }
                }
                else
                {
                    QuestStoryCompleted?.Invoke(this);
                }
            }
        }

        private void Reset()
        {
            foreach (var quest in _questList)
            {
                quest.Reset();
                quest.QuestCompleted += OnQuestCompleted;
            }
            index = 0;
            isOrdered = true;
        }

        public void Dispose()
        {
            foreach (IQuest quest in _questList)
            {
                quest.QuestCompleted += OnQuestCompleted;
                quest.Dispose();
            }
        }
    }
}
