using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class BulletView : LevelObjectView
    {
        [SerializeField] private TrailRenderer _trailRenderer;
        [SerializeField] private int _damagePoint = 10;

        public int DamagePoint { get => _damagePoint; set => _damagePoint = value; }
        public TrailRenderer TrailRenderer { get => _trailRenderer; set => _trailRenderer = value; }
    }
}
