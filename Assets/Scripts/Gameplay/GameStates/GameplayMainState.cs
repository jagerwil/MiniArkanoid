using Game.Gameplay._Factories;
using Game.Gameplay._Providers;
using Game.Gameplay._Services;
using Jagerwil.Core.Architecture.StateMachine;

namespace Game.Gameplay.GameStates {
    public class GameplayMainState : IGameState {
        private readonly IGameStateMachine _stateMachine;
        private readonly IInputService _inputService;
        private readonly IGameplayLoopService _gameplayLoopService;
        private readonly IPlatformFactory _platformFactory;
        private readonly IPlatformProvider _platformProvider;
        private readonly IBricksFieldProvider _bricksFieldProvider;
        
        public GameplayMainState(IGameStateMachine stateMachine, 
            IInputService inputService,
            IGameplayLoopService gameplayLoopService,
            IPlatformFactory platformFactory, 
            IPlatformProvider platformProvider,
            IBricksFieldProvider bricksFieldProvider) {
            _stateMachine = stateMachine;
            
            _inputService = inputService;
            _gameplayLoopService = gameplayLoopService;
            
            _platformFactory = platformFactory;
            
            _platformProvider = platformProvider;
            _bricksFieldProvider = bricksFieldProvider;
        }
        
        public void Enter() {
            var platform = _platformFactory.Spawn();
            _platformProvider.SetPlatform(platform);

            _gameplayLoopService.onGameOver += GameLost;
            _gameplayLoopService.StartGame();

            _bricksFieldProvider.BricksField.onAllBricksDestroyed += GameWon;

            //Subscribe to some event that would call RestartGame()
            
            _inputService.Enable();
        }
        
        public void Exit() {
            if (_gameplayLoopService != null) {
                _gameplayLoopService.onGameOver -= GameLost;
            }

            if (_bricksFieldProvider != null && _bricksFieldProvider.BricksField) {
                _bricksFieldProvider.BricksField.onAllBricksDestroyed -= GameWon;
            }
            
            _inputService.Disable();
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
