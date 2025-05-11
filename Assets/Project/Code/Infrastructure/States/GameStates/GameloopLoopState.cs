using Code.Gameplay.Common.Time;
using Code.Gameplay.LevelGenerator.Factories;
using Code.Gameplay.LevelGenerator.Systems;
using Code.Gameplay.Player.Systems;
using Code.Gameplay.ScoreCounter.Systems;
using Code.Infrastructure.States.StateInfrastructure;

namespace Code.Infrastructure.States.GameStates
{
    public class GameloopLoopState : IState, IUpdateable
    {
        private readonly IPlayerMoverSystem _playerMoverSystem;
        private readonly IScoreCounterSystem _scoreCounterSystem;
        private readonly ILevelGeneratorSystem _levelGeneratorSystem;
        private readonly ITimeService _timeService;

        public GameloopLoopState(IPlayerMoverSystem playerMoverSystem,
            IScoreCounterSystem scoreCounterSystem,
            ILevelGeneratorSystem levelGeneratorSystem,
            ITimeService timeService)
        {
            _playerMoverSystem = playerMoverSystem;
            _scoreCounterSystem = scoreCounterSystem;
            _levelGeneratorSystem = levelGeneratorSystem;
            _timeService = timeService;
        }

        public void Enter()
        {
            //_levelGeneratorSystem.Init();
        }

        public void Update()
        {
            _playerMoverSystem.UpdatePlayer();
            _scoreCounterSystem.UpdateScore();
            _levelGeneratorSystem.TryGenerate();
            _levelGeneratorSystem.TryDestroy();
            _levelGeneratorSystem.UpdateBehaviours();
            _timeService.UpdateGameTime();
        }

        public void Exit()
        {
        }
    }
}