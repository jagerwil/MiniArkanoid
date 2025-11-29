using System;
using Jagerwil.Core.Architecture;
using Jagerwil.Extensions;
using UnityEngine;

namespace Game.Gameplay.Balls {
    public class Ball : MonoBehaviour, IPoolableObject<Ball> {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private float _shootSpeed = 5f;
        [Space]
        [SerializeField] private float _minHorizontalAngle = 10f;
        [SerializeField] private float _angleCheckThreshold = 9f;

        private Transform _defaultParent;
        private bool _isMoving;

        public Vector2 Direction => _rigidbody.linearVelocity.normalized;
        public event Action<Ball> onDespawnRequested;

        private void OnCollisionEnter2D(Collision2D other) {
            ValidateVelocityAngle();
        }

        public void OnSpawned() {
            _collider.enabled = false;
            _rigidbody.simulated = false;
            _isMoving = false;
        }
        
        public void OnDespawned() { }

        public void Initialize(Transform defaultRoot) {
            _defaultParent = defaultRoot;
        }

        public void Shoot(Vector2 direction) {
            _collider.enabled = true;
            _rigidbody.simulated = true;
            _isMoving = true;
            
            _rigidbody.linearVelocity = direction.normalized * _shootSpeed;
            ValidateVelocityAngle();
            
            transform.SetParent(_defaultParent);
        }

        public void Despawn() {
            onDespawnRequested?.Invoke(this);
        }

        private void ValidateVelocityAngle() {
            if (!_isMoving || _rigidbody.linearVelocity.ApproximatelyZero()) {
                return;
            }
            
            var angle = Vector2.SignedAngle(Vector2.right, _rigidbody.linearVelocity);
            var sign = Mathf.Sign(angle);

            if (Mathf.Abs(angle) < _angleCheckThreshold) {
                var deltaAngle = sign * _minHorizontalAngle - angle;
                _rigidbody.linearVelocity = Quaternion.Euler(0f, 0f, deltaAngle) * _rigidbody.linearVelocity;
                Debug.Log($"BALL ANGLE CORRECTION: Angle: {angle} => {sign * _minHorizontalAngle}");
                return;
            }

            if (angle < 0f) {
                angle += 360f;
            }

            if (Mathf.Abs(angle - 180f) < _angleCheckThreshold) {
                var resultAngle = sign * (180f - _minHorizontalAngle) - angle;
                _rigidbody.linearVelocity = Quaternion.Euler(0f, 0f, resultAngle) * _rigidbody.linearVelocity;
                Debug.Log($"BALL ANGLE CORRECTION: Angle: {angle} => {sign * (180f - _minHorizontalAngle)}");
            }
        }
    }
}
