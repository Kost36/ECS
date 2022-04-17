using ECSCore.BaseObjects;
using System;

namespace GameLib.Mechanics.Move.Components
{
    /// <summary>
    /// Компонент ускорения
    /// (dm/sec)
    /// </summary>
    [Serializable]
    public class Acceleration : ComponentBase
    {
        /// <summary>
        /// Ускорение 
        /// (dm/sec)
        /// </summary>
        public float Acc = 0.05f;
        /// <summary>
        /// Использование энергии, для ускорения
        /// (value/sec)
        /// </summary>
        public float EnargyUse = 5;
        /// <summary>
        /// Флаг - скорость достигнута
        /// </summary>
        public bool SpeedOk;
    }
}
