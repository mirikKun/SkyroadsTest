using Code.Gameplay.LevelGenerator.Configs;
using Code.Gameplay.LevelGenerator.Factories;
using Code.Gameplay.LevelGenerator.LevelChunks;
using Code.Gameplay.LevelGenerator.Obstacles;
using Code.Gameplay.ScoreCounter.Systems;
using Code.Gameplay.StaticData;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.LevelGenerator.Systems
{
    public class ObstacleGeneratorSystem : IObstacleGeneratorSystem
    {
        private LevelGeneratorConfig _getLevelGeneratorConfig;
        private LevelDifficultyConfig _getLevelDifficultyConfig;
        private float _distanceMultiplier;
        private IScoreCounterSystem _scoreCounterSystem;
        private ILevelFactory _levelFactory;

        public ObstacleGeneratorSystem(IStaticDataService staticDataService,IScoreCounterSystem scoreCounterSystem,ILevelFactory levelFactory)
        {
            _levelFactory = levelFactory;
            _scoreCounterSystem = scoreCounterSystem;
            
            _getLevelGeneratorConfig = staticDataService.GetLevelGeneratorConfig();
            _getLevelDifficultyConfig = staticDataService.GetLevelDifficultyConfig();
        }

        public void GenerateObstacles(LevelChunk previousChunk, LevelChunk newChunk)
        {
            UpdateDifficulty(newChunk);
            
            
            Vector2 chunkSize = newChunk.ChunkSize;
            Vector3 chunkPosition = newChunk.transform.position;

            float startX = chunkPosition.x - chunkSize.x / 2;
            float endX = chunkPosition.x + chunkSize.x / 2;
            float startZ = chunkPosition.z - chunkSize.y / 2;
            float endZ = chunkPosition.z + chunkSize.y / 2;
            ObstacleData obstacleData = _levelFactory.GetRandomObstacleData();
            

            float obstacleGridFrequency = _getLevelGeneratorConfig.ObstacleGridFrequency;
            for (float z = startZ; z <= endZ; z += obstacleGridFrequency)
            {
                float randomXOffset = Random.Range(startX, endX);
                for (float x = startX; x <= endX; x += obstacleGridFrequency)
                {
                    float randomX = (x + randomXOffset);
                    randomX = randomX > endX ? randomX - endX : randomX;
                    Vector3 position = new Vector3(randomX, chunkPosition.y, z);


                    if (CanPlaceObstacle(position, obstacleData.ObstacleRadius * _distanceMultiplier, previousChunk, newChunk))
                    {
                        _levelFactory.CreateObstacle(position, newChunk, obstacleData.ObstacleIndex);
                    }
                }
            }
        }

        private void UpdateDifficulty(LevelChunk newChunk)
        {
            float score= _scoreCounterSystem.Score;
            _distanceMultiplier = _getLevelDifficultyConfig.GetObstacleRadiusMultiplier(score);
            newChunk.SetObstaclesRadiusMultiplier(_distanceMultiplier);
        }


        private bool CanPlaceObstacle(Vector3 position, float radius, LevelChunk previousChunk,
            LevelChunk newChunk)
        {
            foreach (Obstacle obstacle in newChunk.Obstacles)
            {
                float minDistance = radius + obstacle.ObstacleRadius * _distanceMultiplier;
                if (Vector3.Distance(position, obstacle.transform.position) < minDistance)
                {
                    return false;
                }
            }

            if (previousChunk != null)
            {
                Obstacle[] obstaclesInPreviousChunk = previousChunk.GetComponentsInChildren<Obstacle>();
                foreach (Obstacle obstacle in obstaclesInPreviousChunk)
                {
                    float minDistance = radius + obstacle.ObstacleRadius * _distanceMultiplier;
                    if (Vector3.Distance(position, obstacle.transform.position) < minDistance)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}