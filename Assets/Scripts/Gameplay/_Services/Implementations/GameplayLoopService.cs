using System;
using Game.Configs;
using Game.Gameplay._Factories;
using Game.Gameplay._Providers;
using R3;

namespace Game.Gameplay._Services.Implementations {
    public class GameplayLoopService : IGameplayLoopService, IGameplayService {
        private readonly IPlatformProvider _platformProvider;
        private readonly IBallFactory _ballFactory;
        private readonly GameplayLoopInfo _info;
        
        private readonly ReactiveProperty<int> _playerLivesLeft = new();

        public ReadOnlyReactiveProperty<int> PlayerLivesLeft => _playerLivesLeft;
        public int MaxPlayerLives => _info.PlayerLives;
        
        public event Action onGameOver;

        public GameplayLoopService(IPlatformProvider platformProvider,
            IBallFactory ballFactory,
            GameConfig gameConfig) {
            _platformProvider = platformProvider;
            _ballFactory = ballFactory;
            
            _info = gameConfig.GameplayLoopInfo;
        }

        public void GameplayStarted() {
            _playerLivesLeft.Value = MaxPlayerLives;
            SpawnBall();
            
            _ballFactory.onAllBallsDespawned += TryRespawnBall;
        }

        public void GameplayEnded() {
            _ballFactory.onAllBallsDespawned -= TryRespawnBall;
        }

        private void TryRespawnBall() {
            if (_playerLivesLeft.Value <= 0) {
                onGameOver?.Invoke();
                return;
            }

            _playerLivesLeft.Value -= 1;
            SpawnBall();
        }

        private void SpawnBall() {
            _platformProvider.Platform.SpawnBall();
        }
    }
}
