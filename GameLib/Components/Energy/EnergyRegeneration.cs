using ECSCore.BaseObjects;
using System;

namespace GameLib.Components.Energy
{
    /// <summary>
    /// Компонент Регенерации энергии
    /// </summary>
    [Serializable]
    public class EnergyRegeneration : ComponentBase
    {
        /// <summary>
        /// Регенерации энергии в секунду
        /// </summary>
        public float Regeneration;
    }
}
