using System;
using UnityEngine;

namespace PlatformerMVC
{
    public class InteractiveObjectView : LevelObjectView
    {
        public Action<DestroyableObjectsView> TakeDamage { get; set; }
        public Action<QuestObjectView> QuestComplete { get; set; }
        public Action<QuestObjectView> PickUpCoin { get; set; }
        public Action<LevelObjectView> LevelCompleted { get; set; }


        bool flag = false;
        QuestObjectView player;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out LevelObjectView contactView))
            {
                if (contactView is DestroyableObjectsView)
                {
                    TakeDamage?.Invoke((DestroyableObjectsView)contactView);
                }

                if (contactView is QuestObjectView)
                {
                    player = (QuestObjectView)contactView;
                    flag = true;

                    if (contactView.CompareTag("QuestCoin"))
                    {
                        PickUpCoin?.Invoke((QuestObjectView)contactView);
                        QuestComplete?.Invoke((QuestObjectView)contactView);
                    }
                }
                if (contactView.CompareTag("LevelCompletion"))
                {
                    LevelCompleted?.Invoke(contactView);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            flag = false;
            player = null;
        }

        private void Update()
        {
            if(flag && Input.GetKeyDown(KeyCode.E))
            {
                QuestComplete?.Invoke(player);
            }
        }
    }
}
