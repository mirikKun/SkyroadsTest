using Cinemachine;
using UnityEngine;

namespace Code.Gameplay.Levels
{
    public interface ILevelDataProvider
    {
        Transform PlayerSpawnTransform { get; }
        Transform LevelGeneratorTransform { get; }
        CinemachineVirtualCamera MainCamera { get; }
        void SetStartPoint(Transform spawnTransform);
        void SetLevelGeneratorTransform(Transform levelGeneratorTransform);
        void SetCamera(CinemachineVirtualCamera mainCamera);
    }
}