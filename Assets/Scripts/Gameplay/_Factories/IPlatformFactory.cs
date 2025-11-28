using Game.Gameplay.Platforms;
using Jagerwil.Core.Architecture.Factories;
using UnityEngine;

namespace Game.Gameplay._Factories {
    public interface IPlatformFactory : IGameFactory<Platform> {
        public Platform Spawn();
    }
}
