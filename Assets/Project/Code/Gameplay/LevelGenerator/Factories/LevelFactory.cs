using Code.Gameplay.LevelGenerator.Configs;
using Code.Gameplay.LevelGenerator.LevelChunks;
using Code.Gameplay.LevelGenerator.Obstacles;
using Code.Gameplay.StaticData;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.LevelGenerator.Factories
{
    public class LevelFactory : ILevelFactory
    {
        private IStaticDataService _staticDataService;

        public LevelFactory(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }
        public LevelChunk CreateLevelChunk(Vector3 lastPosition, Transform parent)
        {
            LevelGeneratorConfig config = _staticDataService.GetLevelGeneratorConfig();
            Vector3 newPosition = lastPosition + new Vector3(0, 0, config.LevelChunkPrefab.ChunkLength);

            LevelChunk chunk = Object.Instantiate(config.LevelChunkPrefab, newPosition, Quaternion.identity, parent);
            return chunk;
        }

        public Obstacle CreateObstacle(Vector3 at, LevelChunk levelChunk,int obstacleIndex)
        {
            LevelGeneratorConfig config = _staticDataService.GetLevelGeneratorConfig();
            Obstacle obstacle = Object.Instantiate(config.ObstaclePrefabs[obstacleIndex], at+config.Offset,
                Quaternion.Euler(0, Random.Range(0, 360), 0), levelChunk.transform);
            levelChunk.Obstacles.Add(obstacle);
            return obstacle;        
        }

        public ObstacleData GetRandomObstacleData()
        {
            LevelGeneratorConfig config = _staticDataService.GetLevelGeneratorConfig();
            int randomObstacleIndex = Random.Range(0, config.ObstaclePrefabs.Length);
            ObstacleData obstacleData = new ObstacleData()
            {
                ObstacleIndex = randomObstacleIndex,
                ObstacleRadius = config.ObstaclePrefabs[randomObstacleIndex].ObstacleRadius
            };
            return obstacleData;
        }
    }
}