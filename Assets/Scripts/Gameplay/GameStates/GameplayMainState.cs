using Game.Gameplay._Factories;
using Game.Gameplay._Providers;
using Game.Gameplay._Services;
using Jagerwil.Core.Architecture.StateMachine;
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
            
            _gameplayLoopService.StartGame();
            
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
