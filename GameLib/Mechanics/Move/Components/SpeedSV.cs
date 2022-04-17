using ECSCore.BaseObjects;
using System;

namespace GameLib.Mechanics.Move.Components
{
    /// <summary>
    /// Компонент заданной скорости.
    /// (m/sec)
    /// </summary>
    [Serializable]
    public class SpeedSV : ComponentBase
    {
        /// <summary>
        /// Заданная скорость по оси X
        /// (m/sec)
        /// </summary>
        public float dXSV;
        /// <summary>
        /// Заданная скорость по оси Y
        /// (m/sec)
        /// </summary>
        public float dYSV;
        /// <summary>
        /// Заданная скорость по оси Z
        /// (m/sec)
        /// </summary>
        public float dZSV;
        /// <summary>
        /// Заданная скорость
        /// (m/sec)
        /// </summary>
        public float SVSpeed;
        /// <summary>
        /// Флаг - заданная скорость изменена
        /// </summary>
        public bool Update;
    }
}
