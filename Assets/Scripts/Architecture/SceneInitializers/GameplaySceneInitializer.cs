using System;
using Game.Gameplay.GameStates;
using Jagerwil.Core.Architecture.StateMachine;
using Zenject;

namespace Game.Architecture.SceneInitializers {
    public class GameplaySceneInitializer : IInitializable, IDisposable {
        private readonly IGameStateMachine _stateMachine;

        public GameplaySceneInitializer(IGameStateMachine stateMachine,
            GameplayInitializationState initializationState,
            GameplayMainState mainState,
            GameplayGameEndState gameEndState,
            GameplayRestartState restartState) {
            _stateMachine = stateMachine;
            
            _stateMachine.Register(initializationState);
            _stateMachine.Register(mainState);
            _stateMachine.Register(gameEndState);
            _stateMachine.Register(restartState);
        }
        
        public void Initialize() {
            _stateMachine.Enter<GameplayInitializationState>();
        }

        public void Dispose() {
            if (_stateMachine == null) {
                return;
            }

            _stateMachine.Unregister<GameplayInitializationState>();
            _stateMachine.Unregister<GameplayMainState>();
            _stateMachine.Unregister<GameplayGameEndState>();
            _stateMachine.Unregister<GameplayRestartState>();
        }
    }
}
