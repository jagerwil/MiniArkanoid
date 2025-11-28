using Jagerwil.Core.Architecture.StateMachine;
using UnityEngine;

namespace Game.Gameplay.GameStates {
    public class GameplayGameEndState : IGameState<bool> {
        private readonly IGameStateMachine _stateMachine;
        
        public GameplayGameEndState(IGameStateMachine stateMachine /*, services to stop gameplay */) {
            _stateMachine = stateMachine;
        }
        
        public void Enter(bool isWon) {
            //Show game over / win window?
            //Subscribe to some event that would call RestartGame()
        }
        
        public void Exit() { }

        private void RestartGame() {
            _stateMachine.Enter<GameplayRestartState>();
        }
    }
}
