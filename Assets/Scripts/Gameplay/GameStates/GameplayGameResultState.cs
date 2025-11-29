using Game.Gameplay.Windows;
using Jagerwil.Core.Architecture.StateMachine;
using Jagerwil.Core.Services;
using UnityEngine;

namespace Game.Gameplay.GameStates {
    public class GameplayGameResultState : IGameState<bool> {
        private readonly IGameStateMachine _stateMachine;
        private readonly IWindowService _windowService;
        
        public GameplayGameResultState(IGameStateMachine stateMachine, IWindowService windowService) {
            _stateMachine = stateMachine;
            _windowService = windowService;
        }
        
        public void Enter(bool isWon) {
            var gameResultWindow = _windowService.Open<GameResultWindow>();
            if (gameResultWindow) {
                gameResultWindow.Initialize(isWon, RestartGame);
            }
        }

        public void Exit() { }

        private void RestartGame() {
            _stateMachine.Enter<GameplayRestartState>();
        }
    }
}
