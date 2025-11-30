using System;
using R3;
using UnityEngine;

namespace Game.Gameplay.Platforms {
    public class PlatformSize : MonoBehaviour {
        [SerializeField] private Vector2 _initialSize;
        [SerializeField] private BoxCollider2D _collider;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private readonly ReactiveProperty<Vector2> _size = new();

        public ReadOnlyReactiveProperty<Vector2> Size => _size;

        private void Awake() {
            SetSize(_initialSize);
        }
        
#if UNITY_EDITOR
        private void OnValidate() {
            SetSize(_initialSize);
        }
#endif

        public void SetSize(Vector2 size) {
            _size.Value = size;
            _collider.size = size;
            _spriteRenderer.size = _collider.size;
        }
    }
}
