using System;
using System.Collections.Generic;
using Project.Code.Gameplay.LevelGenerator.LevelChunks;
using UnityEngine;

namespace Project.Code.Gameplay.LevelGenerator.Behaviours
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _player;
        [SerializeField] private LevelChunk _levelChunkPrefab;

        [SerializeField] private float _aheadOffset = 90;
        [SerializeField] private float _backwardOffset = 50;


        [SerializeField] private ObstaclesGenerator _obstaclesGenerator;

        private Vector3 _lastRoadPosition;
        private List<LevelChunk> _chunks = new List<LevelChunk>();

        private bool CanSpawnRoad => _player.position.z > _lastRoadPosition.z - _aheadOffset;

        private bool CanRemoveRoad =>
            _chunks.Count > 0 && _player.position.z > _chunks[0].transform.position.z + _backwardOffset;

        private void Start()
        {
            _lastRoadPosition = _startPoint.position;
        }

        private void Update()
        {
            while (CanSpawnRoad)
            {
                SpawnRoad();
            }

            while (CanRemoveRoad)
            {
                RemoveRoad();
            }
        }

        private void SpawnRoad()
        {
            LevelChunk chunk = GameObject.Instantiate(_levelChunkPrefab, _startPoint.position, Quaternion.identity,
                _startPoint);
            chunk.transform.position = _lastRoadPosition + new Vector3(0, 0, chunk.ChunkLength);
            _lastRoadPosition = chunk.transform.position;
            if (_chunks.Count > 0)
            {
                _obstaclesGenerator.SpawnObstacles(_chunks[^1], chunk); 
            }

            _chunks.Add(chunk);
        }

        private void RemoveRoad()
        {
            LevelChunk road = _chunks[0];
            _chunks.RemoveAt(0);
            GameObject.Destroy(road.gameObject);
        }
    }
}