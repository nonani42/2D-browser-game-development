using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class EmitterController
    {
        private List<BulletController> _bulletControllers = new List<BulletController>();
        private Transform _emitterTransform;

        private int _index;
        private float _timeTillNextBull;
        private float _startSpeed = 15f;
        private float _delay = 2f;

        public EmitterController(Transform emitterTransform, List<BulletView> bulletViews)
        {
            _emitterTransform = emitterTransform;

            for (int i = 0; i < bulletViews.Count; i++)
            {
                _bulletControllers.Add(new BulletController(bulletViews[i]));
            }
        }

        public void Update()
        {
            if (_timeTillNextBull > 0)
            {
                _bulletControllers[_index].Active(false);
                _timeTillNextBull -= Time.deltaTime;
            }
            else
            {
                _timeTillNextBull = _delay;
                _bulletControllers[_index].Throw(_emitterTransform.position, -_emitterTransform.up * _startSpeed);
                _index++;
                if (_index >= _bulletControllers.Count)
                {
                    _index = 0;
                }
            }
        }
    }
}
