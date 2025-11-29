using System;
using Game.Gameplay.Balls;
using UnityEngine;

namespace Game.Gameplay.Platforms {
    public class PlatformBallCollider : MonoBehaviour {
        [SerializeField] private Vector2 _platformSize;
        [SerializeField] private float _deltaAngleAtEdges = 20f;
        [SerializeField] private float _deltaAngleSpeedMultiplier = 0.2f;

        private Vector3 _prevPosition;
        private float _currentSpeed;

        private void Awake() {
            _prevPosition = transform.position;
            _currentSpeed = 0f;
        }

        private void Update() {
            _currentSpeed = (transform.position.x - _prevPosition.x) / Time.deltaTime;
            _prevPosition = transform.position;
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
                direction.y *= -1f;
                return;
            }

            var platformOffset = (other.contacts[0].point.x - transform.position.x);
            var positionOffsetAngle = -1f * _deltaAngleAtEdges * (platformOffset / (_platformSize.x * 0.5f));
            var velocityOffsetVector = Vector2.right * (_currentSpeed * _deltaAngleSpeedMultiplier);
            
            var newDirection = (Vector2)(Quaternion.Euler(0f, 0f, positionOffsetAngle) * direction) + velocityOffsetVector;
            ball.Shoot(newDirection);
        }
    }
}
