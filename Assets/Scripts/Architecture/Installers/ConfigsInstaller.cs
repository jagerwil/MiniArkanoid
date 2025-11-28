using Game.Configs;
using UnityEngine;
using Zenject;

namespace Game.Architecture.Installers {
    [CreateAssetMenu(fileName = "ConfigsInstaller", menuName = "Configs/Configs Installer", order = 0)]
    public class ConfigsInstaller : ScriptableObjectInstaller {
        [SerializeField] private SceneAddressesConfig _scenesAddresses;
        [SerializeField] private PrefabAddresses _prefabAddresses;
        [Space]
        [SerializeField] private GameConfig _gameConfig;
        
        public override void InstallBindings() {
            Container.Bind<SceneAddressesConfig>().FromInstance(_scenesAddresses).AsSingle();
            Container.Bind<PrefabAddresses>().FromInstance(_prefabAddresses).AsSingle();
            
            Container.Bind<GameConfig>().FromInstance(_gameConfig).AsSingle();
        }
    }
}
