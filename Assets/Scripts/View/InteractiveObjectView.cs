using System;
using UnityEngine;

namespace PlatformerMVC
{
    public class InteractiveObjectView : LevelObjectView
    {
        public Action<DestroyableObjectsView> TakeDamage { get; set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out DestroyableObjectsView contactView))
            {
                TakeDamage?.Invoke(contactView);
            }
        }
    }
}
