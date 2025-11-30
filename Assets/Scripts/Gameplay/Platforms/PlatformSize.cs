using Game.Configs;
using R3;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Platforms {
    public class PlatformSize : MonoBehaviour {
        [SerializeField] private BoxCollider2D _collider;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private PlatformSizeInfo _info;
        private readonly ReactiveProperty<Vector2> _size = new();

        public ReadOnlyReactiveProperty<Vector2> Size => _size;

        [Inject]
        private void Inject(PlatformConfig platformConfig) {
            _info = platformConfig.Size;
            SetSize(_info.Size);
        }

        private void SetSize(Vector2 size) {
            _size.Value = size;
            _collider.size = size;
            _spriteRenderer.size = _collider.size;
        }
    }
}
