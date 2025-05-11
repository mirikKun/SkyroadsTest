using Code.Gameplay.Common.Time;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.ScoreCounter.Behaviours
{
    public class CurrentTimeDisplay:MonoBehaviour
    {
        [SerializeField] private TMP_Text _timeText;
        private ITimeService _timeService;

        [Inject]
        private void Construct(ITimeService timeService)
        {
            _timeService = timeService;
        }
        private void Update()
        {
            _timeText.text = _timeService.CurrentGameTime.ToString("0:00");
        }
    }
}