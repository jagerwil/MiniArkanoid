using UnityEngine;
using Zenject;

namespace Game.Gameplay.Platforms {
    public class Platform : MonoBehaviour, IPoolable {
        [SerializeField] private PlatformBallHandler _ballHandler;
        
        public void OnSpawned() {
            _ballHandler.SpawnBall();
        }
        
        public void OnDespawned() {
            _ballHandler.TryDespawnBall();
        }
    }
}
