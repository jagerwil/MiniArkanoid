using System;
using R3;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace Game.Gameplay._Services.Implementations {
    public class InputService : IInputService, IGameplayService {
        private readonly ReactiveProperty<float> _moveAxis = new();
        private readonly InputActions _inputActions = new();
        
        public ReadOnlyReactiveProperty<float> MoveAxis => _moveAxis;
        public event Action onShootBallTriggered;

        public InputService() {
            _inputActions.Player.MovePlatform.performed += MovePlatformPerformed;
            _inputActions.Player.MovePlatform.canceled += MovePlatformCanceled;

            _inputActions.Player.ShootBall.started += ShootBallStarted;
        }
        
        public void GameplayStarted() {
            _inputActions.Enable();
        }
        
        public void GameplayEnded() {
            _inputActions.Disable();
        }

        private void MovePlatformPerformed(CallbackContext ctx) {
            _moveAxis.Value = ctx.ReadValue<float>();
        }

        private void MovePlatformCanceled(CallbackContext ctx) {
            _moveAxis.Value = 0f;
        }

        private void ShootBallStarted(CallbackContext ctx) {
            onShootBallTriggered?.Invoke();
        }
    }
}
