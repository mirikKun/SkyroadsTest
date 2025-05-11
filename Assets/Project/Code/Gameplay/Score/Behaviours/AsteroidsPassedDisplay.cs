using Code.Gameplay.Score.Systems;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Score.Behaviours
{
    public class AsteroidsPassedDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        private IPassedObstaclesCounterSystem _obstaclesCounter;

        [Inject]
        private void Construct(IPassedObstaclesCounterSystem obstaclesCounter)
        {
            _obstaclesCounter = obstaclesCounter;
        }

        private void Start()
        {
            Display();
        }

        private void Update()
        {
            Display();
        }

        private void Display()
        {
            _text.text = _obstaclesCounter.PassedObstaclesCount.ToString();
        }
    }
}