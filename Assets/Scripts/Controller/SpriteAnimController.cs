using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerMVC
{
    public class SpriteAnimController : IDisposable
    {

        private sealed class Animation
        {
            public AnimState Track;
            public List<Sprite> Sprites;
            public bool Loop;
            public float Speed = 10f;
            public float Counter = 0f;
            public bool Sleep;

            public void Update()
            {
                if (Sleep) return;

                Counter += Time.deltaTime * Speed;

                if (Loop)
                {
                    while(Counter > Sprites.Count)
                    {
                        Counter -= Sprites.Count;
                    }
                } 
                else if (Counter > Sprites.Count)
                {
                    Counter = Sprites.Count;
                    Sleep = true;
                }
            }
        }

        private AnimationConfig _config;
        private Dictionary<SpriteRenderer, Animation> _activeAnimations = new Dictionary<SpriteRenderer, Animation>();

        public SpriteAnimController(AnimationConfig config)
        {
            _config = config;
        }

        public void StartAnimation(SpriteRenderer spriteRenderer, AnimState track, bool loop, float speed)
        {
            if(_activeAnimations.TryGetValue(spriteRenderer, out Animation animation))
            {
                animation.Loop = loop;
                animation.Speed = speed;
                animation.Sleep = false;

                if (animation.Track != track)
                {
                    animation.Track = track;
                    animation.Sprites = _config.Sequences.Find(sequence => sequence.Track == track).Sprites;
                    animation.Counter = 0f;
                }
            }
            else
            {
                _activeAnimations.Add(spriteRenderer, new Animation()
                {
                    Track = track,
                    Sprites = _config.Sequences.Find(sequence => sequence.Track == track).Sprites,
                    Loop = loop,
                    Speed = speed,
                    Counter = 0f,
                    Sleep = false,
                });
            }
        }

        public void StopAnimation(SpriteRenderer spriteRenderer)
        {
            if (_activeAnimations.TryGetValue(spriteRenderer, out Animation animation))
            {
                _activeAnimations.Remove(spriteRenderer);
            }
        }

        public void Update()
        {
            foreach(var anim in _activeAnimations)
            {
                anim.Value.Update();
                if(anim.Value.Counter < anim.Value.Sprites.Count)
                {
                    anim.Key.sprite = anim.Value.Sprites[(int)anim.Value.Counter];
                }
            }
        }

        public void Dispose()
        {
            _activeAnimations.Clear();
        }
    }
}
