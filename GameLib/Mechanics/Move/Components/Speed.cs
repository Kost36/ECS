using ECSCore.BaseObjects;
using System;

namespace GameLib.Mechanics.Move.Components
{
    /// <summary>
    /// Компонент скорости
    /// (m/sec)
    /// </summary>
    [Serializable]
    public class Speed : ComponentBase
    {
        /// <summary>
        /// Cкорости по оси X
        /// (m/sec)
        /// </summary>
        public float dX;
        /// <summary>
        /// Cкорости по оси Y
        /// (m/sec)
        /// </summary>
        public float dY;
        /// <summary>
        /// Cкорости по оси Z
        /// (m/sec)
        /// </summary>
        public float dZ;
        /// <summary>
        /// Фактической скорости
        /// (m/sec)
        /// </summary>
        public float Fact;
        /// <summary>
        /// Максимальной скорости
        /// (m/sec)
        /// </summary>
        public float Max;
    }
}
