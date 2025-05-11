using Code.Gameplay.LevelGenerator.LevelChunks;

namespace Code.Gameplay.LevelGenerator.Systems
{
    public interface IObstacleGeneratorSystem
    {
        void GenerateObstacles(LevelChunk previousChunk, LevelChunk newChunk);
    }
}