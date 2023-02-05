using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class DestroyableObjectsView : InteractiveObjectView
    {
        [SerializeField] private int _damagePoint;
        [SerializeField] private int _healthPoint;

        public int DamagePoint { get => _damagePoint; set => _damagePoint = value; }
        public int HealthPoint { get => _healthPoint; set => _healthPoint = value; }
    }
}
