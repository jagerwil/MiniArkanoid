using UnityEngine;

namespace Game.Gameplay.Bricks {
    public class Brick : MonoBehaviour, IDamageable {
        [SerializeField] private float _maxHealth;

        private float _health;

        private void Awake() {
            _health = _maxHealth;
        }

        public void TakeDamage(float damage) {
            _health -= damage;
            if (_health <= 0) {
                gameObject.SetActive(false);
            }
        }
    }
}
