using Game.Configs;
using Game.Gameplay.Balls;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Platforms {
    public class PlatformBallRedirection : MonoBehaviour {
        [SerializeField] private PlatformSize _platformSize;
        [SerializeField] private Rigidbody2D _rigidbody;

        private PlatformBallRedirectionInfo _info;
        private float _currentVelocityX;
        private float _previousVelocityX;

        [Inject]
        private void Inject(PlatformConfig platformConfig) {
            _info = platformConfig.BallRedirection;
        }

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

            var platformSize = _platformSize.Size.CurrentValue;
            var platformOffset = (other.contacts[0].point.x - _rigidbody.position.x);
            
            var positionOffsetAngle = -1f * _info.DeltaAngleAtEdges * (platformOffset / (platformSize.x * 0.5f));
            var velocityOffsetVector = Vector2.right * (_previousVelocityX * _info.DeltaAngleSpeedMultiplier);
            
            var newDirection = (Vector2)(Quaternion.Euler(0f, 0f, positionOffsetAngle) * direction) + velocityOffsetVector;
            if (newDirection.y < 0f) {
                newDirection.y = 0.01f;
            }
            
            ball.Shoot(newDirection);
        }
    }
}
