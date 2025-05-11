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

        public GameloopLoopState(IPlayerMoverSystem playerMoverSystem,IScoreCounterSystem scoreCounterSystem,ILevelGeneratorSystem levelGeneratorSystem)
        {
            _playerMoverSystem = playerMoverSystem;
            _scoreCounterSystem = scoreCounterSystem;
            _levelGeneratorSystem = levelGeneratorSystem;
        }

        public void Enter()
        {
            _levelGeneratorSystem.Init();
        }

        public void Update()
        {
            _playerMoverSystem.UpdatePlayer();
            _scoreCounterSystem.UpdateScore();
            _levelGeneratorSystem.TryGenerate();
            _levelGeneratorSystem.TryDestroy();
        }

        public void Exit()
        {
        }
    }
}