using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class BulletView : EnemyView
    {
        [SerializeField] private TrailRenderer _trailRenderer;

        public TrailRenderer TrailRenderer { get => _trailRenderer; set => _trailRenderer = value; }
    }
}
