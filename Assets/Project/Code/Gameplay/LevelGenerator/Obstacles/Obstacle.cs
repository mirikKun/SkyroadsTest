using System;
using Project.Code.Gameplay.Player.Behaviours;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Code.Gameplay.Obstacles
{
    public class Obstacle:MonoBehaviour
    {
        [SerializeField] private float _obstacleRadius;

        
        [SerializeField] private Vector2 _randomSpeed;
        [SerializeField] private Vector3 _rotationAxis=new Vector3(0,1,0);


        private float _currentSpeed;
        public float ObstacleRadius => _obstacleRadius;
        private void Start()
        {
            int randomSign = Random.Range(0, 2) == 0 ? -1 : 1;
            _currentSpeed= randomSign*Random.Range(_randomSpeed.x, _randomSpeed.y);
        
        }

        private void Update()
        {
            transform.rotation = transform.rotation*Quaternion.Euler(_rotationAxis * (_currentSpeed * Time.deltaTime));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerMover>(out PlayerMover playerMover))
            {
                Debug.Log("Death");
            }
        }
    }
}