using System;
using UnityEngine;

namespace Game.Gameplay.Bricks {
    public class Brick : MonoBehaviour, IDamageable {
        [SerializeField] private float _maxHealth;

        private float _health;

        private Action _destroyedCallback;

        private void Awake() {
            _health = _maxHealth;
        }

        public void Initialize(Action destroyedCallback) {
            _destroyedCallback = destroyedCallback;
        }

        public void Restore() {
            _health = _maxHealth;
            gameObject.SetActive(true);
        }

        public void TakeDamage(float damage) {
            _health -= damage;
            if (_health <= 0) {
                gameObject.SetActive(false);
                _destroyedCallback?.Invoke();
            }
        }
    }
}
