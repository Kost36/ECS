using ECSCore.BaseObjects;
using System;

namespace GameLib.Mechanics.Move.Components
{
    /// <summary>
    /// Компонент пути торможения
    /// </summary>
    [Serializable]
    public class WayToStop : ComponentBase
    {
        /// <summary>
        /// Длинна пути торможения в метрах
        /// </summary>
        public float Len;
        /// <summary>
        /// Флаг - наличие энергии, для полного останова
        /// </summary>
        public bool EnargyHave;
    }
}
