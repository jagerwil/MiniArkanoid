using System;
using Game.Gameplay._Services;
using R3;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Platform {
    public class PlatformInputController : MonoBehaviour {
        [SerializeField] private BoxCollider2D _collider;
        [SerializeField] private Transform _graphicsRoot;
        [Space]
        [SerializeField] private PlatformHorizontalMovement _movement;
        
        private readonly CompositeDisposable _disposable = new();
        private IInputService _inputService;
        
        [Inject]
        private void Inject(IInputService inputService) {
            _inputService = inputService;
            
            inputService.MoveAxis.Subscribe(MoveAxisChanged).AddTo(_disposable);
            inputService.onShootBallTriggered += ShootBallTriggered;
        }

        private void OnDestroy() {
            if (_inputService != null) {
                _inputService.onShootBallTriggered -= ShootBallTriggered;
            }
        }

        private void MoveAxisChanged(float axis) {
            _movement.SetMoveAxis(axis);
        }

        private void ShootBallTriggered() {
            Debug.Log("Shoot ball triggered!");
        }
    }
}
