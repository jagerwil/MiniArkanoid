using Game.Gameplay._Factories;
using Game.Gameplay._Providers;
using Game.Gameplay._Services;
using Game.Gameplay.Windows;
using Jagerwil.Core.Architecture.StateMachine;
using Jagerwil.Core.Services;
using UnityEngine;

namespace Game.Gameplay.GameStates {
    public class GameplayMainState : IGameState {
        private readonly IGameStateMachine _stateMachine;
        private readonly IPlatformFactory _platformFactory;
        private readonly IPlatformProvider _platformProvider;
        private readonly IGameplayLoopService _gameplayLoopService;
        private readonly IInputService _inputService;
        
        public GameplayMainState(IGameStateMachine stateMachine, 
            IPlatformFactory platformFactory, 
            IPlatformProvider platformProvider,
            IGameplayLoopService gameplayLoopService,
            IInputService inputService) {
            _stateMachine = stateMachine;
            
            _platformFactory = platformFactory;
            _platformProvider = platformProvider;
            
            _gameplayLoopService = gameplayLoopService;
            _inputService = inputService;
        }
        
        public void Enter() {
            var platform = _platformFactory.Spawn();
            _platformProvider.SetPlatform(platform);

            _gameplayLoopService.onGameOver += GameOver;
            _gameplayLoopService.StartGame();

            //Subscribe to some event that would call RestartGame()
            
            _inputService.Enable();
        }
        
        public void Exit() {
            if (_gameplayLoopService != null) {
                _gameplayLoopService.onGameOver -= GameOver;
            }
            
            _inputService.Disable();
        }

        private void GameOver() {
            EndGame(false);
        }

        private void EndGame(bool isWon) {
            _stateMachine.Enter<GameplayGameEndState, bool>(isWon);
        }

        private void RestartGame() {
            _stateMachine.Enter<GameplayRestartState>();
        }
    }
}
