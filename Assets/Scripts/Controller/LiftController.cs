using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class LiftController
    {
        private LevelObjectView[] _liftViews;

        private Rigidbody2D[] _liftRbs;
        private Collider2D[] _liftColliders;

        private ContactPooler[] _contactPoolers;

        private float speed;

        private float[] _directions;

        public LiftController(LevelObjectView[] liftViews)
        {
            _liftViews = liftViews;
            _liftRbs = new Rigidbody2D[_liftViews.Length];
            _directions = new float[_liftViews.Length];
            _liftColliders = new Collider2D[_liftViews.Length];
            _contactPoolers = new ContactPooler[_liftViews.Length];
            for (int i = 0; i < _liftRbs.Length; i++)
            {
                _liftRbs[i] = _liftViews[i]._rb;
                _directions[i] = 1f;
                _liftColliders[i] = _liftViews[i]._collider;
                _contactPoolers[i] = new ContactPooler(_liftColliders[i]);
            }
            speed = 40f;

        }

        public void Update()
        {
            Move();

            for (int i = 0; i < _liftViews.Length; i++)
            {
                _contactPoolers[i].Update();

                if (_contactPoolers[i].LeftContact)
                {
                    _directions[i] = 1;
                }

                if (_contactPoolers[i].RightContact)
                {
                    _directions[i] = -1;
                }
            }
        }

        private void Move()
        {
            for (int i = 0; i < _liftRbs.Length; i++)
            {
                _liftRbs[i].AddForce(_directions[i] * speed * Time.deltaTime * Vector3.right, ForceMode2D.Force);
            }
        }
    }
}
