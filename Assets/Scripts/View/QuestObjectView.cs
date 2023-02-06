using UnityEngine;

namespace PlatformerMVC
{
    public class QuestObjectView : LevelObjectView
    {
        private Color _activeColor;
        [SerializeField] private Color _inactiveColor;

        private void Awake()
        {
            _activeColor = _spriteRenderer.color;
        }

        public void ProcessComplete()
        {
            _spriteRenderer.color = _inactiveColor;
            //_collider.enabled = false;
        }

        public void ProcessActivate()
        {
            _spriteRenderer.color = _activeColor;
            //_collider.enabled = true;
        }
    }
}
