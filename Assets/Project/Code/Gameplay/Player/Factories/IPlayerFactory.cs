using Code.Gameplay.Player.Behaviours;
using UnityEngine;

namespace Code.Gameplay.Player.Factories
{
    public interface IPlayerFactory
    {
        PlayerContainer CreatePlayer(Transform parent, Vector3 position);
    }
}