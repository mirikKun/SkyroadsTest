using Code.Infrastructure.States.StateMachine;
using Project.Code.Infrastructure.States.GameStates;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Code.Gameplay.MainMenu
{
    public class MainMenuHud : MonoBehaviour
    {
        [SerializeField] private Button _gameStartButton;
        private IGameStateMachine _gameStateMachine;
        private const string GameplaySceneName = "Gameplay";

        [Inject]
        private void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        private void Start()
        {
            InitButtons();
        }

        private void InitButtons()
        {
            _gameStartButton.onClick.AddListener(EnterGameplayScene);
        }

        private void EnterGameplayScene()
        {
            _gameStateMachine.Enter<LoadingGameplayState, string>(GameplaySceneName);
        }
    }
}