using Game.Gameplay.Windows;
using Jagerwil.Core.Architecture.StateMachine;
using Jagerwil.Core.Services;
using UnityEngine;

namespace Game.Gameplay.GameStates {
    public class GameplayGameEndState : IGameState<bool> {
        private readonly IGameStateMachine _stateMachine;
        private readonly IWindowService _windowService;
        
        public GameplayGameEndState(IGameStateMachine stateMachine, IWindowService windowService) {
            _stateMachine = stateMachine;
            _windowService = windowService;
        }
        
        public void Enter(bool isWon) {
            if (!isWon) {
                var gameOverWindow = _windowService.Open<GameOverWindow>();
            }
            //Subscribe to some event that would call RestartGame()
        }

        public void Exit() {
            //Hide game over / win window?
            //Unsub from all events
        }

        private void RestartGame() {
            _stateMachine.Enter<GameplayRestartState>();
        }
    }
}
