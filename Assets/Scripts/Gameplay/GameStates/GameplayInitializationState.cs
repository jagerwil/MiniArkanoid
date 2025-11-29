using Cysharp.Threading.Tasks;
using Game.Gameplay._Factories;
using Jagerwil.Core.Architecture.StateMachine;
using Jagerwil.Core.Services;

namespace Game.Gameplay.GameStates {
    public class GameplayInitializationState : IGameState {
        private readonly IGameStateMachine _stateMachine;
        private readonly IPlatformFactory _platformFactory;
        private readonly IBallFactory _ballFactory;
        private readonly IWindowService _windowService;

        public GameplayInitializationState(IGameStateMachine stateMachine,
            IPlatformFactory platformFactory, 
            IBallFactory ballFactory,
            IWindowService windowService) {
            _stateMachine = stateMachine;
            _platformFactory = platformFactory;
            _ballFactory = ballFactory;
            _windowService = windowService;
        }
        
        public void Enter() {
            WarmUpFactoriesAsync().Forget();
        }
        
        public void Exit() { }

        private async UniTask WarmUpFactoriesAsync() {
            var ballTask = _ballFactory.WarmUpAsync();
            var platformTask = _platformFactory.WarmUpAsync();
            
            await UniTask.WhenAll(platformTask, ballTask);
            
            _windowService.RegisterAll();
            _stateMachine.Enter<GameplayMainState>();
        }
    }
}
