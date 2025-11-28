using Game.Gameplay.Balls;
using Jagerwil.Core.Architecture.Factories;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Gameplay._Factories {
    public interface IBallFactory : IGameFactory<Ball> {
        public Ball Spawn(Vector3 position, bool isMoving, Vector2 moveDirection, [CanBeNull] Transform root = null);
    }
}
