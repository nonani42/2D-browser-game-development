using UnityEngine;

namespace PlatformerMVC
{
    public class QuestObjectView : InteractiveObjectView
    {
        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _inactiveColor;

        [SerializeField] private int _points;
        [SerializeField] private string _id;


        public int Points { get => _points; private set => _points = value; }
        public string Id { get => _id; private set => _id = value; }

        private void Awake()
        {
            gameObject.name = _id;
        }

        public void ProcessComplete()
        {
            _spriteRenderer.color = _inactiveColor;
            _collider.enabled = false;
        }

        public void ProcessActivate()
        {
            _spriteRenderer.color = _activeColor;
            _collider.enabled = true;
        }
    }
}
