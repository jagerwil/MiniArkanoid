using Game.Architecture.SceneInitializers;
using Game.Gameplay._Services;
using Game.Gameplay._Services.Implementations;
using Game.Gameplay.GameStates;
using UnityEngine;
using Zenject;

namespace Game.Architecture.Installers.Scenes {
    public class GameplaySceneInstaller : MonoInstaller {
        public override void InstallBindings() {
            BindServices();
            BindProviders();
            BindFactories();
            
            BindGameStates();
            Container.BindInterfacesTo<GameplaySceneInitializer>().AsSingle();
        }

        private void BindServices() {
            Container.Bind<IInputService>().To<InputService>().AsSingle();
        }

        private void BindProviders() {
            
        }

        private void BindFactories() {
            
        }

        private void BindGameStates() {
            Container.Bind<GameplayInitializationState>().AsSingle();
            Container.Bind<GameplayMainState>().AsSingle();
            Container.Bind<GameplayGameEndState>().AsSingle();
            Container.Bind<GameplayRestartState>().AsSingle();
        }
    }
}
