using System;
using R3;

namespace Game.Gameplay._Services {
    public interface IGameplayLoopService {
        public ReadOnlyReactiveProperty<int> PlayerLivesLeft { get; }
        public int MaxPlayerLives { get; }

        public event Action onGameOver;
        
        public void StartGame();
        public void StopGame();
    }
}
