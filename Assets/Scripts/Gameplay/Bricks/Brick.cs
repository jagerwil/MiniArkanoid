using System;
using System.Collections.Generic;
using System.Linq;
using Game.Gameplay._Services;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Bricks {
    public class Brick : MonoBehaviour, IDamageable {
        [SerializeField] private float _maxHealth;
        [SerializeField] private int _score;
        [Space]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private List<BrickHealthGraphics> _graphics;
        
        [Inject] private IScoreService _scoreService;

        private float _health;

        private Action _destroyedCallback;

        public void Initialize(Action destroyedCallback) {
            _destroyedCallback = destroyedCallback;
        }
        
#if UNITY_EDITOR
        //Dynamically update the graphics in the editor
        private void OnValidate() {
            SetHealth(_maxHealth);
        }
#endif

        public void Restore() {
            SetHealth(_maxHealth);
            gameObject.SetActive(true);
        }

        public void TakeDamage(float damage) {
            SetHealth(_health - damage);
            if (_health > 0f) {
                return;
            }

            gameObject.SetActive(false);
            _scoreService.ChangeScore(_score);
            _destroyedCallback?.Invoke();
        }

        private void SetHealth(float health) {
            _health = Mathf.Max(health, 0f);
            var healthPercent = _health / _maxHealth;
            
            var graphicsInfo = _graphics.LastOrDefault(graphics => graphics.StartHealthPercent >= healthPercent);
            if (graphicsInfo == null) {
                Debug.LogError($"Could not find the graphics for health percent {healthPercent}");
                return;
            }
            
            _spriteRenderer.sprite = graphicsInfo.Sprite;
            _spriteRenderer.color = graphicsInfo.Color;
        }
    }

    [Serializable]
    public class BrickHealthGraphics {
        [field: Range(0f, 1f)]
        [field: SerializeField] public float StartHealthPercent { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public Color Color { get; private set; }
    }
}
