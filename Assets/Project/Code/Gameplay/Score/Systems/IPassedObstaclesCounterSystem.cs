namespace Code.Gameplay.ScoreCounter.Systems
{
    public interface IPassedObstaclesCounterSystem
    {
        int PassedObstaclesCount { get; }
        void AddPassedObstacle();
        void ResetPassedObstaclesCount();
    }
}