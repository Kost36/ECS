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
        public int MaxSpeedKPerHour = 1000;

        /// <summary>
        /// Максимальная скорость перемещения (М/сек)
        /// </summary>
        public int MaxSpeedMPerSec = 278;
    }
}
