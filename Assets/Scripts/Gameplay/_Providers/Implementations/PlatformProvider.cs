using Game.Gameplay.Platforms;

namespace Game.Gameplay._Providers.Implementations {
    public class PlatformProvider : IPlatformProvider {
        public Platform Platform { get; private set; }
        
        public void SetPlatform(Platform platform) {
            Platform = platform;
        }
    }
}
