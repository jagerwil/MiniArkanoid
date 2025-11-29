using Game.Architecture.SceneInitializers;
using Game.Gameplay._Factories;
using Game.Gameplay._Factories.Implementations;
using Game.Gameplay._Providers;
using Game.Gameplay._Providers.Implementations;
using Game.Gameplay._Services;
using Game.Gameplay._Services.Implementations;
using Game.Gameplay.GameStates;
using UnityEngine;
using Zenject;

namespace Game.Architecture.Installers.Scenes {
    public class GameplaySceneInstaller : MonoInstaller {
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
            Container.Bind<IInputService>().To<InputService>().AsSingle();
            Container.Bind<IGameplayLoopService>().To<GameplayLoopService>().AsSingle();
        }

        private void BindProviders() {
            Container.Bind<IPlatformProvider>().To<PlatformProvider>().AsSingle();
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
            Container.Bind<GameplayGameEndState>().AsSingle();
            Container.Bind<GameplayRestartState>().AsSingle();
        }
    }
}
