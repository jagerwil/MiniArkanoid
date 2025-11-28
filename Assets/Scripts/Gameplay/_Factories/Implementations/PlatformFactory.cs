using Game.Configs;
using Game.Gameplay.Platforms;
using Jagerwil.Core.Architecture.Factories.Implementations;
using Jagerwil.Core.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Game.Gameplay._Factories.Implementations {
    public class PlatformFactory : BaseGameFactory<Platform>, IPlatformFactory {
        private readonly AssetReferenceGameObject _assetReference;

        public PlatformFactory(IInstantiator instantiator,
            IAddressablesLoader addressablesLoader,
            PrefabAddresses prefabAddresses,
            Transform defaultRoot) : base(instantiator, addressablesLoader, new MemoryPoolSettings(), defaultRoot) {
            _assetReference = prefabAddresses.Platform;
        }
        
        public Platform Spawn() {
            return CreateInternal();
        }
        
        protected override AssetReferenceGameObject GetAssetReference() {
            return _assetReference;
        }
    }
}
