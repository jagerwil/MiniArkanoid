using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace Game.Gameplay._Services.Implementations {
    public class TimeService : ITimeService, IGameplayService, IDisposable {
        private readonly ReactiveProperty<TimeSpan> _timeElapsed = new();
        
        private float _startTime;
        private CancellationTokenSource _cancelToken;
        
        public ReadOnlyReactiveProperty<TimeSpan> TimeElapsed => _timeElapsed;

        public void Dispose() {
            _cancelToken?.Dispose();
        }
        
        public void GameplayStarted() {
            _startTime = Time.realtimeSinceStartup;
            UpdateTimerEndlesslyAsync().Forget();
        }

        public void GameplayEnded() {
            _cancelToken?.Cancel();
        }

        private async UniTask UpdateTimerEndlesslyAsync() {
            _cancelToken?.Cancel();
            _cancelToken = new CancellationTokenSource();

            while (true) {
                var deltaTime = Mathf.Round(Time.realtimeSinceStartup - _startTime);
                _timeElapsed.Value = TimeSpan.FromSeconds(deltaTime);
            
                await UniTask.WaitForSeconds(1f, cancellationToken: _cancelToken.Token);
            }
        }
    }
}
