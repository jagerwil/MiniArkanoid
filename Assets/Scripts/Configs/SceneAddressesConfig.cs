using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Configs {
    [CreateAssetMenu(fileName = "ScenesAddressesConfig", menuName = "Configs/Scenes Addresses")]
    public class SceneAddressesConfig : ScriptableObject {
        [field: SerializeField] public AssetReference GameplayScene { get; private set; }
    }
}
