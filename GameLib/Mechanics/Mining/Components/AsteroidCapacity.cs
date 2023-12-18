using ECSCore.BaseObjects;

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
        public Minerals Mineral { get; set; }

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
