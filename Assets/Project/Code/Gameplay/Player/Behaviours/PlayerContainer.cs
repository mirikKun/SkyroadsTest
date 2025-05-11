using Code.Gameplay.GameOver.Systems;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Player.Behaviours
{
    public class PlayerContainer:MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Transform _horizontalStaticTransform;
        private IGameOverSystem _gameOverSystem;

        public Transform PlayerTransform=> _playerTransform;
        public Transform HorizontalStaticTransform => _horizontalStaticTransform;

        [Inject]
        private void Construct(IGameOverSystem gameOverSystem)
        {
            _gameOverSystem = gameOverSystem;
        }
        public void Die()
        {
            _gameOverSystem.GameOver();
        }
    }
}