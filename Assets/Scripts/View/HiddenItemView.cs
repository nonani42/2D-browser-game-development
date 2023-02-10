using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class HiddenItemView : LockedItemView
    {
        public void Start()
        {
            Loot.ProcessComplete();
        }

        public void OnInteraction()
        {
            Loot.ProcessActivate();
        }
    }
}
