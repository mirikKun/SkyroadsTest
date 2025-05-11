namespace Code.Gameplay.Score.Systems
{
    public interface IPassedObstaclesCounterSystem
    {
        int PassedObstaclesCount { get; }
        void AddPassedObstacle();
        void ResetPassedObstaclesCount();
    }
}