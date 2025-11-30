using System;
using UnityEngine;

namespace Game.Configs {
    [CreateAssetMenu(fileName = "PlatformConfig", menuName = "Configs/Platform")]
    public class PlatformConfig : ScriptableObject {
        [field: SerializeField] public PlatformMovementInfo Movement { get; private set; }
        [field: SerializeField] public PlatformBallRedirectionInfo BallRedirection{ get; private set; }
        [field: SerializeField] public PlatformSizeInfo Size { get; private set; }
    }

    [Serializable]
    public class PlatformMovementInfo {
        [field: SerializeField] public float MoveSpeed { get; private set; }
    }

    [Serializable]
    public class PlatformBallRedirectionInfo {
        [field: SerializeField] public float DeltaAngleAtEdges { get; private set; } = 30f;
        [field: SerializeField] public float DeltaAngleSpeedMultiplier { get; private set; } = 0.1f;
    }

    [Serializable]
    public class PlatformSizeInfo {
        [field: SerializeField] public Vector2 Size { get; private set; }
    }
}
