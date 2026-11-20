using System;
using UnityEngine;

namespace CityPresident.Core
{
    /// <summary>
    /// Central simulation clock. Gameplay systems should subscribe to calendar events
    /// rather than performing economic calculations in Update().
    /// </summary>
    public sealed class SimulationClock : MonoBehaviour
    {
        [SerializeField, Min(1f)] private float realSecondsPerGameHour = 60f;
        [SerializeField] private GameSpeed speed = GameSpeed.Paused;

        public int Day { get; private set; } = 1;
        public int Month { get; private set; } = 1;
        public int Year { get; private set; } = 1;
        public int Hour { get; private set; }
        public GameSpeed Speed => speed;

        public event Action HourPassed;
        public event Action DayPassed;
        public event Action MonthPassed;
        public event Action YearPassed;

        private float elapsed;

        private void Update()
        {
            if (speed == GameSpeed.Paused) return;

            elapsed += Time.deltaTime * (int)speed;
            while (elapsed >= realSecondsPerGameHour)
            {
                elapsed -= realSecondsPerGameHour;
                AdvanceOneHour();
            }
        }

        public void SetSpeed(GameSpeed newSpeed) => speed = newSpeed;

        public void ConfigureSecondsPerGameHour(float seconds)
        {
            realSecondsPerGameHour = Mathf.Max(1f, seconds);
        }

        public void Restore(int day, int month, int year, int hour, GameSpeed restoredSpeed)
        {
            Day = Mathf.Clamp(day, 1, 30);
            Month = Mathf.Clamp(month, 1, 12);
            Year = Mathf.Max(1, year);
            Hour = Mathf.Clamp(hour, 0, 23);
            speed = restoredSpeed;
            elapsed = 0f;
        }

        private void AdvanceOneHour()
        {
            Hour++;
            HourPassed?.Invoke();

            if (Hour < 24) return;
            Hour = 0;
            Day++;
            DayPassed?.Invoke();

            if (Day <= 30) return;
            Day = 1;
            Month++;
            MonthPassed?.Invoke();

            if (Month <= 12) return;
            Month = 1;
            Year++;
            YearPassed?.Invoke();
        }
    }
}
