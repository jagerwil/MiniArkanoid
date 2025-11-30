using R3;

namespace Game.Gameplay._Services.Implementations {
    public class ScoreService : IScoreService {
        private readonly ReactiveProperty<int> _score = new();
        
        public ReadOnlyReactiveProperty<int> Score => _score;

        public void ResetScore() {
            _score.Value = 0;
        }

        public void ChangeScore(int deltaScore) {
            _score.Value += deltaScore;
        }
    }
}
