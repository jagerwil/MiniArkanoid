using Game.Gameplay._Factories;
using Game.Gameplay._Providers;

namespace Game.Gameplay._Services.Implementations {
    public class BallService : IBallService {
        private readonly IPlatformProvider _platformProvider;
        private readonly IBallFactory _ballFactory;
        
        public BallService(IPlatformProvider platformProvider,
            IBallFactory ballFactory) {
            _platformProvider = platformProvider;
            _ballFactory = ballFactory;

            _ballFactory.onAllBallsDespawned += SpawnBall;
        }
        
        public void SpawnBall() {
            _platformProvider.Platform.SpawnBall();
        }
    }
}
