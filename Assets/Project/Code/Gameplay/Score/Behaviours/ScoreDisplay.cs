using System;
using Code.Gameplay.ScoreCounter.Systems;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.ScoreCounter.Behaviours
{
    public class ScoreDisplay:MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _highScoreText;
        private IScoreCounterSystem _scoreCounterSystem;

        [Inject]
        private void Construct(IScoreCounterSystem scoreCounterSystem)
        {
            _scoreCounterSystem = scoreCounterSystem;
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
            if(_scoreText)
                _scoreText.text = $"{_scoreCounterSystem.Score.ToString("0.0")}";
            if(_highScoreText)
                _highScoreText.text = $"{_scoreCounterSystem.HighScore.ToString("0.0")}";
        }
    }
}