using System;
using Code.Gameplay.Input.Service;
using UnityEngine;
using Zenject;

namespace Project.Code.Gameplay.Player.Behaviours
{
    public class PlayerMover:MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private float _forwardSpeed = 5f;
        [SerializeField] private float _sideSpeed = 3f;
        [SerializeField] private float _boostSpeedMultiplier = 2f;
        [SerializeField] private Vector2 _roadBorders = new Vector2(-3, 3);
        private IInputService _inputService;
        
        private float _horizontalInput;
        private bool _isBoosted;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Update()
        {
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
        }
    }
}