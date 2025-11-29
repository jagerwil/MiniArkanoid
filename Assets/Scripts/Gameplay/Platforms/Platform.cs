using System;
using Jagerwil.Core.Architecture;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Platforms {
    public class Platform : MonoBehaviour, IPoolableObject<Platform> {
        [SerializeField] private PlatformBallHandler _ballHandler;
        
        public event Action<Platform> onDespawnRequested;
        
        public void OnSpawned() { }
        
        public void OnDespawned() {
            _ballHandler.TryDespawnBall();
        }

        public void SpawnBall() {
            _ballHandler.SpawnBall();
        }
    }
}
