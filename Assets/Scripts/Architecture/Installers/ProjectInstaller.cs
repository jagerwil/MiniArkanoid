using Game.Architecture._Services;
using Game.Architecture._Services.Implementations;
using Game.Architecture.StateMachine;
using Jagerwil.Core.Architecture.StateMachine;
using Jagerwil.Core.Services;
using Jagerwil.Core.Services.Implementations;
using Zenject;

namespace Game.Architecture.Installers {
    public class ProjectInstaller : MonoInstaller {
        public override void InstallBindings() {
            BindServices();
            BindGameStateMachine();
        }

        private void BindServices() {
            Container.Bind<IAddressablesLoader>().To<AddressablesLoader>().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
        }

        private void BindGameStateMachine() {
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
            Container.Bind<InitializationState>().AsSingle();
            Container.Bind<SceneLoadingState>().AsSingle();
        }
    }
}
