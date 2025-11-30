using System;
using Game.Gameplay._Services;
using R3;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.UI {
    public class PlayerScoreUI : MonoBehaviour {
        [SerializeField] private string _textFormat = "Score: {0}";
        [SerializeField] private TMP_Text _text;

        private readonly CompositeDisposable _disposables = new();

        [Inject]
        private void Inject(IScoreService scoreService) {
            scoreService.Score.Subscribe(ScoreChanged).AddTo(_disposables);
        }

        private void OnDestroy() {
            _disposables?.Dispose();
        }

        private void ScoreChanged(int score) {
            _text.text = string.Format(_textFormat, score);
        }
    }
}
