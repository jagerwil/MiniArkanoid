using System;
using R3;
using UnityEngine;

namespace Game.Gameplay._Services {
    public interface IInputService {
        public ReadOnlyReactiveProperty<float> MoveAxis { get; }
        public ReadOnlyReactiveProperty<Vector2> TouchPosition { get; }
        
        public event Action onTouchStarted;
        public event Action onTouchEnded;
        
        public event Action onShootBallPressed;
    }
}
