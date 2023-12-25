using ECSCore.BaseObjects;

namespace GameLib.Mechanics.Mining.Components
{
    /// <summary>
    /// Корабельный модуль добычи ископаемых 
    /// </summary>
    public class ShipModuleMining : ComponentBase
    {
        /// <summary>
        /// Энергопотребление в секунду
        /// </summary>
        public float EnergyConsumptionPerSec;
        /// <summary>
        /// Процент выполнения в секунду
        /// </summary>
        public float CompletionPercentagePerSec;
    }
}
