using Game.Gameplay._Factories;
using Game.Gameplay._Providers;
using Game.Gameplay._Services;
using Jagerwil.Core.Architecture.StateMachine;

namespace Game.Gameplay.GameStates {
    public class GameplayRestartState : IGameState {
        private readonly IGameStateMachine _stateMachine;
        private readonly IPlatformFactory _platformFactory;
        private readonly IBallFactory _ballFactory;
        private readonly IBricksFieldProvider _bricksFieldProvider;

        public GameplayRestartState(IGameStateMachine stateMachine,
            IPlatformFactory platformFactory,
            IBallFactory ballFactory,
            IBricksFieldProvider bricksFieldProvider) {
            _stateMachine = stateMachine;
            _platformFactory = platformFactory;
            _ballFactory = ballFactory;
            _bricksFieldProvider = bricksFieldProvider;
        }

        public void Enter() {
            _platformFactory.DespawnAll();
            _ballFactory.DespawnAll();
            _bricksFieldProvider.BricksField.RestoreField();

            _stateMachine.Enter<GameplayMainState>();
        }
        
        public void Exit() { }
    }
}
