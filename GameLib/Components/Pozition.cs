using ECSCore.BaseObjects;
using System;

namespace GameLib.Components
{
    /// <summary>
    /// Компонент позиции
    /// </summary>
    [Serializable]
    public class Pozition : ComponentBase
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
