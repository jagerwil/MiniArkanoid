using System;
using Unity.Mathematics;
using UnityEngine;

namespace Game.Extensions {
    public static class TimeSpanExtensions {
        public static string ToMinutesSecondsString(this TimeSpan timeSpan) {
            var secondsFormat = $"{(int)math.round(timeSpan.Seconds):d2}";
            return $"{(int)math.round(timeSpan.TotalMinutes)}:{secondsFormat}";
        }
    }
}
