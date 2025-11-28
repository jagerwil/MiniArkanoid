using System;
using R3;
using UnityEngine;

namespace Game.Gameplay._Services {
    public interface IInputService {
        public ReadOnlyReactiveProperty<float> MoveAxis { get; }
        public event Action onShootBallTriggered;
        
        public void Enable();
        public void Disable();
    }
}
