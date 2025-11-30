using System;
using Game.Configs;
using Jagerwil.Extensions;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Platforms {
    public class PlatformHorizontalMovement : MonoBehaviour {
        [SerializeField] private Rigidbody2D _rigidbody;

        private PlatformMovementInfo _info;
        private bool _hasTargetPosition;
        private float _targetPositionX;

        [Inject]
        private void Inject(PlatformConfig platformConfig) {
            _info = platformConfig.Movement;
        }

        private void FixedUpdate() {
            if (!_hasTargetPosition) {
                return;
            }
            
            var deltaPos = (_targetPositionX - _rigidbody.position.x);
            if (deltaPos.ApproximatelyZero()) {
                _rigidbody.linearVelocityX = 0f;
                return;
            }
            
            //We change linear velocity instead of using MovePosition because we want
            //a proper velocity for recalculating the ball angle when colliding with a platform
            _rigidbody.linearVelocityX = deltaPos / Time.fixedDeltaTime;
        }

        public void SetMoveAxis(float moveAxis) {
            _hasTargetPosition = false;
            _rigidbody.linearVelocityX = moveAxis * _info.MoveSpeed;
        }

        public void SetMoveTargetPosition(float targetPositionX) {
            _hasTargetPosition = true;
            _targetPositionX = targetPositionX;
        }
    }
}
