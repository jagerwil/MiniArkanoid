using System;
using UnityEngine;

namespace Game.Configs {
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/Game")]
    public class GameConfig : ScriptableObject {
        [field: SerializeField] public GameplayLoopInfo GameplayLoopInfo { get; private set; }
    }

    [Serializable]
    public class GameplayLoopInfo {
        [field: SerializeField] public int PlayerLives { get; private set; }
    }
}
