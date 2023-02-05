using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class EnemyView : LevelObjectView
    {
        [SerializeField] private int _damagePoint;

        public int DamagePoint { get => _damagePoint; set => _damagePoint = value; }
    }
}
