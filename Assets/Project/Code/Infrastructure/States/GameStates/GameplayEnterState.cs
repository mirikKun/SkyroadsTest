using Cinemachine;
using Code.Gameplay.Common.Time;
using Code.Gameplay.LevelGenerator.Systems;
using Code.Gameplay.Levels;
using Code.Gameplay.Player.Behaviours;
using Code.Gameplay.Player.Factories;
using Code.Gameplay.Player.Systems;
using Code.Gameplay.Score.Systems;
using Code.Gameplay.Windows;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;

namespace Code.Infrastructure.States.GameStates
{
    public class GameplayEnterState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ILevelDataProvider _levelDataProvider;
        private readonly IPlayerFactory _playerFactory;
        private readonly IPlayerMoverSystem _playerMoverSystem;
        private readonly IScoreCounterSystem _scoreCounterSystem;
        private readonly ILevelGeneratorSystem _levelGeneratorSystem;
        private readonly IPassedObstaclesCounterSystem _passedObstaclesCounter;
        private readonly ITimeService _timeService;
        private readonly IWindowService _windowService;


        public GameplayEnterState(IGameStateMachine stateMachine, ILevelDataProvider levelDataProvider,
            IPlayerFactory playerFactory,IPlayerMoverSystem playerMoverSystem,
            IScoreCounterSystem scoreCounterSystem,ILevelGeneratorSystem levelGeneratorSystem,
            IPassedObstaclesCounterSystem passedObstaclesCounter,ITimeService timeService,
            IWindowService windowService)
        {
            _stateMachine = stateMachine;
            _levelDataProvider = levelDataProvider;
            _playerFactory = playerFactory;
            _playerMoverSystem = playerMoverSystem;
            _scoreCounterSystem = scoreCounterSystem;
            _levelGeneratorSystem = levelGeneratorSystem;
            _passedObstaclesCounter = passedObstaclesCounter;
            _timeService = timeService;
            _windowService = windowService;
        }

        public void Enter()
        {
            ResetSystems();
            PlacePlayer();
            SetupCamera();
            _levelGeneratorSystem.Init();
            _levelGeneratorSystem.TryGenerate();
            _windowService.Open(WindowId.GamePlayHud);
            _stateMachine.Enter<GameplayWaitForKeyState>();
        }

        private void ResetSystems()
        {
            _scoreCounterSystem.ResetScore();
            _passedObstaclesCounter.ResetPassedObstaclesCount();
            _timeService.ResetGameTime();
        }

        private void PlacePlayer()
        {
            PlayerContainer player = _playerFactory.CreatePlayer(_levelDataProvider.PlayerSpawnTransform, _levelDataProvider.PlayerSpawnTransform.position);
            _playerMoverSystem.SetPlayer(player);
            
        }

        private void SetupCamera()
        {
            CinemachineVirtualCamera camera = _levelDataProvider.MainCamera;
            camera.Follow = _playerMoverSystem.Player.HorizontalStaticTransform.transform;
            camera.LookAt = _playerMoverSystem.Player.HorizontalStaticTransform.transform;
        }

        public void Exit()
        {
        }
    }
}