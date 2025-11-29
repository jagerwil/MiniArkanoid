using System;
using Game.Gameplay.Balls;
using Jagerwil.Core.Architecture.Factories;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay._Factories {
    public interface IBallFactory : IGameFactory<Ball> {
        public event Action<Ball> onBallSpawned;
        public event Action<Ball> onBallDespawned;
        public event Action onAllBallsDespawned;
        
        public Ball Spawn(Vector3 position, bool isMoving, Vector2 moveDirection, [CanBeNull] Transform root = null);
    }
}
