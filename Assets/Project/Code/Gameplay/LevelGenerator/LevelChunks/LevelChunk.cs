using System.Collections.Generic;
using Project.Code.Gameplay.Obstacles;
using UnityEngine;

namespace Project.Code.Gameplay.LevelGenerator.LevelChunks
{
    public class LevelChunk:MonoBehaviour
    {
        [SerializeField] private  Vector2 _chunkSize=new Vector2(18,40);


        private List<Obstacle> _obstacles = new List<Obstacle>();
        public float ChunkLength => _chunkSize.y;
        public Vector2 ChunkSize => _chunkSize;
        public List<Obstacle> Obstacles => _obstacles;
    }
}