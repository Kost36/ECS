using ECSCore.BaseObjects;
using System;

namespace GameLib.Components
{
    /// <summary>
    /// Компонент веса
    /// Кг
    /// </summary>
    [Serializable]
    public class Weight : ComponentBase
    {
        /// <summary>
        /// Вес (Кг)
        /// </summary>
        public float Fact;
    }
}
