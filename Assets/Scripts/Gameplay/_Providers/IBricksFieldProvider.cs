using Game.Gameplay.Bricks;
using UnityEngine;

namespace Game.Gameplay._Providers {
    public interface IBricksFieldProvider {
        public BricksField BricksField { get; }
    }
}
