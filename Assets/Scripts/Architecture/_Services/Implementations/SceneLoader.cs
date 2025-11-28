using Cysharp.Threading.Tasks;
using Game.Configs;
using Jagerwil.Core.Services;
using Jagerwil.Core.Services.Implementations;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Architecture._Services.Implementations {
    public class SceneLoader : BaseSceneLoader<SceneType>, ISceneLoader {
        private readonly SceneAddressesConfig _sceneAddressesConfig;
        
        public SceneLoader(IAddressablesLoader addressablesLoader,
            SceneAddressesConfig sceneAddressesConfig)
            : base(addressablesLoader) {
            _sceneAddressesConfig = sceneAddressesConfig;
        }
        
        public override async UniTask LoadAsync(SceneType sceneType) {
            switch (sceneType) {
                case SceneType.Gameplay:
                    await LoadAsyncInternal(_sceneAddressesConfig.GameplayScene);
                    break;
                default:
                    Debug.LogError($"{nameof(SceneLoader)}.{nameof(LoadAsync)}: Scene type {sceneType} is not supported");
                    break;
            }
        }

        protected override AssetReference GetTransitionSceneRef() {
            return null;
        }
    }
}
