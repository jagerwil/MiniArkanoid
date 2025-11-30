using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace Game.Gameplay._Services.Implementations {
    public class TimeService : ITimeService, IGameplayService, IDisposable {
        private readonly ReactiveProperty<TimeSpan> _timeElapsed = new();
        
        private double _startTime;
        private CancellationTokenSource _cancelToken;
        
        public ReadOnlyReactiveProperty<TimeSpan> TimeElapsed => _timeElapsed;

        public void Dispose() {
            _cancelToken?.Dispose();
        }
        
        public void GameplayStarted() {
            _startTime = Time.realtimeSinceStartupAsDouble;
            UpdateTimerEndlesslyAsync().Forget();
        }

        public void GameplayEnded() {
            _cancelToken?.Cancel();
        }

        private async UniTask UpdateTimerEndlesslyAsync() {
            _cancelToken?.Cancel();
            _cancelToken = new CancellationTokenSource();

            var timeSinceStartup = Time.realtimeSinceStartup;
            _timeElapsed.Value = TimeSpan.FromSeconds(timeSinceStartup - _startTime);
            
            await UniTask.WaitForSeconds(1f, cancellationToken: _cancelToken.Token);
        }
    }
}
