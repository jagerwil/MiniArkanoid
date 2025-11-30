using System;
using R3;

namespace Game.Gameplay._Services {
    public interface ITimeService { 
        public ReadOnlyReactiveProperty<TimeSpan> TimeElapsed { get; }
    }
}
