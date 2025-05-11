using Code.Gameplay.Common.Time;
using Code.Gameplay.Player.Systems;
using Code.Progress.Provider;
using Zenject;

namespace Code.Gameplay.ScoreCounter.Systems
{
    public class ScoreCounterSystem : IScoreCounterSystem
    {
        private readonly IPlayerMoverSystem _playerMoverSystem;
        private readonly ITimeService _timeService;
        private readonly IProgressProvider _progressProvider;
        private const float PlayerInBoostScoreMultiplier = 2f;
        public float Score { get; private set; }
        public float HighScore=>_progressProvider.ProgressData.HighScore;


        public ScoreCounterSystem(IPlayerMoverSystem playerMoverSystem,ITimeService timeService, IProgressProvider progressProvider)
        {
            _timeService = timeService;
            _progressProvider = progressProvider;
            _playerMoverSystem = playerMoverSystem;
        }

        public void UpdateScore()
        {
            float scoreUpdate= (_playerMoverSystem.IsPlayerInBoost ? PlayerInBoostScoreMultiplier : 1f) *_timeService.DeltaTime;
            Score+= scoreUpdate;

            if (Score > HighScore)
            {
                _progressProvider.ProgressData.HighScore = Score;
                _progressProvider.SaveProgress();
            }
        }
        

        public void ResetScore()
        {
            Score = 0;
        }
    }
}