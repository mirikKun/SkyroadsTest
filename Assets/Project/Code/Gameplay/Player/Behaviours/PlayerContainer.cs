using UnityEngine;

namespace Code.Gameplay.Player.Behaviours
{
    public class PlayerContainer:MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Transform _horizontalStaticTransform;
        
        public Transform PlayerTransform=> _playerTransform;
        public Transform HorizontalStaticTransform => _horizontalStaticTransform;

    }
}