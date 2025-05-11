using Code.Gameplay.Player.Behaviours;
using Code.Gameplay.ScoreCounter.Systems;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Gameplay.LevelGenerator.Obstacles
{
    public class Obstacle:MonoBehaviour
    {
        [SerializeField] private float _obstacleRadius;

        
        [SerializeField] private Vector2 _randomSpeed;
        [SerializeField] private Vector3 _rotationAxis=new Vector3(0,1,0);


        private float _currentSpeed;
        private Transform _playerTransform;
        private IPassedObstaclesCounterSystem _passedObstaclesCounter;
        private bool _playerPassed;
        public float ObstacleRadius => _obstacleRadius;
        private void Start()
        {
            int randomSign = Random.Range(0, 2) == 0 ? -1 : 1;
            _currentSpeed= randomSign*Random.Range(_randomSpeed.x, _randomSpeed.y);
        }

        private void Update()
        {
            RotateView();
            CheckOnPlayerPassing();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerMover>(out PlayerMover playerMover))
            {
                Debug.Log("Death");
            }
        }

        public void SetPlayerPassingCheck(Transform playerTransform, IPassedObstaclesCounterSystem passedObstaclesCounter)
        {
            _passedObstaclesCounter = passedObstaclesCounter;
            _playerTransform = playerTransform;
        }

        private void CheckOnPlayerPassing()
        {
            if(!_playerPassed)
            {
                if (_playerTransform.position.z > transform.position.z)
                {
                    _passedObstaclesCounter.AddPassedObstacle();
                    _playerPassed = true;
                }
            }
        }

        private void RotateView()
        {
            transform.rotation = transform.rotation*Quaternion.Euler(_rotationAxis * (_currentSpeed * Time.deltaTime));
        }
    }
}