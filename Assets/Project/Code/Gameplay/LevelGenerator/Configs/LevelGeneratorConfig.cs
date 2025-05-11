using Code.Gameplay.LevelGenerator.LevelChunks;
using Code.Gameplay.LevelGenerator.Obstacles;
using UnityEngine;

namespace Code.Gameplay.LevelGenerator.Configs
{
    [CreateAssetMenu(fileName = "LevelGeneratorConfig", menuName = "Configs/Level Generator Config")]
    public class LevelGeneratorConfig:ScriptableObject
    {
        [field: Header("Obstacles")]
        [field: SerializeField] public Obstacle[] ObstaclePrefabs { get; private set; }
        [field: SerializeField] public Vector3 Offset { get; private set; }
        [field: SerializeField] public float ObstacleGridFrequency { get; private set; } = 2;
        [field: Header("Road")]
        [field: SerializeField] public LevelChunk LevelChunkPrefab { get; private set; }

        [field: SerializeField] public float AheadOffset { get; private set; } = 90;
        [field: SerializeField] public float BackwardOffset { get; private set; } = 50;

    }
}