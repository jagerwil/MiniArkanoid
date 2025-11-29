using Game.Gameplay.Bricks;

namespace Game.Gameplay._Providers.Implementations {
    public class BricksFieldProvider : IBricksFieldProvider {
        public BricksField BricksField { get; }

        public BricksFieldProvider(BricksField bricksField) {
            BricksField = bricksField;
        }
    }
}
