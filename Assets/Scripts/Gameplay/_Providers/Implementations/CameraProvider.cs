using UnityEngine;

namespace Game.Gameplay._Providers.Implementations {
    public class CameraProvider : ICameraProvider {
        public Camera Camera { get; private set; }

        //Camera.main might not work when loading / switching scenes
        public CameraProvider(Camera camera) {
            Camera = camera;
        }
    }
}
