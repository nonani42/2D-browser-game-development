using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class QuestConfiguratorController
    {
        private DestroyableObjectsView _playerView;
        private QuestView _questView;

        private QuestConfig[] _quest—onfigs;
        private QuestItemConfig[] _questItem—onfigs;
        private QuestStoryConfig[] _questStoryConfigs;

        private QuestObjectView[] _questObjectViews;


        private List<IQuestStory> _questStoryList;
        //private List<IQuest> _questList;


        private Dictionary<QuestType, Func<IQuestModel>> _singleQuestDic = new Dictionary<QuestType, Func<IQuestModel>>(10);
        //private Dictionary<QuestStoryType, Func<List<IQuest>, IQuestStory>> _storyQuestDic = new Dictionary<QuestStoryType, Func<List<IQuest>, IQuestStory>>(10);
        
        
        public List<IQuestStory> QuestStoryList { get => _questStoryList; set => _questStoryList = value; }

        public QuestConfiguratorController(QuestView questView, DestroyableObjectsView playerView)
        {
            _questView = questView;
            _playerView = playerView;

            _quest—onfigs = _questView.QuestConfigs;
            _questItem—onfigs = _questView.QuestItemConfigs;
            _questStoryConfigs = _questView.StoryQuestConfigs;

            _questObjectViews = _questView.QuestObjects;
            Start();
        }

        public void Start()
        {
            _singleQuestDic.Add(QuestType.Coins, () => new QuestIdModel());
            _singleQuestDic.Add(QuestType.Buttons, () => new QuestIdModel());

            //_storyQuestDic.Add(QuestStoryType.Common, questCollection => new QuestStoryController(questCollection));

            //_questList = new List<IQuest>();
            //foreach (QuestConfig config in _quest—onfigs)
            //{
            //    if (_quest—onfigs == null) continue;
            //    IQuest quest = CreateQuest(config);
            //    _questList.Add(quest);
            //    quest.Reset();
            //}

            _questStoryList = new List<IQuestStory>();
            foreach (QuestStoryConfig config in _questStoryConfigs)
            {
                if (_quest—onfigs == null) continue;
                _questStoryList.Add(CreateQuestStory(config));
            }
        }

        private IQuestStory CreateQuestStory(QuestStoryConfig config)
        {
            List<IQuest> quests = new List<IQuest>();

            foreach (QuestConfig questConfig in config.questsConfig)
            {
                IQuest quest = CreateQuest(questConfig);
                if (quest == null)
                {
                    Debug.LogWarning("No quest created!");
                    continue;
                }
                quests.Add(quest);
            }
            QuestStoryController questStoryController = new QuestStoryController(quests, config.questStoryType);
            questStoryController.QuestStoryId = config.questStoryId;
            return questStoryController;
        }

        private IQuest CreateQuest(QuestConfig config)
        {
            List<QuestObjectView> itemViews = new List<QuestObjectView>();
            QuestItemConfig questItemsConfig = _questItem—onfigs.FirstOrDefault(value => value.questId == config.questId);
            if(questItemsConfig != null)
            {
                for (int i = 0; i < questItemsConfig.questItemId.Count; i++)
                {
                    itemViews.Add(_questObjectViews.FirstOrDefault(value => value.Id == questItemsConfig.questItemId[i]));
                }
                if (_singleQuestDic.TryGetValue(config.type, out var factory))
                {
                    IQuestModel model = factory.Invoke();
                    return new QuestController(_playerView, itemViews, model);
                }
                Debug.LogWarning("No matching model!");
                return null;
            }
            Debug.LogWarning("No matching ID!");
            return null;
        }
    }
}
