using System;
using Jagerwil.Extensions;
using UnityEngine;

namespace Game.Gameplay.Platforms {
    public class PlatformHorizontalMovement : MonoBehaviour {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _moveSpeed = 5f;

        private float _moveAxis;
        private float _blockedMovementSign;

        private void FixedUpdate() {
            if (_moveAxis * _blockedMovementSign > 0f) {
                _rigidbody.linearVelocityX = 0f;
                return;
            }
            
            _rigidbody.linearVelocityX = _moveAxis * _moveSpeed;
        }

        public void SetMoveAxis(float moveAxis) {
            _moveAxis = moveAxis;
        }

        private void OnCollisionEnter2D(Collision2D other) {
            var gameBorder = other.gameObject.GetComponent<GameBorder>();
            if (!gameBorder) {
                return;
            }
            
            var contactNormal = other.contacts[0].normal;
            _blockedMovementSign = -1f * Mathf.Sign(contactNormal.x);
        }

        private void OnCollisionExit2D(Collision2D other) {
            var gameBorder = other.gameObject.GetComponent<GameBorder>();
            if (!gameBorder) {
                return;
            }
            
            _blockedMovementSign = 0f;
        }
    }
}
