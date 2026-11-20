namespace CityPresident.Core
{
    /// <summary>Available simulation speeds. Paused means no simulation ticks are processed.</summary>
    public enum GameSpeed
    {
        Paused = 0,
        Normal = 1,
        Fast = 2,
        VeryFast = 5,
        UltraFast = 10
    }
}
