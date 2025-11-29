using System;
using Game.Gameplay.Balls;
using UnityEngine;

namespace Game.Gameplay {
    public class BallDespawnTrigger : MonoBehaviour {
        private void OnTriggerEnter2D(Collider2D other) {
            var ball = other.gameObject.GetComponent<Ball>();
            ball.Despawn(true);
        }
    }
}
