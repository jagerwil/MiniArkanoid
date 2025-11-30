using Game.Gameplay._Factories;
using Game.Gameplay._Providers;
using Game.Gameplay._Services;
using Jagerwil.Core.Architecture.StateMachine;

namespace Game.Gameplay.GameStates {
    public class GameplayMainState : IGameState {
        private readonly IGameStateMachine _stateMachine;
        private readonly IPlatformFactory _platformFactory;
        private readonly IPlatformProvider _platformProvider;
        private readonly IBricksFieldProvider _bricksFieldProvider;
        
        private readonly IGameplayService[] _gameplayServices;
        private readonly IGameplayLoopService _gameplayLoopService;
        
        public GameplayMainState(IGameStateMachine stateMachine, 
            IPlatformFactory platformFactory, 
            IPlatformProvider platformProvider,
            IBricksFieldProvider bricksFieldProvider,
            IGameplayService[] gameplayServices,
            IGameplayLoopService gameplayLoopService) {
            _stateMachine = stateMachine;

            _platformFactory = platformFactory;
            
            _platformProvider = platformProvider;
            _bricksFieldProvider = bricksFieldProvider;
            
            _gameplayServices = gameplayServices;
            _gameplayLoopService = gameplayLoopService;
        }
        
        public void Enter() {
            var platform = _platformFactory.Spawn();
            _platformProvider.SetPlatform(platform);
            
            _gameplayLoopService.onGameOver += GameLost;
            _bricksFieldProvider.BricksField.onAllBricksDestroyed += GameWon;

            foreach (var gameplayService in _gameplayServices) {
                gameplayService.GameplayStarted();
            }

            //Subscribe to some event that would call RestartGame()
        }
        
        public void Exit() {
            if (_gameplayLoopService != null) {
                _gameplayLoopService.onGameOver -= GameLost;
            }

            if (_bricksFieldProvider?.BricksField) {
                _bricksFieldProvider.BricksField.onAllBricksDestroyed -= GameWon;
            }

            foreach (var gameplayService in _gameplayServices) {
                gameplayService.GameplayEnded();
            }
        }

        private void GameLost() {
            EndGame(false);
        }

        private void GameWon() {
            EndGame(true);
        }

        private void EndGame(bool isWon) {
            _stateMachine.Enter<GameplayGameResultState, bool>(isWon);
        }

        private void RestartGame() {
            _stateMachine.Enter<GameplayRestartState>();
        }
    }
}
