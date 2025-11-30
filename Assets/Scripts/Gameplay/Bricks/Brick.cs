using System;
using Game.Gameplay._Services;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Bricks {
    public class Brick : MonoBehaviour, IDamageable {
        [SerializeField] private float _maxHealth;
        [SerializeField] private int _score;
        
        [Inject] private IScoreService _scoreService;

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
            if (_health > 0f) {
                return;
            }

            gameObject.SetActive(false);
            _scoreService.ChangeScore(_score);
            _destroyedCallback?.Invoke();
        }
    }
}
