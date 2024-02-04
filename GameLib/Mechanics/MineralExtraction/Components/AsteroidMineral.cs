using ECSCore.BaseObjects;
using GameLib.Mechanics.Products.Enums;

namespace GameLib.Mechanics.MineralExtraction.Components
{
    /// <summary>
    /// Минерал астеройда
    /// </summary>
    public class AsteroidMineral : ComponentBase
    {
        /// <summary>
        /// Тип минерала
        /// </summary>
        public MineralType Type { get; set; }

        /// <summary>
        /// Начальный объем
        /// </summary>
        public int InitialCapacity { get; set; }

        /// <summary>
        /// Остаточный объем
        /// </summary>
        public int Capacity { get; set; }
    }
}
