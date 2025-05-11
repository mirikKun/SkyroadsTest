namespace Code.Gameplay.ScoreCounter.Systems
{
    public interface IScoreCounterSystem
    {
        float Score { get; }
        float HighScore { get; }
        void UpdateScore();
        void ResetScore();
    }
}