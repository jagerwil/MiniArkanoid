using Game.Gameplay.UI;
using UnityEngine;

namespace Game.Gameplay._Providers.Implementations {
    public class GameUIProvider : IGameUIProvider {
        public GameUI GameUI { get; }

        public GameUIProvider(GameUI gameUI) {
            GameUI = gameUI;
        }
    }
}
