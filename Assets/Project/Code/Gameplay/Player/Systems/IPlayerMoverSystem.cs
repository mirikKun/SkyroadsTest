using System;
using Code.Gameplay.Player.Behaviours;
using UnityEngine;

namespace Code.Gameplay.Player.Systems
{
    public interface IPlayerMoverSystem
    {
        void UpdatePlayer();
        void SetPlayer(PlayerContainer player);
        Vector3 PlayerPosition { get; }
        bool IsPlayerInBoost { get; }
        PlayerContainer Player { get; }


        event Action StartedBoost;
        event Action StoppedBoost;
        event Action<int> SideMoveStarted;
        event Action SideMoveStopped;
    }
}