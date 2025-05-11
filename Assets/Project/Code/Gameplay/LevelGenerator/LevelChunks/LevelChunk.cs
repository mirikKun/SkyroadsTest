using System.Collections.Generic;
using Code.Gameplay.LevelGenerator.Obstacles;
using UnityEngine;

namespace Code.Gameplay.LevelGenerator.LevelChunks
{
    public class LevelChunk : MonoBehaviour
    {
        [SerializeField] private Vector2 _chunkSize = new Vector2(18, 40);


        private readonly List<Obstacle> _obstacles = new List<Obstacle>();
        private float _radiusMultiplier;
        public float ChunkLength => _chunkSize.y;
        public Vector2 ChunkSize => _chunkSize;
        public List<Obstacle> Obstacles => _obstacles;

        public void CheckObstaclesOnPass()
        {
            foreach (Obstacle obstacle in _obstacles)
            {
                obstacle.CheckOnPlayerPassing();
            }
        }

        public void SetObstaclesRadiusMultiplier(float multiplier)
        {
            _radiusMultiplier = multiplier;
        }

        private void OnDrawGizmosSelected()
        {
            foreach (var obstacle in _obstacles)
            {
                Gizmos.DrawWireSphere(obstacle.transform.position, obstacle.ObstacleRadius * _radiusMultiplier);
            }
        }
    }
}