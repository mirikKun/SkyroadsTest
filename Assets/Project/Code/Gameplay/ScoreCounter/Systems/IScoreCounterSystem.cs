namespace Code.Gameplay.ScoreCounter.Systems
{
    public interface IScoreCounterSystem
    {
        float Score { get; }
        void UpdateScore();
        void ResetScore();
    }
}