using Game.Gameplay._Providers;
using Game.Gameplay._Services;
using R3;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Platforms {
    public class PlatformInputController : MonoBehaviour {
        [SerializeField] private BoxCollider2D _collider;
        [Space]
        [SerializeField] private PlatformHorizontalMovement _movement;
        [SerializeField] private PlatformBallHandler _ballHandler;

        [Inject] private ICameraProvider _cameraProvider;
        
        private readonly CompositeDisposable _disposable = new();
        private IInputService _inputService;

        private float _moveAxis;
        private bool _hasTouch;
        private Vector2 _touchPosition;

        [Inject]
        private void Inject(IInputService inputService) {
            _inputService = inputService;
            
            inputService.MoveAxis.Subscribe(MoveAxisChanged).AddTo(_disposable);
            inputService.TouchPosition.Subscribe(TouchPositionChanged).AddTo(_disposable);

            inputService.onTouchStarted += TouchStarted;
            inputService.onTouchEnded += TouchEnded;
            inputService.onShootBallPressed += ShootBall;
        }

        private void OnDestroy() {
            if (_inputService != null) {
                _inputService.onShootBallPressed -= ShootBall;
            }
        }

        private void MoveAxisChanged(float moveAxis) {
            _moveAxis = moveAxis;
            ApplyPlatformMovement();
        }

        private void TouchPositionChanged(Vector2 touchPosition) {
            var screenPoint = new Vector3(touchPosition.x, touchPosition.y, _cameraProvider.Camera.nearClipPlane);
            _touchPosition = _cameraProvider.Camera.ScreenToWorldPoint(screenPoint);
            
            ApplyPlatformMovement();
        }

        private void TouchStarted() {
            _hasTouch = true;
            ApplyPlatformMovement();
        }

        private void TouchEnded() {
            if (!_hasTouch) {
                return;
            }
            
            ShootBall();
            _hasTouch = false;
            ApplyPlatformMovement();
        }

        private void ShootBall() {
            _ballHandler.ShootBall();
        }

        private void ApplyPlatformMovement() {
            if (_hasTouch) {
                _movement.SetMoveTargetPosition(_touchPosition.x);
            }
            else {
                _movement.SetMoveAxis(_moveAxis);
            }
        }
    }
}
