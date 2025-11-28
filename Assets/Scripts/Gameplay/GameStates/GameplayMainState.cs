using Game.Gameplay._Factories;
using Game.Gameplay._Services;
using Jagerwil.Core.Architecture.StateMachine;
using UnityEngine;

namespace Game.Gameplay.GameStates {
    public class GameplayMainState : IGameState {
        private readonly IGameStateMachine _stateMachine;
        private readonly IPlatformFactory _platformFactory;
        private readonly IInputService _inputService;
        
        public GameplayMainState(IGameStateMachine stateMachine, 
            IPlatformFactory platformFactory, 
            IInputService inputService) {
            _stateMachine = stateMachine;
            _platformFactory = platformFactory;
            _inputService = inputService;
        }
        
        public void Enter() {
            _platformFactory.Spawn();
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
