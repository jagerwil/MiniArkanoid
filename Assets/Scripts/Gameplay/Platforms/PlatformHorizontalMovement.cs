using System;
using Jagerwil.Extensions;
using UnityEngine;

namespace Game.Gameplay.Platforms {
    public class PlatformHorizontalMovement : MonoBehaviour {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _moveSpeed = 5f;

        private bool _hasTargetPosition;
        private float _targetPositionX;

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
            _rigidbody.linearVelocityX = moveAxis * _moveSpeed;
        }

        public void SetMoveTargetPosition(float targetPositionX) {
            _hasTargetPosition = true;
            _targetPositionX = targetPositionX;
        }
    }
}
