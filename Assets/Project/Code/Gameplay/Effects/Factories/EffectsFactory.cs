using Code.Infrastructure.AssetManagement;
using UnityEngine;

namespace Code.Gameplay.Effects.Factories
{
    public class EffectsFactory : IEffectsFactory
    {
        private readonly IAssetProvider _assetProvider;
        private const string PlayerDeathEffectPath = "Effects/DeathEffect";

        public EffectsFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public void CreatePlayerDeathEffect(Vector3 position)
        {
            var effectPrefab = _assetProvider.LoadAsset(PlayerDeathEffectPath);
            var effect = Object.Instantiate(effectPrefab, position, Quaternion.identity);
        }
    }
}