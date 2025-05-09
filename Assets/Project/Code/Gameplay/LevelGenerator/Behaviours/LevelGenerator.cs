using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Code.Gameplay.LevelGenerator.Behaviours
{
    public class LevelGenerator:MonoBehaviour
    {
        
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _roadPrefab;
        [SerializeField] private float _roadPrefabLength;
        [SerializeField] private float _aheadOffset=15;
        [SerializeField] private float _backwardOffset=5;
        
        [SerializeField] private Transform _player;
        private Vector3 _lastRoadPosition;
        private List<Transform> _roads = new List<Transform>();

        private bool CanSpawnRoad => _player.position.z > _lastRoadPosition.z - _aheadOffset;
        private bool CanRemoveRoad => _roads.Count>0&&_player.position.z > _roads[0].position.z + _backwardOffset;

        private void Start()
        {
            _lastRoadPosition= _startPoint.position;    
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
            Transform road = GameObject.Instantiate(_roadPrefab, _startPoint.position, Quaternion.identity);
            road.position = _lastRoadPosition + new Vector3(0, 0, _roadPrefabLength);
            _lastRoadPosition = road.position;
            _roads.Add(road);
            
        }

        private void RemoveRoad()
        {
            Transform road = _roads[0];
            _roads.RemoveAt(0);
            GameObject.Destroy(road.gameObject);
        }
    }
}