using UnityEngine;

namespace PlatformerMVC
{
    public class CoinController
    {
        private QuestObjectView _coinView;
        private SpriteAnimController _coinAnimator;
        private AnimationConfig _config;
        private SpriteRenderer _coinSpriteRenderer;

        private float _speed = 14f;

        public CoinController(QuestObjectView questObjectView)
        {
            _coinView = questObjectView;

            _config = Resources.Load<AnimationConfig>("CoinAnimConfig");
            _coinAnimator = new SpriteAnimController(_config);
            _coinSpriteRenderer = _coinView._spriteRenderer;
            _coinAnimator.StartAnimation(_coinSpriteRenderer, AnimState.Idle, true, _speed);
        }

        public void Update()
        {
            _coinAnimator.Update();
        }
    }
}
