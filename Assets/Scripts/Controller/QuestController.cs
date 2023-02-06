using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class QuestController : IQuest
    {
        private DestroyableObjectsView _playerView;
        private QuestObjectView _questView;
        private IQuestModel _questModel;
        private bool _isActive;

        public event EventHandler<IQuest> QuestCompleted;

        public bool IsCompleted { get; private set; }

        public QuestController(DestroyableObjectsView playerView, QuestObjectView questView, IQuestModel questModel)
        {
            _playerView = playerView;
            _questView = questView;
            _questModel = questModel;
            _isActive = false;
        }

        public void Reset()
        {
            if (_isActive) return;
            _isActive = true;
            _playerView.QuestComplete += OnContact;
            _questView.ProcessActivate();
        }

        public void OnContact(QuestObjectView questItem)
        {
            if (questItem != null)
            {
                if (_questModel.TryComplete(questItem.gameObject))
                {
                    if(questItem == _questView)
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
            _questView.ProcessComplete();
            QuestCompleted?.Invoke(this, this);
        }

        public void Dispose()
        {
            _playerView.QuestComplete -= OnContact;
        }
    }
}
