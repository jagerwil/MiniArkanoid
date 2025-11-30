using Game.Architecture.SceneInitializers;
using Game.Gameplay._Factories;
using Game.Gameplay._Factories.Implementations;
using Game.Gameplay._Providers;
using Game.Gameplay._Providers.Implementations;
using Game.Gameplay._Services;
using Game.Gameplay._Services.Implementations;
using Game.Gameplay.Bricks;
using Game.Gameplay.GameStates;
using Game.Gameplay.UI;
using Jagerwil.Core.Services;
using Jagerwil.Core.Services.Implementations;
using UnityEngine;
using Zenject;

namespace Game.Architecture.Installers.Scenes {
    public class GameplaySceneInstaller : MonoInstaller {
        [SerializeField] private BricksField _bricksField;
        [SerializeField] private GameUI _gameUI;
        [Space]
        [SerializeField] private Transform _platformSpawnRoot;
        [SerializeField] private Transform _ballsSpawnRoot;
        
        public override void InstallBindings() {
            BindServices();
            BindProviders();
            BindFactories();
            
            BindGameStates();
            Container.BindInterfacesTo<GameplaySceneInitializer>().AsSingle();
        }

        private void BindServices() {
            Container.BindInterfacesTo<InputService>().AsSingle();
            Container.BindInterfacesTo<WindowService>().AsSingle();

            Container.BindInterfacesTo<TimeService>().AsSingle();
            Container.BindInterfacesTo<ScoreService>().AsSingle();
            Container.BindInterfacesTo<GameplayLoopService>().AsSingle();
        }

        private void BindProviders() {
            Container.Bind<IPlatformProvider>().To<PlatformProvider>().AsSingle();
            
            Container.Bind<IBricksFieldProvider>()
                     .To<BricksFieldProvider>()
                     .AsSingle().WithArguments(_bricksField);
            
            Container.Bind<IGameUIProvider>()
                     .To<GameUIProvider>()
                     .AsSingle().WithArguments(_gameUI);
        }

        private void BindFactories() {
            Container.Bind<IPlatformFactory>().To<PlatformFactory>()
                     .AsSingle().WithArguments(_platformSpawnRoot);
            
            Container.Bind<IBallFactory>().To<BallFactory>()
                     .AsSingle().WithArguments(_ballsSpawnRoot);
        }

        private void BindGameStates() {
            Container.Bind<GameplayInitializationState>().AsSingle();
            Container.Bind<GameplayMainState>().AsSingle();
            Container.Bind<GameplayGameResultState>().AsSingle();
            Container.Bind<GameplayRestartState>().AsSingle();
        }
    }
}
