using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Configs {
    [CreateAssetMenu(fileName = "PrefabAddresses", menuName = "Configs/Prefab Addresses")]
    public class PrefabAddresses : ScriptableObject {
        [field: SerializeField] public AssetReferenceGameObject Platform { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject Ball { get; private set; }
    }
}
