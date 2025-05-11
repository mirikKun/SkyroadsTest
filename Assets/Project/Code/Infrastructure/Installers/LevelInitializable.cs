using Cinemachine;
using Code.Gameplay.Levels;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class LevelInitializer : MonoBehaviour, IInitializable
    {
        [SerializeField] private CinemachineVirtualCamera _mainCamera;
        [SerializeField] private Transform _levelGeneratorParent;
        [SerializeField] private Transform _playerSpawnPoint;
        private ILevelDataProvider _levelDataProvider;

        [Inject]
        private void Construct( ILevelDataProvider levelDataProvider)
        {
            _levelDataProvider = levelDataProvider;
        }

        public void Initialize()
        {
            _levelDataProvider.SetStartPoint(_playerSpawnPoint);
            _levelDataProvider.SetLevelGeneratorTransform(_levelGeneratorParent);
            _levelDataProvider.SetCamera(_mainCamera);
        }
    }
}