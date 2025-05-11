using Code.Gameplay.Effects.Factories;
using Code.Gameplay.Player.Behaviours;
using Code.Gameplay.Player.Systems;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using UnityEngine;

namespace Code.Gameplay.GameOver.Systems
{
    public class GameOverSystem : IGameOverSystem
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IPlayerMoverSystem _playerMoverSystem;
        private readonly IEffectsFactory _effectsFactory;

        public GameOverSystem(IGameStateMachine stateMachine, IPlayerMoverSystem playerMoverSystem,
            IEffectsFactory effectsFactory)
        {
            _stateMachine = stateMachine;
            _playerMoverSystem = playerMoverSystem;
            _effectsFactory = effectsFactory;
        }

        public void GameOver()
        {
            PlayerContainer player = _playerMoverSystem.Player;
            _effectsFactory.CreatePlayerDeathEffect(player.PlayerTransform.position);
            Object.Destroy(player.gameObject);
            _stateMachine.Enter<GameOverState>();
        }
    }
}