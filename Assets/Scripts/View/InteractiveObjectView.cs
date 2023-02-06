using System;
using UnityEngine;

namespace PlatformerMVC
{
    public class InteractiveObjectView : LevelObjectView
    {
        public Action<DestroyableObjectsView> TakeDamage { get; set; }
        public Action<QuestObjectView> QuestComplete { get; set; }
        public Action<InteractiveObjectView> Interact { get; set; }



        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out LevelObjectView contactView))
            {
                if(contactView is DestroyableObjectsView)
                {
                    TakeDamage?.Invoke((DestroyableObjectsView)contactView);
                }

                if (contactView is QuestObjectView)
                {
                    QuestComplete?.Invoke((QuestObjectView)contactView);
                }


                if (contactView is InteractiveObjectView)
                {
                    Interact?.Invoke((InteractiveObjectView)contactView);
                }
            }
        }
    }
}
