using Game.Gameplay._Factories;
using Game.Gameplay.Balls;
using UnityEngine;
using Zenject;
using Vector2 = UnityEngine.Vector2;

namespace Game.Gameplay.Platforms {
    public class PlatformBallHandler : MonoBehaviour {
        [SerializeField] private Transform _ballSpawnPoint;
        
        [Inject] private IBallFactory _ballFactory;
        
        private Ball _holdBall;

        public void SpawnBall() {
            if (_holdBall) {
                Debug.LogError("Hold ball was already spawned!");
                return;
            }

            _holdBall = _ballFactory.Spawn(_ballSpawnPoint.position, false, Vector2.zero, _ballSpawnPoint);
        }

        public void TryDespawnBall() {
            if (_holdBall && _ballFactory != null) {
                _ballFactory.Despawn(_holdBall);
            }
        }

        public void ShootBall() {
            if (!_holdBall) {
                return;
            }
            
            _holdBall.Shoot(Vector2.up);
            _holdBall = null;
        }
    }
}
