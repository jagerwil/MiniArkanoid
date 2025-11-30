using R3;

namespace Game.Gameplay._Services {
    public interface IScoreService {
        public ReadOnlyReactiveProperty<int> Score { get; }
        
        public void ChangeScore(int deltaScore);
    }
}
