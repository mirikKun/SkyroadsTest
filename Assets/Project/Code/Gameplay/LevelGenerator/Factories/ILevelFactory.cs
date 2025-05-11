using Code.Gameplay.LevelGenerator.LevelChunks;
using Code.Gameplay.LevelGenerator.Obstacles;
using UnityEngine;

namespace Code.Gameplay.LevelGenerator.Factories
{
    public interface ILevelFactory
    {
        LevelChunk CreateLevelChunk(Vector3 lastPosition, Transform parent);
        Obstacle CreateObstacle(Vector3 at, LevelChunk levelChunk,int obstacleIndex);
        
        ObstacleData GetRandomObstacleData();
    }
}