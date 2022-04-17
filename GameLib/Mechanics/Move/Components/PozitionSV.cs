using ECSCore.BaseObjects;
using System;

namespace GameLib.Mechanics.Move.Components
{
    /// <summary>
    /// Компонент заданной позиции.
    /// Позиция, в которую нужно переместиться
    /// </summary>
    [Serializable]
    public class PozitionSV : ComponentBase
    {
        /// <summary>
        /// Позиция X
        /// </summary>
        public float X;
        /// <summary>
        /// Позиция Y
        /// </summary>
        public float Y;
        /// <summary>
        /// Позиция Z
        /// </summary>
        public float Z;
    }
}
