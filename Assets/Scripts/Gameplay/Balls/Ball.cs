using System;
using Jagerwil.Core.Architecture;
using UnityEngine;

namespace Game.Gameplay.Balls {
    public class Ball : MonoBehaviour, IPoolableObject<Ball> {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _shootSpeed = 5f;

        private Transform _defaultParent;

        public event Action<Ball> onDespawnRequested;
        
        public void OnSpawned() {
            Debug.Log("Spawned!");
        }
        
        public void OnDespawned() {
            Debug.Log("Despawned!");
        }

        public void Initialize(Transform defaultRoot) {
            _defaultParent = defaultRoot;
        }

        public void Shoot(Vector2 direction) {
            _rigidbody.linearVelocity = direction * _shootSpeed;
            transform.SetParent(_defaultParent);
        }

        public void Despawn(bool force = false) {
            onDespawnRequested?.Invoke(this);
        }
    }
}
