namespace Code.Gameplay.Score.Systems
{
    public class PassedObstaclesCounterSystem : IPassedObstaclesCounterSystem
    {
        public int PassedObstaclesCount { get; private set; }

        public void AddPassedObstacle()
        {
            PassedObstaclesCount++;
        }

        public void ResetPassedObstaclesCount()
        {
            PassedObstaclesCount = 0;
        }
    }
}