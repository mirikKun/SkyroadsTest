using System.Collections.Generic;
using Project.Code.Gameplay.LevelGenerator.LevelChunks;
using Project.Code.Gameplay.Obstacles;
using UnityEngine;

namespace Project.Code.Gameplay.LevelGenerator.Behaviours
{
    public class ObstaclesGenerator : MonoBehaviour
    {
        [SerializeField] private Obstacle[] _obstaclePrefabs;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _distanceMultiplier;
        [SerializeField] private int _exceptionsCount;
        [SerializeField] private float _obstacleGridFrequency = 2;

        public void SpawnObstacles(LevelChunk previousChunk, LevelChunk newChunk)
        {
            List<Obstacle> placedObstacles = new List<Obstacle>();

            // Створюємо сітку для розміщення перешкод
            Vector2 chunkSize = newChunk.ChunkSize;
            Vector3 chunkPosition = newChunk.transform.position;

            // Визначаємо початкову та кінцеву позиції для сітки
            float startX = chunkPosition.x - chunkSize.x / 2;
            float endX = chunkPosition.x + chunkSize.x / 2;
            float startZ = chunkPosition.z - chunkSize.y / 2;
            float endZ = chunkPosition.z + chunkSize.y / 2;

            // Створення списку точок сітки для розміщення
            List<Vector3> gridPoints = new List<Vector3>();
            for (float x = startX; x <= endX; x += _obstacleGridFrequency)
            {
                for (float z = startZ; z <= endZ; z += _obstacleGridFrequency)
                {
                    gridPoints.Add(new Vector3(x, chunkPosition.y, z));
                }
            }

            // Перемішуємо точки для рандомізації початкового положення
            ShuffleList(gridPoints);

            // Розміщення перешкод у відповідності з сіткою
            int obstacleIndex = 0;
            while (gridPoints.Count > 0 && obstacleIndex < _obstaclePrefabs.Length)
            {
                // Вибираємо поточну перешкоду
                Obstacle obstaclePrefab = _obstaclePrefabs[obstacleIndex];
                float obstacleRadius = obstaclePrefab.ObstacleRadius * _distanceMultiplier;
                bool placed = false;

                // Шукаємо підходяще місце для цієї перешкоди
                for (int i = 0; i < gridPoints.Count; i++)
                {
                    Vector3 potentialPosition = gridPoints[i];

                    if (CanPlaceObstacle(potentialPosition, obstacleRadius, placedObstacles, previousChunk))
                    {
                        // Створюємо перешкоду
                        Obstacle obstacle = Instantiate(obstaclePrefab, potentialPosition,
                            Quaternion.Euler(0, Random.Range(0, 360), 0), newChunk.transform);
                        placedObstacles.Add(obstacle);

                        // Видаляємо використану точку
                        gridPoints.RemoveAt(i);
                        placed = true;
                        break;
                    }
                }

                // Якщо не вдалося розмістити поточну перешкоду, переходимо до наступної
                if (!placed || Random.value < 0.3f) // 30% шанс зміни перешкоди, навіть якщо поточна була розміщена
                {
                    obstacleIndex = (obstacleIndex + 1) % _obstaclePrefabs.Length;
                }

                // Якщо всі перешкоди перевірені і нічого не розміщено, завершуємо
                if (!placed && obstacleIndex == 0)
                {
                    break;
                }
            }

            // Додаємо випадкові винятки
            for (int i = 0; i < _exceptionsCount; i++)
            {
                // Вибираємо випадкову перешкоду
                Obstacle obstaclePrefab = _obstaclePrefabs[Random.Range(0, _obstaclePrefabs.Length)];
                float obstacleRadius = obstaclePrefab.ObstacleRadius * _distanceMultiplier;

                // Пробуємо розмістити у випадковій точці
                bool placed = false;
                for (int attempt = 0; attempt < 10; attempt++) // Обмеження на кількість спроб
                {
                    // Вибираємо випадкову позицію в межах чанку
                    float randomX = Random.Range(startX, endX);
                    float randomZ = Random.Range(startZ, endZ);
                    Vector3 randomPosition = new Vector3(randomX, chunkPosition.y, randomZ);

                    if (CanPlaceObstacle(randomPosition, obstacleRadius, placedObstacles, previousChunk))
                    {
                        // Створюємо перешкоду
                        Obstacle obstacle = Instantiate(obstaclePrefab, randomPosition,
                            Quaternion.Euler(0, Random.Range(0, 360), 0), newChunk.transform);
                        placedObstacles.Add(obstacle);
                        placed = true;
                        break;
                    }
                }
            }
        }

        private void ShuffleList<T>(List<T> list)
        {
            int n = list.Count;
            for (int i = 0; i < n; i++)
            {
                int r = i + Random.Range(0, n - i);
                T temp = list[r];
                list[r] = list[i];
                list[i] = temp;
            }
        }

        private bool CanPlaceObstacle(Vector3 position, float radius, List<Obstacle> placedObstacles,
            LevelChunk previousChunk)
        {
            foreach (Obstacle obstacle in placedObstacles)
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