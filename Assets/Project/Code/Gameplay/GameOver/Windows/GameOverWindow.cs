using Code.Gameplay.ScoreCounter.Systems;
using Code.Gameplay.Windows;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Gameplay.GameOver.Windows
{
    public class GameOverWindow:BaseWindow
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _mainMenuButton;
        
        [SerializeField] private GameObject _newHighScorePanel;
        private IGameStateMachine _gameStateMachine;
        private IScoreCounterSystem _scoreCounterSystem;

        [Inject]
        private void Construct( IGameStateMachine gameStateMachine,IScoreCounterSystem scoreCounterSystem)
        {
            _scoreCounterSystem = scoreCounterSystem;
            _gameStateMachine = gameStateMachine;
        }
        protected override void OnAwake()
        {
            base.OnAwake();
            _restartButton.onClick.AddListener(RestartGame);
            _mainMenuButton.onClick.AddListener(GoToMainMenu);
            _newHighScorePanel.SetActive(_scoreCounterSystem.HasNewHighScore);
        }

        private void RestartGame()
        {
            _gameStateMachine.Enter<GameplayEnterState>();
        }

        private void GoToMainMenu()
        {
            _gameStateMachine.Enter<LoadingMainMenuScreenState>();

        }
    }
}