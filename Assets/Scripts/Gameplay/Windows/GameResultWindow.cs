using System;
using Game.Extensions;
using Game.Gameplay._Services;
using Jagerwil.Core.UI;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Windows {
    public class GameResultWindow : BaseWindow {
        [SerializeField] private GameObject _victoryView;
        [SerializeField] private GameObject _gameOverView;
        [Space]
        [SerializeField] private TMP_Text _timeText;
        [SerializeField] private TMP_Text _scoreText;

        [Inject] private ITimeService _timeService;
        [Inject] private IScoreService _scoreService;

        private Action _clickButtonAction;

        public void Initialize(bool isVictory, Action restartLevelAction) {
            _clickButtonAction = restartLevelAction;
            
            _victoryView.SetActive(isVictory);
            _gameOverView.SetActive(!isVictory);

            _timeText.text = _timeService.TimeElapsed.CurrentValue.ToMinutesSecondsString();
            _scoreText.text = _scoreService.Score.CurrentValue.ToString();
        }

        public void RestartButtonPressed() {
            Hide();
            _clickButtonAction?.Invoke();
        }
    }
}
