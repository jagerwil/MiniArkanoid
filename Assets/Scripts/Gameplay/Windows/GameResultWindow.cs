using System;
using Game.Gameplay._Services;
using Jagerwil.Core.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Gameplay.Windows {
    public class GameResultWindow : BaseWindow {
        [SerializeField] private GameObject _victoryView;
        [SerializeField] private GameObject _gameOverView;
        [Space]
        [SerializeField] private string _scoreTextFormat = "Score: <color=#FF0>{0}</color>";
        [SerializeField] private TMP_Text _scoreText;
        [Space]
        [SerializeField] private Button _restartButton;

        [Inject] private IScoreService _scoreService;

        private Action _clickButtonAction;

        private void Awake() {
            _restartButton.onClick.AddListener(RestartButtonPressed);
        }

        public void Initialize(bool isVictory, Action restartLevelAction) {
            _clickButtonAction = restartLevelAction;
            
            _victoryView.SetActive(isVictory);
            _gameOverView.SetActive(!isVictory);
            
            _scoreText.text = string.Format(_scoreTextFormat, _scoreService.Score.CurrentValue);
        }

        private void RestartButtonPressed() {
            Hide();
            _clickButtonAction?.Invoke();
        }
    }
}
