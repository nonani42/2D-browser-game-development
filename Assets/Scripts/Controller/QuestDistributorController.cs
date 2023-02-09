using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class QuestDistributorController
    {
        private PlayerController _player;
        private List<IQuestStory> _questStoriesList;

        private DoorController[] _doorControllers;
        private ChestController[] _chestControllers;


        public QuestDistributorController(PlayerController player, List<IQuestStory> questStoryList, DoorController[] doorControllers, ChestController[] chestControllers)
        {
            _player = player;
            _questStoriesList = new List<IQuestStory>();
            _questStoriesList = questStoryList;
            _doorControllers = doorControllers;
            _chestControllers = chestControllers;
            Subscribe();
        }

        private void Subscribe()
        {
            for (int i = 0; i < _questStoriesList.Count; i++)
            {
                for (int j = 0; j < _doorControllers.Length; j++)
                {
                    if(_doorControllers[j].QuestStoryId == _questStoriesList[i].QuestStoryId)
                    {
                        _questStoriesList[i].QuestStoryCompleted += _doorControllers[j].Unlock;
                    }
                }
                for (int j = 0; j < _chestControllers.Length; j++)
                {
                    if (_chestControllers[j].QuestStoryId == _questStoriesList[i].QuestStoryId)
                    {
                        _questStoriesList[i].QuestStoryCompleted += _chestControllers[j].Unlock;
                    }
                }
            }
        }
    }
}
