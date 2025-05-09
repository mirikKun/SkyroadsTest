using System;
using Code.Gameplay.Input.Service;
using UnityEngine;
using Zenject;

namespace Project.Code.Gameplay.Player.Behaviours
{
    public class PlayerMover:MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Transform _horizontalStaticTransform;
        [SerializeField] private float _forwardSpeed = 5f;
        [SerializeField] private float _sideSpeed = 3f;
        [SerializeField] private float _boostSpeedMultiplier = 2f;
        [SerializeField] private Vector2 _roadBorders = new Vector2(-3, 3);
        private IInputService _inputService;
        
        private float _horizontalInput;
        private bool _isBoosted;
        
        public Action StartedBoost;
        public Action StoppedBoost;

        public Action<int> SideMoveStarted;
        public Action SideMoveStopped;
        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Update()
        {
            CheckBehaviourChanges();
            GetInput();
            MovePlayer();
        }

        private void GetInput()
        {
            _horizontalInput = _inputService.GetHorizontalAxis();
            _isBoosted = _inputService.HasSpaceInput();
        }

        private void MovePlayer()
        {
            float speedMultiplier = _isBoosted ?  _boostSpeedMultiplier : 1;
            Vector3 velocity = new Vector3(_horizontalInput * _sideSpeed*speedMultiplier, 0, _forwardSpeed*speedMultiplier);
            
            Vector3 newPosition = _playerTransform.position + velocity * Time.deltaTime;
            newPosition.x = Mathf.Clamp(newPosition.x, _roadBorders.x, _roadBorders.y);
            _playerTransform.position = newPosition;
            _horizontalStaticTransform.position = new Vector3(0, newPosition.y, newPosition.z);
        }

        private void CheckBehaviourChanges()
        {
            if(_isBoosted&&!_inputService.HasSpaceInput())
            {
                StoppedBoost?.Invoke();
            }
            if (!_isBoosted && _inputService.HasSpaceInput())
            {
                StartedBoost?.Invoke();
            }
            if (Mathf.Abs(_horizontalInput)<float.Epsilon && _inputService.HasAxisInput())
            {
                SideMoveStarted?.Invoke(Math.Sign(_inputService.GetHorizontalAxis()));
            }
            if(Mathf.Abs(_horizontalInput)>float.Epsilon && !_inputService.HasAxisInput())
            {
                SideMoveStopped?.Invoke();
            }
            
        }
    }
}