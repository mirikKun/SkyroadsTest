using System;
using Code.Gameplay.ScoreCounter.Systems;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.ScoreCounter.Behaviours
{
    public class AsteroidsPassedDisplay:MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        private IPassedObstaclesCounterSystem _obstaclesCounter;

        [Inject]
        private void Construct(IPassedObstaclesCounterSystem obstaclesCounter)
        {
            _obstaclesCounter = obstaclesCounter;
        }

        private void Update()
        {
            _text.text=_obstaclesCounter.PassedObstaclesCount.ToString();
        }
    }
}