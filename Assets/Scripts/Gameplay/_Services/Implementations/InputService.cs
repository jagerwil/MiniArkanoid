using System;
using R3;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace Game.Gameplay._Services.Implementations {
    public class InputService : IInputService, IGameplayService {
        private readonly ReactiveProperty<float> _moveAxis = new();
        private readonly ReactiveProperty<Vector2> _touchPosition = new();
        
        private readonly InputActions _inputActions = new();
        
        public ReadOnlyReactiveProperty<float> MoveAxis => _moveAxis;
        public ReadOnlyReactiveProperty<Vector2> TouchPosition => _touchPosition;

        public event Action onTouchStarted;
        public event Action onTouchEnded;
        
        public event Action onShootBallPressed;

        public InputService() {
            _inputActions.Player.MovePlatform.performed += MovePlatformPerformed;
            _inputActions.Player.MovePlatform.canceled += MovePlatformCanceled;

            _inputActions.Player.Touch.performed += TouchPerformed;
            

            _inputActions.Player.ShootBall.started += ShootBallStarted;
        }
        
        public void GameplayStarted() {
            _inputActions.Enable();
        }
        
        public void GameplayEnded() {
            onTouchEnded?.Invoke();
            _inputActions.Disable();
        }

        private void MovePlatformPerformed(CallbackContext ctx) {
            _moveAxis.Value = ctx.ReadValue<float>();
        }

        private void MovePlatformCanceled(CallbackContext ctx) {
            _moveAxis.Value = 0f;
        }

        private void TouchPerformed(CallbackContext ctx) {
            var touch = ctx.ReadValue<TouchState>();
            _touchPosition.Value = touch.position;

            switch (touch.phase) {
                case TouchPhase.Began:
                    onTouchStarted?.Invoke();
                    break;
                
                case TouchPhase.Ended:
                    onTouchEnded?.Invoke();
                    break;
            }
        }

        private void ShootBallStarted(CallbackContext ctx) {
            onShootBallPressed?.Invoke();
        }
    }
}
