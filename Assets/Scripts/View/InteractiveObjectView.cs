using System;
using UnityEngine;

namespace PlatformerMVC
{
    public class InteractiveObjectView : LevelObjectView
    {
        public Action<EnemyView> TakeDamage { get; set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out EnemyView contactView))
            {
                TakeDamage?.Invoke(contactView);
            }
        }
    }
}
