using ECSCore.BaseObjects;
using System;

namespace GameLib.Components.Energy
{
    /// <summary>
    /// Компонент вместимости энергии
    /// </summary>
    [Serializable]
    public class EnergyCapacity : ComponentBase
    {
        /// <summary>
        /// Максимальный запас энергии
        /// </summary>
        public float Max;
    }
}
