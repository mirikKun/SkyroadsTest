using Cinemachine;
using Code.Gameplay.Levels;
using Code.Gameplay.Player.Behaviours;
using Code.Gameplay.Player.Factories;
using Code.Gameplay.Player.Systems;
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


        public GameplayEnterState(IGameStateMachine stateMachine,
            ILevelDataProvider levelDataProvider,IPlayerFactory playerFactory,IPlayerMoverSystem playerMoverSystem)
        {
            _stateMachine = stateMachine;
            _levelDataProvider = levelDataProvider;
            _playerFactory = playerFactory;
            _playerMoverSystem = playerMoverSystem;
        }

        public void Enter()
        {
            PlacePlayer();
            SetupCamera();
            _stateMachine.Enter<GameloopLoopState>();
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