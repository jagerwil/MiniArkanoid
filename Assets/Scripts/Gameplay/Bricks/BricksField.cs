using System;
using System.Collections.Generic;
using EditorAttributes;
using UnityEngine;

namespace Game.Gameplay.Bricks {
    public class BricksField : MonoBehaviour {
        [SerializeField] private Vector2 _startPosition;
        [SerializeField] private Vector2 _brickSize;
        
        private List<Brick> _bricks = new();

        public event Action onAllBricksDestroyed;

        private void Awake() {
            MoveBricksToGrid();
            RestoreField();
        }

        public void RestoreField() {
            _bricks.Clear();
            foreach (Transform child in transform) {
                var brick = child.GetComponent<Brick>();
                if (brick) {
                    brick.Restore();
                }
                _bricks.Add(brick);
            }
        }

        [Button]
        private void MoveBricksToGrid() {
            foreach (Transform child in transform) {
                var brick = child.GetComponent<Brick>();
                if (!brick) {
                    continue;
                }

                brick.Initialize(() => BrickDestroyed(brick));
                MoveBrickToGrid(child);
            }
        }

        private void MoveBrickToGrid(Transform brick) {
            var gridPos = (Vector2)brick.localPosition - _startPosition;
                
            var gridIndex = Vector2.Scale(gridPos, new Vector2(1f / _brickSize.x, 1f / _brickSize.y));
            gridIndex.x = Mathf.RoundToInt(gridIndex.x);
            gridIndex.y = Mathf.RoundToInt(gridIndex.y);
                
            brick.localPosition = Vector2.Scale(gridIndex, _brickSize) + _startPosition;
        }

        private void BrickDestroyed(Brick brick) {
            _bricks.Remove(brick);
            if (_bricks.Count <= 0) {
                onAllBricksDestroyed?.Invoke();
            }
        }
    }
}
