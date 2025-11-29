using Game.Gameplay.Platforms;
using UnityEngine;

namespace Game.Gameplay._Providers {
    public interface IPlatformProvider {
        public Platform Platform { get; }

        public void SetPlatform(Platform platform);
    }
}
