using System;
using Game.Gameplay._Services;
using R3;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.UI {
    public class PlayerTimerUI : MonoBehaviour {
        [SerializeField] private string _textFormat = "Time: {0}";
        [SerializeField] private TMP_Text _text;

        private readonly CompositeDisposable _disposables = new();

        [Inject]
        private void Inject(ITimeService timeService) {
            timeService.TimeElapsed.Subscribe(TimeChanged).AddTo(_disposables);
        }

        private void OnDestroy() {
            _disposables?.Dispose();
        }

        private void TimeChanged(TimeSpan time) {
            _text.text = string.Format(_textFormat, time.ToSafeString());
        }
    }
}
