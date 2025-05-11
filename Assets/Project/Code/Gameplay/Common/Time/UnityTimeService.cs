using System;

namespace Code.Gameplay.Common.Time
{
    public class UnityTimeService : ITimeService
    {
        private bool _paused;

        public float DeltaTime => !_paused ? UnityEngine.Time.deltaTime : 0;
        public float CurrentGameTime { get; private set; }
        public void ResetGameTime()
        {
            CurrentGameTime = 0;
        }

        public void UpdateGameTime()
        {
            CurrentGameTime += DeltaTime;;
        }

        public DateTime UtcNow => DateTime.UtcNow;

        public void StopTime() => _paused = true;
        public void StartTime() => _paused = false;
    }
}