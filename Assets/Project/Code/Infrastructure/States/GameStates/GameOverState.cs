using Code.Gameplay.LevelGenerator.Systems;
using Code.Gameplay.Windows;
using Code.Infrastructure.States.StateInfrastructure;

namespace Code.Infrastructure.States.GameStates
{
    public class GameOverState : IState
    {
        private readonly IWindowService _windowService;
        private readonly ILevelGeneratorSystem _levelGeneratorSystem;

        public GameOverState(IWindowService windowService,ILevelGeneratorSystem levelGeneratorSystem)
        {
            _windowService = windowService;
            _levelGeneratorSystem = levelGeneratorSystem;
        }

        public void Enter()
        {
            _windowService.Open(WindowId.GameOverWindow);
            _windowService.Close(WindowId.GamePlayHud);
        }

        public void Exit()
        {
            _windowService.Close(WindowId.GameOverWindow);
            _levelGeneratorSystem.Reset();
        }
    }
}