using UnityEngine;

namespace Game.Gameplay.UI {
    public class GameUI : MonoBehaviour {
        public void SetActive(bool isActive) {
            gameObject.SetActive(isActive);
        }
    }
}
