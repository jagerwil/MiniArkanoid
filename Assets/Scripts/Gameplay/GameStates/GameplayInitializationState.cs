using Cysharp.Threading.Tasks;
using Jagerwil.Core.Architecture.StateMachine;

namespace Game.Gameplay.GameStates {
    public class GameplayInitializationState : IGameState {
        private readonly IGameStateMachine _stateMachine;

        public GameplayInitializationState(IGameStateMachine stateMachine /*,  factories */) {
            _stateMachine = stateMachine;
        }
        
        public void Enter() {
            WarmUpFactoriesAsync().Forget();
        }
        
        public void Exit() { }

        private async UniTask WarmUpFactoriesAsync() {
            await UniTask.WhenAll(/* add all warm up tasks here */);
            
            _stateMachine.Enter<GameplayMainState>();
        }
    }
}
