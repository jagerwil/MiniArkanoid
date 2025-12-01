using System;
using Game.Gameplay._Services;
using R3;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.UI {
    public class PlayerLivesUI : MonoBehaviour {
        [SerializeField] private string _textFormat = "Lives: {0} / {1}";
        [SerializeField] private TMP_Text _text;
        
        private readonly CompositeDisposable _disposables = new();

        [Inject]
        private void Inject(IGameplayLoopService gameplayLoopService) {
            gameplayLoopService.PlayerLivesLeft
                               .Subscribe(PlayerLivesChanged).AddTo(_disposables);
        }

        private void OnDestroy() {
            _disposables.Dispose();
        }

        private void PlayerLivesChanged(int playerLivesLeft) {
            _text.text = string.Format(_textFormat, playerLivesLeft);
        }
    }
}
