using System;
using Code.Gameplay.Input.Service;
using Code.Gameplay.Player.Behaviours;
using Code.Gameplay.Player.Configs;
using Code.Gameplay.Player.Factories;
using Code.Gameplay.StaticData;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Player.Systems
{
    public class PlayerMoverSystem : IPlayerMoverSystem
    {
        private IInputService _inputService;
        private float _horizontalInput;
        private bool _isBoosted;
        private PlayerMovementConfig _movementConfig;
        private PlayerContainer _player;

        public Vector3 PlayerPosition { get; private set; }

        public bool IsPlayerInBoost => _isBoosted;
        public PlayerContainer Player => _player;

        public event Action StartedBoost;

        public event Action StoppedBoost;

        public event Action<int> SideMoveStarted;

        public event Action SideMoveStopped;


        public PlayerMoverSystem(IInputService inputService,IStaticDataService staticDataService,IPlayerFactory playerFactory)
        {
            _inputService = inputService;
            
            _movementConfig = staticDataService.GetPlayerMovementConfig();
        }

        public void SetPlayer(PlayerContainer player)
        {
            _player = player;
            PlayerPosition= player.PlayerTransform.position;
        }
        
        public void UpdatePlayer()
        {
            CheckBehaviourChanges();
            GetInput();
            MovePlayer();
        }

        private void MovePlayer()
        {
            float speedMultiplier = _isBoosted ?  _movementConfig.BoostSpeedMultiplier : 1;
            Vector3 velocity = new Vector3(_horizontalInput * _movementConfig.SideSpeed*speedMultiplier, 0, _movementConfig.ForwardSpeed*speedMultiplier);
            
            Vector3 newPosition = _player.PlayerTransform.position + velocity * Time.deltaTime;
            newPosition.x = Mathf.Clamp(newPosition.x, _movementConfig.RoadBorders.x, _movementConfig.RoadBorders.y);
            _player.PlayerTransform.position = newPosition;
            _player.HorizontalStaticTransform.position = new Vector3(0, newPosition.y, newPosition.z);
            PlayerPosition=newPosition;
        }

        private void GetInput()
        {
            _horizontalInput = _inputService.GetHorizontalAxis();
            _isBoosted = _inputService.HasSpaceInput();
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