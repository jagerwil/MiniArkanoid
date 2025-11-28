using Cysharp.Threading.Tasks;
using Game.Gameplay._Factories;
using Jagerwil.Core.Architecture.StateMachine;

namespace Game.Gameplay.GameStates {
    public class GameplayInitializationState : IGameState {
        private readonly IGameStateMachine _stateMachine;
        private readonly IPlatformFactory _platformFactory;
        private readonly IBallFactory _ballFactory;

        public GameplayInitializationState(IGameStateMachine stateMachine,
            IPlatformFactory platformFactory, 
            IBallFactory ballFactory) {
            _stateMachine = stateMachine;
            _platformFactory = platformFactory;
            _ballFactory = ballFactory;
        }
        
        public void Enter() {
            WarmUpFactoriesAsync().Forget();
        }
        
        public void Exit() { }

        private async UniTask WarmUpFactoriesAsync() {
            var ballTask = _ballFactory.WarmUpAsync();
            var platformTask = _platformFactory.WarmUpAsync();
            
            await UniTask.WhenAll(platformTask, ballTask);
            
            _stateMachine.Enter<GameplayMainState>();
        }
    }
}
