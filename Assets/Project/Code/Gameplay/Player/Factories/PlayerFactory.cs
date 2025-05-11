using Code.Gameplay.Player.Behaviours;
using Code.Infrastructure.AssetManagement;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Player.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
        private IAssetProvider _assetProvider;
        private IInstantiator _instantiator;

        public PlayerFactory(IAssetProvider assetProvider, IInstantiator instantiator)
        {
            _instantiator = instantiator;
            _assetProvider = assetProvider;
        }

        public PlayerContainer CreatePlayer(Transform parent, Vector3 position)
        {
            PlayerContainer playerPrefab = _assetProvider.LoadAsset<PlayerContainer>("Player/Player");
            PlayerContainer player = _instantiator.InstantiatePrefabForComponent<PlayerContainer>(playerPrefab, position, Quaternion.identity, parent);
            return player;
        }
    }
}