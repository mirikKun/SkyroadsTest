using System;

namespace Code.Gameplay.Common.Time
{
    public interface ITimeService
    {
        float DeltaTime { get; }
        float CurrentGameTime { get; }
        
        void ResetGameTime();
        void UpdateGameTime();
        void StopTime();
        void StartTime();
    }
}