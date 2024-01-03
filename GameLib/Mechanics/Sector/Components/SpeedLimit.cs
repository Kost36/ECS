using ECSCore.BaseObjects;

namespace GameLib.Mechanics.Sector.Components
{
    /// <summary>
    /// Лимит скоростей в секторе
    /// </summary>
    public class SpeedLimit : ComponentBase
    {
        /// <summary>
        /// Максимальная скорость перемещения (Км/ч)
        /// </summary>
        public float MaxSpeedKmPerHour = Constants.Constants.MaxSpeed * 3.6F; // Constants.Constants.MaxSpeed / 1000 * 3600 => Constants.Constants.MaxSpeed * 3.6;

        /// <summary>
        /// Максимальная скорость перемещения (М/сек)
        /// </summary>
        public float MaxSpeedMPerSec = Constants.Constants.MaxSpeed;
    }
}
