using ECSCore.BaseObjects;
using System;

namespace GameLib.Components.Energy
{
    /// <summary>
    /// Компонент энергии
    /// </summary>
    [Serializable]
    public class Energy : ComponentBase
    {
        /// <summary>
        /// Фактический запас энергии
        /// </summary>
        public float Fact;
    }
}
