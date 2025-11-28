using Game.Architecture.SceneInitializers;
using Zenject;

namespace Game.Architecture.Installers.Scenes {
    public class EntryPointSceneInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container.BindInterfacesTo<EntryPointSceneInitializer>().AsSingle();
        }
    }
}
