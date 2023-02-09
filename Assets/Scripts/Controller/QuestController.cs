using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class QuestController : IQuest
    {
        private DestroyableObjectsView _playerView;
        private List<QuestObjectView> _questView = new List<QuestObjectView>();
        private IQuestModel _questModel;
        private bool _isActive;

        public event EventHandler<IQuest> QuestCompleted;

        public bool IsCompleted { get; private set; }
        public List<string> ItemIds { get; private set; }

        public QuestController(DestroyableObjectsView playerView, List<QuestObjectView> questView, IQuestModel questModel)
        {
            _playerView = playerView;
            _questView = questView;
            _questModel = questModel;
            _isActive = false;
            ItemIds = new List<string>();

            foreach (QuestObjectView objects in _questView)
            {
                ItemIds.Add(objects.Id);
            }
        }

        public void Reset()
        {
            if (_isActive) return;
            _isActive = true;
            IsCompleted = false;
            _playerView.QuestComplete += OnContact;
            foreach (QuestObjectView objects in _questView)
            {
                objects.ProcessActivate();
            }
        }

        public void OnContact(QuestObjectView questItem)
        {
            if (questItem != null && _questView.Contains(questItem))
            {
                questItem.ProcessComplete();
                if (_questModel.TryComplete(questItem, this))
                {
                    {
                        Completed();
                    }
                }
            }
        }

        private void Completed()
        {
            if (!_isActive) return;
            _isActive = false;
            _playerView.QuestComplete -= OnContact;
            IsCompleted = true;
            QuestCompleted?.Invoke(this, this);
        }

        public void Dispose()
        {
            _playerView.QuestComplete -= OnContact;
        }
    }
}
