using UnityEngine;
using Zenject;

namespace Game.Gameplay.Balls {
    public class Ball : MonoBehaviour, IPoolable {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _shootSpeed = 5f;
        
        public void OnSpawned() {
            Debug.Log("Spawned!");
        }
        
        public void OnDespawned() {
            Debug.Log("Despawned!");
        }

        public void Shoot(Vector2 direction) {
            _rigidbody.linearVelocity = direction * _shootSpeed;
        }
    }
}
