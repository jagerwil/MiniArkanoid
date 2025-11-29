using System;
using Jagerwil.Core.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay.Windows {
    public class GameResultWindow : BaseWindow {
        [SerializeField] private GameObject _victoryView;
        [SerializeField] private GameObject _gameOverView;
        [SerializeField] private Button _restartButton;

        private Action _clickButtonAction;

        private void Awake() {
            _restartButton.onClick.AddListener(RestartButtonPressed);
        }

        public void Initialize(bool isVictory, Action restartLevelAction) {
            _clickButtonAction = restartLevelAction;
            
            _victoryView.SetActive(isVictory);
            _gameOverView.SetActive(!isVictory);
        }

        private void RestartButtonPressed() {
            Hide();
            _clickButtonAction?.Invoke();
        }
    }
}
