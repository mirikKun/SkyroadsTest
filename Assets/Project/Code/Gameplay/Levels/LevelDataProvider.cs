using Cinemachine;
using UnityEngine;

namespace Code.Gameplay.Levels
{
    public class LevelDataProvider : ILevelDataProvider
    {
        public Transform PlayerSpawnTransform { get; private set; }
        public Transform LevelGeneratorTransform { get; private set; }
        
        public CinemachineVirtualCamera MainCamera { get; private set; }


        public void SetStartPoint(Transform spawnTransform)
        {
            PlayerSpawnTransform = spawnTransform;
        }

        public void SetLevelGeneratorTransform(Transform levelGeneratorTransform)
        {
            LevelGeneratorTransform = levelGeneratorTransform;
        }

        public void SetCamera(CinemachineVirtualCamera mainCamera)
        {
            MainCamera= mainCamera;
        }
    }
}