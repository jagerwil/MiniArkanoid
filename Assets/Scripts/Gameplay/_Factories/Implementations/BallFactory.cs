using System;
using Game.Configs;
using Game.Gameplay.Balls;
using Jagerwil.Core.Architecture.Factories.Implementations;
using Jagerwil.Core.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Game.Gameplay._Factories.Implementations {
    public class BallFactory : BaseGameFactory<Ball>, IBallFactory {
        private readonly AssetReferenceGameObject _prefabAddress;

        public event Action<Ball> onBallSpawned;
        public event Action<Ball> onBallDespawned;
        public event Action onAllBallsDespawned;

        public BallFactory(IInstantiator instantiator,
            IAddressablesLoader addressablesLoader,
            PrefabAddresses prefabAddresses,
            Transform defaultRoot)
            : base(instantiator, addressablesLoader, new MemoryPoolSettings(), defaultRoot) {
            _prefabAddress = prefabAddresses.Ball;
        }

        public Ball Spawn(Vector3 position, bool isMoving, Vector2 moveDirection, Transform root) {
            var ball = CreateInternal(position, Quaternion.identity, root);
            if (!ball) {
                return null;
            }
            
            ball.Initialize(_defaultRoot);
            if (isMoving) {
                ball.Shoot(moveDirection);
            }
            
            onBallSpawned?.Invoke(ball);
            return ball;
        }

        public override void Despawn(Ball obj) {
            base.Despawn(obj);
            if (!obj) {
                return;
            }
            
            onBallDespawned?.Invoke(obj);
            if (!AreAnyObjectsSpawned) {
                onAllBallsDespawned?.Invoke();
            }
        }

        protected override AssetReferenceGameObject GetAssetReference() {
            return _prefabAddress;
        }
    }
}
