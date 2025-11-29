using System;
using UnityEngine;

namespace Game.Gameplay.Balls {
    public class BallDamage : MonoBehaviour {
        [SerializeField] private float _damage;

        private void OnCollisionEnter2D(Collision2D other) {
            var damageable = other.gameObject.GetComponent<IDamageable>();
            damageable?.TakeDamage(_damage);
        }
    }
}
