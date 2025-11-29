using Game.Gameplay._Providers;
using Game.Gameplay.UI;
using Game.Gameplay.Windows;
using Jagerwil.Core.Architecture.StateMachine;
using Jagerwil.Core.Services;
using UnityEngine;

namespace Game.Gameplay.GameStates {
    public class GameplayGameResultState : IGameState<bool> {
        private readonly IGameStateMachine _stateMachine;
        private readonly IWindowService _windowService;
        private readonly IGameUIProvider _gameUIProvider;
        
        public GameplayGameResultState(IGameStateMachine stateMachine, 
            IWindowService windowService, 
            IGameUIProvider gameUIProvider) {
            _stateMachine = stateMachine;
            _windowService = windowService;
            _gameUIProvider = gameUIProvider;
        }
        
        public void Enter(bool isWon) {
            _gameUIProvider.GameUI.SetActive(false);
            var gameResultWindow = _windowService.Open<GameResultWindow>();
            if (gameResultWindow) {
                gameResultWindow.Initialize(isWon, RestartGame);
            }
        }

        public void Exit() {
            if (_gameUIProvider?.GameUI) {
                _gameUIProvider.GameUI.SetActive(true);
            }
        }

        private void RestartGame() {
            _stateMachine.Enter<GameplayRestartState>();
        }
    }
}
