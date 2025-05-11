using Code.Gameplay.Input.Service;
using Code.Gameplay.Windows;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;

namespace Code.Infrastructure.States.GameStates
{
    public class GameplayWaitForKeyState: IState, IUpdateable
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IInputService _inputService;
        private readonly IWindowService _windowService;

        public GameplayWaitForKeyState(IGameStateMachine stateMachine,IInputService inputService,IWindowService windowService)
        {
            _stateMachine = stateMachine;
            _inputService = inputService;
            _windowService = windowService;
        }

        public void Update()
        {
            if (_inputService.HasAnyInput())
            {
                _stateMachine.Enter<GameloopLoopState>();
            }
        }

        public void Enter()
        {
            _windowService.Open(WindowId.PressAnyKeyWindow);
        }

        public void Exit()
        {
            _windowService.Close(WindowId.PressAnyKeyWindow);
        }
    }
}