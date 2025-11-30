using System;
using Game.Gameplay.Balls;
using UnityEngine;

namespace Game.Gameplay.Platforms {
    public class PlatformBallCollider : MonoBehaviour {
        [SerializeField] private Vector2 _platformSize;
        [SerializeField] private float _deltaAngleAtEdges = 20f;
        [SerializeField] private float _deltaAngleSpeedMultiplier = 0.2f;
        [SerializeField] private Rigidbody2D _rigidbody;

        private float _currentVelocityX;
        private float _previousVelocityX;

        private void FixedUpdate() {
            _previousVelocityX = _currentVelocityX;
            _currentVelocityX = _rigidbody.linearVelocityX;
        }

        private void OnCollisionEnter2D(Collision2D other) {
            var ball = other.gameObject.GetComponent<Ball>();
            if (!ball) {
                return;
            }
            
            //Ignore the ball that is under platform
            if (other.transform.position.y <= transform.position.y) {
                return;
            }

            var direction = ball.Direction;
            if (direction.y < 0f) {
                return;
            }

            var platformOffset = (other.contacts[0].point.x - _rigidbody.position.x);
            var positionOffsetAngle = -1f * _deltaAngleAtEdges * (platformOffset / (_platformSize.x * 0.5f));
            var velocityOffsetVector = Vector2.right * (_previousVelocityX * _deltaAngleSpeedMultiplier);
            
            var newDirection = (Vector2)(Quaternion.Euler(0f, 0f, positionOffsetAngle) * direction) + velocityOffsetVector;
            if (newDirection.y < 0f) {
                newDirection.y = 0.01f;
            }
            
            ball.Shoot(newDirection);
        }
    }
}
