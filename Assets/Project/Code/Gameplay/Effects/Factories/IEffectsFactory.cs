using UnityEngine;

namespace Code.Gameplay.Effects.Factories
{
    public interface IEffectsFactory
    {
        void CreatePlayerDeathEffect(Vector3 position);
    }
}