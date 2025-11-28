using Jagerwil.Core.Architecture.StateMachine;

namespace Game.Gameplay.GameStates {
    public class GameplayRestartState : IGameState {
        private readonly IGameStateMachine _stateMachine;

        public GameplayRestartState(IGameStateMachine stateMachine /*, factories to despawn objects */) {
            _stateMachine = stateMachine;
        }

        public void Enter() {
            //Despawn all objects
            //Reset all services
            _stateMachine.Enter<GameplayMainState>();
        }
        
        public void Exit() { }
    }
}
