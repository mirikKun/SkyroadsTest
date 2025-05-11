using System.Collections.Generic;
using Code.Gameplay.LevelGenerator.Configs;
using Code.Gameplay.LevelGenerator.Factories;
using Code.Gameplay.LevelGenerator.LevelChunks;
using Code.Gameplay.Levels;
using Code.Gameplay.Player.Systems;
using Code.Gameplay.StaticData;
using UnityEngine;

namespace Code.Gameplay.LevelGenerator.Systems
{
    public class LevelGeneratorSystem : ILevelGeneratorSystem
    {
        private readonly IPlayerMoverSystem _playerMoverSystem;

        private readonly ILevelFactory _levelFactory;
        private readonly ILevelDataProvider _levelDataProvider;
        private readonly LevelGeneratorConfig _generatorConfig;
        private readonly IObstacleGeneratorSystem _obstacleGeneratorSystem;
        private Vector3 _lastRoadPosition;
        private readonly List<LevelChunk> _chunks = new List<LevelChunk>();

        private bool CanSpawnRoad =>
            _playerMoverSystem.PlayerPosition.z > _lastRoadPosition.z - _generatorConfig.AheadOffset;

        private bool CanRemoveRoad =>
            _chunks.Count > 0 && _playerMoverSystem.PlayerPosition.z >
            _chunks[0].transform.position.z + _generatorConfig.BackwardOffset;

        public LevelGeneratorSystem(IPlayerMoverSystem playerMoverSystem, ILevelFactory levelFactory,
            ILevelDataProvider levelDataProvider,
            IStaticDataService staticDataService, IObstacleGeneratorSystem obstacleGeneratorSystem)
        {
            _obstacleGeneratorSystem = obstacleGeneratorSystem;
            _generatorConfig = staticDataService.GetLevelGeneratorConfig();
            _levelDataProvider = levelDataProvider;
            _levelFactory = levelFactory;
            _playerMoverSystem = playerMoverSystem;
        }

        public void Init()
        {
            _lastRoadPosition = _levelDataProvider.LevelGeneratorTransform.position;

            foreach (var chunk in _chunks)
            {
                Object.Destroy(chunk.gameObject);
            }

            _chunks.Clear();
        }

        public void UpdateBehaviours()
        {
            foreach (var chunk in _chunks)
            {
                chunk.CheckObstaclesOnPass();
            }
        }

        public void TryGenerate()
        {
            while (CanSpawnRoad)
            {
                SpawnRoad();
            }
        }

        public void TryDestroy()
        {
            while (CanRemoveRoad)
            {
                RemoveRoad();
            }
        }

        private void SpawnRoad()
        {
            LevelChunk chunk =
                _levelFactory.CreateLevelChunk(_lastRoadPosition, _levelDataProvider.LevelGeneratorTransform);
            _lastRoadPosition = chunk.transform.position;
            if (_chunks.Count > 0)
            {
                _obstacleGeneratorSystem.GenerateObstacles(_chunks[^1], chunk);
            }

            _chunks.Add(chunk);
        }

        private void RemoveRoad()
        {
            LevelChunk road = _chunks[0];
            _chunks.RemoveAt(0);
            Object.Destroy(road.gameObject);
        }
    }
}