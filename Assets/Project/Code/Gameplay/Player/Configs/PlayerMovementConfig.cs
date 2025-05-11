using UnityEngine;

namespace Code.Gameplay.Player.Configs
{
    [CreateAssetMenu(fileName = "PlayerMovementConfig", menuName = "Configs/Player Movement Config")]
    public class PlayerMovementConfig : ScriptableObject
    {
        [field: SerializeField] public float ForwardSpeed { get; private set; } = 15f;
        [field: SerializeField] public float SideSpeed { get; private set; } = 6f;
        [field: SerializeField] public float BoostSpeedMultiplier { get; private set; } = 2f;
        [field: SerializeField] public Vector2 RoadBorders { get; private set; } = new Vector2(-9, 9);
    }
}