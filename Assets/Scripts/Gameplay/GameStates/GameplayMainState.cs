using Game.Gameplay._Services;
using Jagerwil.Core.Architecture.StateMachine;
using UnityEngine;

namespace Game.Gameplay.GameStates {
    public class GameplayMainState : IGameState {
        private readonly IGameStateMachine _stateMachine;
        private readonly IInputService _inputService;
        
        public GameplayMainState(IGameStateMachine stateMachine, IInputService inputService
            /*, factories to spawn objects when game starts */) {
            _stateMachine = stateMachine;
            _inputService = inputService;
        }
        
        public void Enter() {
            //Spawn objects!
            
            //Subscribe to some event that would call EndGame()
            //Subscribe to some event that would call RestartGame()
            
            _inputService.Enable();
        }
        
        public void Exit() {
            _inputService.Disable();
            
            //Unsub from all events
        }

        private void EndGame(bool isWon) {
            _stateMachine.Enter<GameplayGameEndState, bool>(isWon);
        }

        private void RestartGame() {
            _stateMachine.Enter<GameplayRestartState>();
        }
    }
}
