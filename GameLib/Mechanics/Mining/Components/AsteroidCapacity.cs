using ECSCore.BaseObjects;
using GameLib.Enums;

namespace GameLib.Mechanics.Mining.Components
{
    /// <summary>
    /// Вместимость сырья в астеройде
    /// </summary>
    public class AsteroidCapacity : ComponentBase
    {
        /// <summary>
        /// Тип минерала
        /// </summary>
        public MineralType Mineral { get; set; }
        /// <summary>
        /// Начальный объем
        /// </summary>
        public int InitialCapacity { get;  set; }
        /// <summary>
        /// остаточный объем
        /// </summary>
        public int Capacity { get; set; }
    }
}
