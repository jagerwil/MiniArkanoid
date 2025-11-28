using System;
using UnityEngine;

namespace Game.Gameplay.Platform {
    public class PlatformHorizontalMovement : MonoBehaviour {
        [SerializeField] private float _moveSpeed = 5f;
        
        private float _moveAxis;

        private void Update() {
            transform.Translate(Vector3.right * (_moveAxis * _moveSpeed * Time.deltaTime));
        }

        public void SetMoveAxis(float moveAxis) {
            _moveAxis = moveAxis;
        }
    }
}
