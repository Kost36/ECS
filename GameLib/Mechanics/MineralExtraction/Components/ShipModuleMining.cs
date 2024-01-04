using ECSCore.BaseObjects;

namespace GameLib.Mechanics.MineralExtraction.Components
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
        /// <summary>
        /// Количество добываемого сыря за цикл
        /// </summary>
        public int QuantityPerСycle = 5; //Todo в зависимости от типа модуля
    }
}
