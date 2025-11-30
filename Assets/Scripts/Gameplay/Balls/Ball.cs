using System;
using Jagerwil.Core.Architecture;
using Jagerwil.Extensions;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

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
            if (_rigidbody.linearVelocity.ApproximatelyZero()) {
                return;
            }
            
            var moveDirection = GetMoveDirectionAfterCollision(other.contacts[0].normal);
            RecalculateVelocity(moveDirection);
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
            _rigidbody.simulated = true;
            _isMoving = true;

            //_ballPhysics.SetMoveDirection(direction);
            RecalculateVelocity(direction);
            
            _collider.enabled = true;
            transform.SetParent(_defaultParent);
        }

        public void Despawn() {
            onDespawnRequested?.Invoke(this);
        }

        private Vector2 GetMoveDirectionAfterCollision(Vector2 normal) {
            var velocity = _rigidbody.linearVelocity;
            if (velocity.ApproximatelyZero()) {
                return velocity;
            }
            
            velocity = ValidateBounceFromObject(velocity, normal);

            var velocitySignX = Mathf.Sign(velocity.x);
            var normalSignY = Mathf.Sign(normal.y);

            if (Mathf.Abs(velocity.y) < 0.01f) {
                return new Vector2(velocitySignX, 0.01f * normalSignY).normalized;
            }

            return velocity;
        }

        private Vector2 ValidateBounceFromObject(Vector2 velocity, Vector2 normal) {
            var dotProduct = Vector2.Dot(normal, velocity);
            //if dot product is >= 0f, it means that ball already bounced out of the collided object
            if (dotProduct >= 0f) {
                return velocity;
            }

            var angle = Vector2.SignedAngle(velocity, normal);
            var rotateAngle = 2f * (angle - 90f);
            return Quaternion.Euler(0f, 0f, rotateAngle) * velocity;
        }

        private void RecalculateVelocity(Vector2 moveDirection) {
            if (!_isMoving || moveDirection.ApproximatelyZero()) {
                return;
            }
            
            var angle = Vector2.SignedAngle(Vector2.right, moveDirection);
            var sign = Mathf.Sign(angle);

            if (Mathf.Abs(angle) < _angleCheckThreshold) {
                RecalculateVelocityFromAngle(sign * _minHorizontalAngle);
                return;
            }

            if (angle < 0f) {
                angle += 360f;
            }

            if (Mathf.Abs(angle - 180f) < _angleCheckThreshold) {
                RecalculateVelocityFromAngle(sign * (180f - _minHorizontalAngle));
                return;
            }
            
            _rigidbody.linearVelocity = moveDirection.normalized * _shootSpeed;
        }

        private void RecalculateVelocityFromAngle(float angle) {
            var resultAngle = Quaternion.Euler(0f, 0f, angle);
            _rigidbody.linearVelocity = resultAngle * (Vector3.right * _shootSpeed);
        }
    }
}
