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
        public float X;
        public float Y;
        public float Z;
    }
}
