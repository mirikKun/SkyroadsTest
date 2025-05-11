using Code.Gameplay.Common.Time;
using Code.Gameplay.Player.Systems;
using Zenject;

namespace Code.Gameplay.ScoreCounter.Systems
{
    public class ScoreCounterSystem : IScoreCounterSystem
    {
        private IPlayerMoverSystem _playerMoverSystem;
        private ITimeService _timeService;
        private const float PlayerInBoostScoreMultiplier = 2f;
        public float Score { get; private set; }

        
        public ScoreCounterSystem(IPlayerMoverSystem playerMoverSystem,ITimeService timeService)
        {
            _timeService = timeService;
            _playerMoverSystem = playerMoverSystem;
        }

        public void UpdateScore()
        {
            float scoreUpdate=_playerMoverSystem.IsPlayerInBoost?
                PlayerInBoostScoreMultiplier: 1f *_timeService.DeltaTime;
            Score+= scoreUpdate;
        }
        

        public void ResetScore()
        {
            Score = 0;
        }
    }
}