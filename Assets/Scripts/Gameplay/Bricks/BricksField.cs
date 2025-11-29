using EditorAttributes;
using UnityEngine;

namespace Game.Gameplay.Bricks {
    public class BricksField : MonoBehaviour {
        [SerializeField] private Vector2 _startPosition;
        [SerializeField] private Vector2 _brickSize;

        [Button]
        private void MoveBricksToGrid() {
            foreach (Transform child in transform) {
                var gridPos = (Vector2)child.localPosition - _startPosition;
                
                var gridIndex = Vector2.Scale(gridPos, new Vector2(1f / _brickSize.x, 1f / _brickSize.y));
                gridIndex.x = Mathf.RoundToInt(gridIndex.x);
                gridIndex.y = Mathf.RoundToInt(gridIndex.y);
                
                child.localPosition = Vector2.Scale(gridIndex, _brickSize) + _startPosition;
            }
        }
    }
}
