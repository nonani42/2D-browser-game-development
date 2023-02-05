using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class EnemyPatrolView : DestroyableObjectsView
    {
        [SerializeField] private Transform[] _patrolPoints;

        public Transform[] PatrolPoints { get => _patrolPoints; set => _patrolPoints = value; }
    }
}
